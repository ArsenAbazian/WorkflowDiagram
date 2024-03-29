﻿using System;
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
            return RunOnceSubTree(new WfConnectionPoint[] { startPoint }, startPoint, out operationRes);
        }

        public bool RunOnceSubTree(WfConnectionPoint[] startPoints, WfConnectionPoint branchPoint, out object operationRes) {
            WfNode lastVisited = LastVisitedNode;
            int visitIndex = VisitIndex;
            bool success = Success;
            bool result = false;
            operationRes = null;
            try {
                int startVisitIndex = VisitIndex;
                for(int i = 0; i < startPoints.Length; i++) {
                    WfConnectionPoint startPoint = startPoints[i];
                    startVisitIndex = Math.Max(startVisitIndex, startPoints[i].VisitIndex + 1);
                }
                startVisitIndex = Math.Max(startVisitIndex, branchPoint.VisitIndex + 1);
                VisitIndex = startVisitIndex;
                for(int i = 0; i < startPoints.Length; i++) {
                    WfConnectionPoint startPoint = startPoints[i];
                    startPoint.Visit(this, startPoint.Value);
                }
                branchPoint.Visit(this, branchPoint.Value);

                List<WfNode> startNodes = new List<WfNode>();
                for(int i = 0; i < startPoints.Length; i++)
                    startNodes.AddRange(startPoints[i].GetNextNodes());
                startNodes.AddRange(branchPoint.GetNextNodes());
                
                result = RunCore(1, false, startNodes, null);
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
                        processed = true;
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

        public virtual void WalkTree(WfNode rootNode, Action<WfNode> onVisit) {
            List<WfNode> currentNodes = rootNode.GetNextNodes();
            List<WfNode> waitList = new List<WfNode>();
            List<WfNode> nextNodes = new List<WfNode>();
            object visitMark = new object();
            while(currentNodes.Count > 0) {
                nextNodes = new List<WfNode>();
                for(int i = 0; i < currentNodes.Count;) {
                    if(currentNodes[i].VisitMark == visitMark) {
                        currentNodes.RemoveAt(i);
                        continue;
                    }
                    onVisit(currentNodes[i]);
                    currentNodes[i].VisitMark = visitMark;
                    nextNodes.AddRange(currentNodes[i].GetNextNodes().ToList());
                    currentNodes.RemoveAt(i);
                }
                currentNodes.AddRange(nextNodes);
            }
        }

        protected virtual bool RunCore(int maxIterationCount) {
            return RunCore(maxIterationCount, null);
        }

        protected virtual bool RunCore(int maxIterationCount, Func<List<WfNode>, bool> initStart) {
            List<WfNode> startNodes = Document.GetStartNodes();
            VisitIndex = 0;
            return RunCore(maxIterationCount, true, startNodes, initStart);
        }

        protected WfNode LastVisitedNode { get; set; }
        protected virtual bool RunCore(int maxIterationCount, bool shouldReset, List<WfNode> startNodes, Func<List<WfNode>, bool> initStart) {
            if(shouldReset)
                Reset();
            if(initStart != null)
                initStart(startNodes);
            List<WfNode> allNodes = new List<WfNode>();
            for(int i = 0; i < startNodes.Count; i++) {
                allNodes.Add(startNodes[i]);
                List<WfNode> children = startNodes[i].GetChildNodes();
                foreach(WfNode child in children)
                    if(!allNodes.Contains(child))
                        allNodes.Add(child);
            }
            Dictionary<WfNode, List<WfNode>> nn = new Dictionary<WfNode, List<WfNode>>();
            for(int iterationIndex = 0; iterationIndex < maxIterationCount; iterationIndex++, VisitIndex++) {
                List<WfNode> currentNodes = new List<WfNode>(startNodes);
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
                        if(currentNodes[i].GetIsSkipped()) {
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
                        nn[currentNodes[i]] = next;
                        foreach(var n in next) { // Move node to last...
                            nextNodes.Remove(n);
                            nextNodes.Add(n);
                        }
                        currentNodes.RemoveAt(i);
                    }
                    foreach(var next in nextNodes)
                        if(!currentNodes.Contains(next))
                            currentNodes.Add(next);
                    if(!processed) {
                        int count = currentNodes.Count(n => n.Enabled);
                        if(count > 0) {
                            for(int i = 0; i < currentNodes.Count; i++) {
                                if(!currentNodes[i].CheckDependency(VisitIndex))
                                    Document.DiagnosticHelper.Error("This node is depend on others, that were not updated. " + currentNodes[i].ToString());
                            }
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

        public bool RunOnce(Func<List<WfNode>, bool> initStart) {
            return RunCore(1, initStart);
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
