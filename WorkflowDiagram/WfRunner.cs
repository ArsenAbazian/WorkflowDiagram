using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class WfRunner {
        public WfRunner(WfDocument document) {
            Document = document;
        }
        
        public WfDocument Document { get; private set; }
        public bool Success { get; set; }

        public void Reset() {
            Document.Reset();
            IsStopped = false;
            VisitIndex = 0;
            if(Document.Owner != null)
                Document.Owner.OnReset(this);
        }

        protected void ResetNode(WfNode node) {
            node.VisitIndex = -1;
            foreach(var point in node.Points)
                ResetPoint(point);
        }

        public void Stop() {
            IsStopped = true;
        }

        public void Cancel() {
            IsStopped = true;
            if(CancellationSource != null)
                CancellationSource.Cancel();
        }

        protected bool IsStopped { get; set; }

        protected void ResetPoint(WfConnectionPoint point) {
            point.VisitIndex = -1;
        }
        protected void ResetConnector(WfConnector connector) {
            connector.VisitIndex = -1;
        }

        public int VisitIndex { get; protected set; }
        
        public Task<bool> InitializeAsync() {
            CancellationSource = new CancellationTokenSource();
            return Task.Run(() => { return Initialize(); }, CancellationSource.Token);
        }

        public bool RunOnceSubTree(WfConnectionPoint startPoint, out object operationRes) {
            WfNode lastVisited = LastVisitedNode;
            int visitIndex = VisitIndex;
            bool success = Success;
            bool result = false;
            operationRes = null;
            try {
                if(VisitIndex <= startPoint.VisitIndex)
                    VisitIndex = startPoint.VisitIndex + 1;
                startPoint.Visit(this, VisitIndex);
                result = RunCore(1, false, startPoint.GetNextNodes());
            }
            finally {
                operationRes = LastVisitedNode.DataContext;
                Success = success;
                VisitIndex = visitIndex;
                LastVisitedNode = lastVisited;
            }
            return result;
        }

        public virtual bool Initialize() {
            Reset();
            
            VisitIndex = 0;
            if(Document.Owner != null)
                Document.Owner.BeforeDocumentInitialize(this, Document);
            List<WfNode> currentNodes = Document.GetStartNodes();
            List<WfNode> waitList = new List<WfNode>();
            List<WfNode> nextNodes = new List<WfNode>();
            while(currentNodes.Count > 0) {
                nextNodes = new List<WfNode>();
                bool processed = false;
                for(int i = 0; i < currentNodes.Count;) {
                    if(!currentNodes[i].CheckDependency(VisitIndex)) {
                        i++;
                        continue;
                    }
                    if(currentNodes[i].IsVisited(VisitIndex)) {
                        currentNodes.RemoveAt(i);
                        continue;
                    }
                    processed = true;
                    InitializeNode(currentNodes[i], VisitIndex);
                    if(IsStopped)
                        return false;
                    if(CancellationSource != null && CancellationSource.IsCancellationRequested)
                        return false;
                    nextNodes.AddRange(currentNodes[i].GetNextNodes().Where(n => !n.IsVisited(VisitIndex)).ToList());
                    currentNodes.RemoveAt(i);
                }   
                currentNodes.AddRange(nextNodes);
                if(!processed)
                    return false;
            }
            if(Document.Owner != null)
                Document.Owner.AfterDocumentInitialize(this, Document);
            return true;
        }

        protected virtual bool RunCore(int maxIterationCount) {
            List<WfNode> startNodes = Document.GetStartNodes();
            VisitIndex = 0;
            return RunCore(maxIterationCount, true, startNodes);
        }

        protected WfNode LastVisitedNode { get; set; }
        protected virtual bool RunCore(int maxIterationCount, bool shouldReset, List<WfNode> startNodes) {
            if(shouldReset)
                Reset();
            
            for(int iterationIndex = 0; iterationIndex < maxIterationCount; iterationIndex++, VisitIndex++) {
                List<WfNode> currentNodes = startNodes;
                List<WfNode> waitList = new List<WfNode>();
                List<WfNode> nextNodes = new List<WfNode>();
                while(currentNodes.Count > 0) {
                    bool processed = false;
                    nextNodes = new List<WfNode>();
                    for(int i = 0; i < currentNodes.Count;) {
                        if(!currentNodes[i].CheckDependency(VisitIndex)) {
                            i++;
                            continue;
                        }
                        if(!currentNodes[i].Enabled) {
                            currentNodes.RemoveAt(i);
                            continue;
                        }
                        processed = true;
                        VisitNode(currentNodes[i]);
                        if(currentNodes[i].HasErrors) {
                            Document.DiagnosticHelper.Diagnostics.AddRange(currentNodes[i].Diagnostic);
                            Document.DiagnosticHelper.Error("One or more nodes has errors.");
                            return false;
                        }
                        if(IsStopped)
                            return Success;
                        if(CancellationSource != null && CancellationSource.IsCancellationRequested) {
                            Document.DiagnosticHelper.Warning("Process was canceled.");
                            return false;
                        }
                        var next = currentNodes[i].GetNodesFromVisitedPoints(VisitIndex);
                        foreach(var n in next) { // Move node to last...
                            nextNodes.Remove(n);
                            nextNodes.Add(n);
                        }
                        currentNodes.RemoveAt(i);
                    }
                    currentNodes.AddRange(nextNodes);
                    if(!processed) {
                        int count = currentNodes.Count(n => n.Enabled);
                        if(count > 0) {
                            Document.DiagnosticHelper.Error("No one node was processed, this means that your workflow entered endless loop.");
                            return false;
                        }
                    }
                }
            }
            Success = true;
            return true;
        }

        public bool RunOnce() {
            return RunCore(1);
        }

        public bool Run() {
            return RunCore(int.MaxValue);
        }

        protected CancellationTokenSource CancellationSource { get; private set; }
        public Task<bool> RunAsync() {
            CancellationSource = new CancellationTokenSource();
            return Task.Run(() => { return Run(); }, CancellationSource.Token);
        }

        public Task<bool> RunOnceAsync() {
            CancellationSource = new CancellationTokenSource();
            return Task.Run(() => { return RunOnce(); }, CancellationSource.Token);
        }

        protected virtual void VisitNode(WfNode node) {
            if(node.IsVisited(VisitIndex))
                return;
            node.OnVisit(this);
            LastVisitedNode = node;
        }

        protected virtual void InitializeNode(WfNode node, int visitIndex) {
            if(node.IsVisited(VisitIndex))
                return;
            node.OnInitialize(this);
        }
    }

    public class RunnerCancellationTokenSource : CancellationTokenSource {
        public RunnerCancellationTokenSource(WfRunner runner) { }
    }
}
