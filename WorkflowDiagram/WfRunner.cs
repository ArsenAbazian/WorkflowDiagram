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

        protected int VisitIndex { get; set; }
        
        public Task<bool> InitializeAsync() {
            CancellationSource = new CancellationTokenSource();
            return Task.Run(() => { return Initialize(); }, CancellationSource.Token);
        }

        public virtual bool Initialize() {
            Reset();
            
            int visitIndex = 0;
            List<WfNode> currentNodes = Document.GetStartNodes();
            List<WfNode> waitList = new List<WfNode>();
            List<WfNode> nextNodes = new List<WfNode>();
            while(currentNodes.Count > 0) {
                nextNodes = new List<WfNode>();
                bool processed = false;
                for(int i = 0; i < currentNodes.Count;) {
                    if(!currentNodes[i].CheckDependency(visitIndex)) {
                        i++;
                        continue;
                    }
                    if(currentNodes[i].IsVisited(visitIndex)) {
                        currentNodes.RemoveAt(i);
                        continue;
                    }
                    processed = true;
                    InitializeNode(currentNodes[i], visitIndex);
                    if(IsStopped)
                        return false;
                    if(CancellationSource != null && CancellationSource.IsCancellationRequested)
                        return false;
                    nextNodes.AddRange(currentNodes[i].GetNextNodes().Where(n => !n.IsVisited(visitIndex)).ToList());
                    currentNodes.RemoveAt(i);
                }   
                currentNodes.AddRange(nextNodes);
                if(!processed)
                    return false;
            }
            return true;
        }

        protected virtual bool RunCore(int maxIterationCount) {
            Reset();

            List<WfNode> startNodes = Document.GetStartNodes();
            for(int visitIndex = 0; visitIndex < maxIterationCount; visitIndex++) {
                VisitIndex = visitIndex;
                List<WfNode> currentNodes = startNodes;
                List<WfNode> waitList = new List<WfNode>();
                List<WfNode> nextNodes = new List<WfNode>();
                while(currentNodes.Count > 0) {
                    nextNodes = new List<WfNode>();
                    bool processed = false;
                    for(int i = 0; i < currentNodes.Count;) {
                        if(!currentNodes[i].CheckDependency(visitIndex)) {
                            i++;
                            continue;
                        }
                        processed = true;
                        VisitNode(currentNodes[i], visitIndex);
                        if(currentNodes[i].HasErrors)
                            return false;
                        if(IsStopped)
                            return Success;
                        if(CancellationSource != null && CancellationSource.IsCancellationRequested)
                            return false;
                        var next = currentNodes[i].GetNextNodes();
                        foreach(var n in next) { // Move node to last...
                            nextNodes.Remove(n);
                            nextNodes.Add(n);
                        }
                        currentNodes.RemoveAt(i);
                    }
                    currentNodes.AddRange(nextNodes);
                    if(!processed)
                        return false;
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

        protected virtual void VisitNode(WfNode node, int visitIndex) {
            if(node.IsVisited(visitIndex))
                return;
            node.OnVisit(this);
            node.VisitIndex = visitIndex;
        }

        protected virtual void InitializeNode(WfNode node, int visitIndex) {
            node.OnInitialize(this);
            node.VisitIndex = visitIndex;
        }
    }

    public class RunnerCancellationTokenSource : CancellationTokenSource {
        public RunnerCancellationTokenSource(WfRunner runner) { }
    }
}
