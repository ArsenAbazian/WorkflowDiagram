using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram.Nodes.Base {
    public class WfProgrammNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Programm";

        public override string Type => "SubProgramm";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            InputParamNodes = GetInputParamNodes();
            List<WfConnectionPoint> res = new List<WfConnectionPoint>();
            for(int i = 0; i < InputParamNodes.Count; i++) {
                WfConnectionPoint point = CreatePointFromNode(InputParamNodes[i]); 
                res.Add(point);
            }
            return res;
        }

        private WfConnectionPoint CreatePointFromNode(WfNode wfNode) {
            return new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = wfNode.Name, Text = wfNode.GetText(), Id = wfNode.Id, Tag = wfNode };
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            OutputParamNodes = GetOutputParamNodes();
            List<WfConnectionPoint> res = new List<WfConnectionPoint>();
            for(int i = 0; i < OutputParamNodes.Count; i++) {
                WfConnectionPoint point = CreatePointFromNode(OutputParamNodes[i]);
                res.Add(point);
            }
            return res;
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            if(SubDocument == null)
                SubDocument = LoadDocument();
            if(SubDocument == null)
                return false;

            InputParamNodes = GetInputParamNodes();
            OutputParamNodes = GetOutputParamNodes();

            for(int i = 1; i < Inputs.Count; i++)
                Inputs[i].Tag = InputParamNodes.FirstOrDefault(n => n.Id == Inputs[i].Id);

            for(int i = 0; i < Outputs.Count; i++)
                Outputs[i].Tag = OutputParamNodes.FirstOrDefault(n => n.Id == Outputs[i].Id);

            return HasErrors;
        }

        private List<WfNode> GetOutputParamNodes() {
            if(SubDocument == null)
                return new List<WfNode>();
            if(SubDocument == null)
                return new List<WfNode>();
            List<WfNode> nodes = SubDocument.Nodes.Where(n => n is WfOutputNode && n.Enabled).ToList();
            return nodes;
        }

        private List<WfNode> GetInputParamNodes() {
            if(SubDocument == null)
                SubDocument = LoadDocument();
            if(SubDocument == null)
                return new List<WfNode>();
            List<WfNode> nodes = SubDocument.Nodes.Where(n => n is WfInputNode && n.Enabled).ToList();
            return nodes;
        }

        protected virtual WfDocument LoadDocument() {
            if(!string.IsNullOrEmpty(SubDocumentFilePath))
                return LoadDocumentFromFile();
            return LoadDocumentById();
        }

        protected virtual WfDocument LoadDocumentById() {
            return Document?.Owner?.LoadDocumentById(SubDocumentId);
        }

        protected virtual WfDocument LoadDocumentFromFile() {
            if(File.Exists(SubDocumentFilePath)) {
                WfDocument doc = new WfDocument();
                try {
                    doc.Load(SubDocumentFilePath);
                }
                catch(Exception e) {
                    OnError("Cannot load sub-document from file '" + SubDocumentFilePath + "'. " + e.ToString());
                }
                return document;
            }
            return null;
        }

        protected List<WfNode> InputParamNodes { get; set; }
        protected List<WfNode> OutputParamNodes { get; set; }

        protected override void OnVisitCore(WfRunner runner) {
            WfRunner subRunner = new WfRunner(SubDocument);
            if(!subRunner.Initialize()) {
                DiagnosticHelper.Diagnostics.AddRange(subRunner.Document.Diagnostics);
                OnError("Error initializing sub-document.");
                return;
            }
            if(!subRunner.Initialize()) {
                DiagnosticHelper.Diagnostics.AddRange(subRunner.Document.Diagnostics);
                OnError("Error initializing sub-document.");
                return;
            }
            if(!subRunner.RunOnce((st) => {
                for(int i = 1; i < Inputs.Count; i++) {
                    WfInputNode inputNode = (WfInputNode)Inputs[i].Tag;
                    inputNode.Value = Inputs[i].Value;
                }
                return true;
            })) {
                DiagnosticHelper.Diagnostics.AddRange(subRunner.Document.Diagnostics);
                OnError("Error executing sub-document.");
                return;
            }
            for(int i = 0; i < Outputs.Count; i++) {
                WfOutputNode outputNode = (WfOutputNode)Outputs[i].Tag;
                Outputs[i].Visit(runner, outputNode.Value);
            }
        }

        string subDocumentFilePath;
        [DisplayName("Document File"), PropertyEditor("WorkflowDiagram.UI.Win.Editors", "WorkflowDiagram.UI.Win.Editors.RepositoryItemOpenFileEditor")]
        public string SubDocumentFilePath {
            get { return subDocumentFilePath; }
            set {
                if(SubDocumentFilePath == value)
                    return;
                subDocumentFilePath = value;
                OnSubDocumentFilePathChanged();
            }
        }

        protected virtual void OnSubDocumentFilePathChanged() {
            if(string.IsNullOrEmpty(SubDocumentFilePath)) {
                SubDocument = null;
                return;
            }
            WfDocument doc = new WfDocument();
            doc.Load(SubDocumentFilePath);
            SubDocument = doc;
        }

        Guid subDocumentId;
        [DisplayName("Document Id")]
        public Guid SubDocumentId {
            get { return subDocumentId; }
            set {
                if(SubDocumentId == value)
                    return;
                subDocumentId = value;
                OnSubDocumentIdChanged();
            }
        }

        protected virtual void OnSubDocumentIdChanged() {
            if(Document.Project == null)
                return;
            SubDocument = Document.Project.Documents.FirstOrDefault(d => d.Id == SubDocumentId);
        }

        WfDocument document;
        [XmlIgnore]
        [Browsable(false)]
        public WfDocument SubDocument {
            get { return document; }
            set {
                if(SubDocument == value)
                    return;
                document = value;
                OnSubDocumentChanged();
            }
        }

        protected virtual void OnSubDocumentChanged() {
            RefreshConnectionPoints();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void RefreshConnectionPoints() {
            InputParamNodes = GetInputParamNodes();
            OutputParamNodes = GetOutputParamNodes();

            SyncConnectionPoints(InputsCore, InputParamNodes);
            SyncConnectionPoints(OutputsCore, OutputParamNodes);

            RaiseChanged();
        }

        protected virtual void SyncConnectionPoints(WfConnectionPointCollection points, List<WfNode> nodes) {
            if(points == null)
                return;
            int pointsStartIndex = points[RunConnectionPointName] == null ? 0 : 1;
            for(int i = 0; i < nodes.Count; i++) {
                WfConnectionPoint current = points.FirstOrDefault(p => p.Id == nodes[i].Id);
                int currentIndex = current == null ? 0 : points.IndexOf(current);
                if(current != null) {
                    int index = currentIndex - pointsStartIndex;
                    if(index != i)
                        points.Move(currentIndex, pointsStartIndex + i);
                }
                else {
                    points.Insert(pointsStartIndex + i, CreatePointFromNode(nodes[i]));
                }
            }
            List<WfConnectionPoint> toRemove = new List<WfConnectionPoint>();
            for(int i = 0; i < points.Count; i++) {
                if(points[i].Name == RunConnectionPointName)
                    continue;
                if(nodes.FirstOrDefault(n => n.Id == points[i].Id) == null)
                    toRemove.Add(points[i]);
            }
            for(int i = 0; i < toRemove.Count; i++)
                points.Remove(toRemove[i]);
        }
    }
}
