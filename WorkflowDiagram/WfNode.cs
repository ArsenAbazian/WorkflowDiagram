using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram {
    [Serializable]
    public abstract class WfNode : INotifyPropertyChanged {
        static Dictionary<Type, int> indices = new Dictionary<Type, int>();
        static int GetNextIndex(Type type) {
            if(!indices.ContainsKey(type))
                indices.Add(type, 0);

            indices[type]++;
            return indices[type];
        }

        protected bool IsRestoringFromXml() {
            StackTrace st = new StackTrace(true);
            int count = Math.Min(10, st.FrameCount);
            for(int i = 0; i < st.FrameCount; i++) {
                StackFrame sf = st.GetFrame(i);
                var m = sf.GetMethod();
                if(m.DeclaringType.Name.StartsWith("XmlSerializationReader"))
                    return true;
            }
            return false;
        }

        public WfNode() {
            Id = Guid.NewGuid();
            UpdatePoints();
            Name = Type.Replace(" ", "") + GetNextIndex(GetType());
        }

        public string GetText() {
            return string.IsNullOrEmpty(Text) ? Name : Text;
        }

        protected internal void OnPropertyChanged(string name) {
            if(this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
            if(Document != null)
                Document.RaiseChanged();
        }

        public virtual WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type) {
            return new WfConnectionPoint() { Type = type, AllowedOperations = WfEditOperation.Edit | WfEditOperation.Remove };
        }

        protected string ConstrainStringValue(string value) {
            if(value == null)
                value = string.Empty;
            value = value.Trim();
            return value;
        }

        object image = null;
        [XmlIgnore]
        [Browsable(false)]
        public object Image {
            get {
                if(image == null)
                    image = CreateImage();
                return image;
            }
        }
        protected virtual object CreateImage() { return Document?.ResourcesProvider?.GetNodeImage(this); }

        public void Connect(string outputName, WfNode node, string inputName) {
            Outputs[outputName].ConnectTo(node, inputName);
        }

        event PropertyChangedEventHandler propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged {
            add { this.propertyChanged += value; }
            remove { this.propertyChanged -= value; }
        }

        protected internal virtual void OnRemoved() {
            for(int i = 0; i < Inputs.Count; i++) {
                List<WfConnector> conn = Inputs[i].Connectors.ToList();
                for(int j = 0; j < conn.Count; j++)
                    conn[j].Detach();
            }
            for(int i = 0; i < Outputs.Count; i++) {
                List<WfConnector> conn = Outputs[i].Connectors.ToList();
                for(int j = 0; j < conn.Count; j++)
                    conn[j].Detach();
            }
        }

        protected internal virtual void OnPointRemoved(WfConnectionPoint point) {
            point.PropertyChanged -= OnPointPropertyChanged;
        }

        private void OnPointPropertyChanged(object sender, PropertyChangedEventArgs e) {
            OnPropertyChanged("Point." + e.PropertyName);
        }

        protected internal virtual void OnPointAdded(WfConnectionPoint point) {
            point.PropertyChanged -= OnPointPropertyChanged;
            point.PropertyChanged += OnPointPropertyChanged;
        }

        internal void OnPointsChanged(WfConnectionPointCollection wfConnectionPointCollection) {
            UpdatePoints();
            OnPropertyChanged(nameof(Points));
        }

        private void UpdatePoints() {
            if(this.points == null)
                return;
            points.Clear();
            foreach(var p in Inputs)
                points.Add(p);
            foreach(var p in Outputs)
                points.Add(p);
        }

        public bool IsVisited(int visitIndex) {
            return VisitIndex == visitIndex;
        }

        public virtual bool CheckDependency(int visitIndex) {
            for(int i = 0; i < Inputs.Count; i++) {
                var point = Inputs[i];
                if(point.Requirement == WfRequirementType.Mandatory && point.Connectors.Count == 0)
                    return false;
                for(int j = 0; j < point.Connectors.Count; j++) {
                    if(point.Connectors[j].From == null || !point.Connectors[j].From.IsVisited(visitIndex))
                        return false;
                }
            }
            return true;
        }

        protected string RunConnectionPointName { get { return "Run"; } }

        public virtual bool Enabled {
            get {
                WfConnectionPoint enable = Inputs[RunConnectionPointName];
                if(enable == null || enable.Connectors.Count == 0)
                    return true;
                for(int i = 0; i < enable.Connectors.Count; i++) {
                    WfConnectionPoint pt = enable.Connectors[i].From;
                    if(!pt.LastVisited)
                        return false;
                    if(pt.Value == null)
                        return true;
                    try {
                        if(Convert.ToBoolean(pt.Value))
                            return true;
                    }
                    catch(Exception) {
                        return pt.Value != null;
                    }
                }
                return false;
            }
        }

        public List<WfNode> GetNextNodes() {
            List<WfNode> nodes = new List<WfNode>();
            foreach(WfConnectionPoint point in Outputs)
                nodes.AddRange(point.GetNextNodes());
            return nodes;
        }

        public List<WfNode> GetPrevNodes() {
            List<WfNode> nodes = new List<WfNode>();
            foreach(WfConnectionPoint point in Inputs)
                nodes.AddRange(point.GetPrevNodes());
            return nodes;
        }

        public List<WfNode> GetNodesFromVisitedPoints(int visitIndex) {
            List<WfNode> nodes = new List<WfNode>();
            foreach(WfConnectionPoint point in Outputs) {
                if(!point.IsVisited(visitIndex) || point.SkipSubTree)
                    continue;
                nodes.AddRange(point.GetNextNodes());
            }
            return nodes;
        }

        public int OrderIndex { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int VisitIndex { get; set; } = -1;

        BindingList<WfConnectionPoint> points;
        [XmlIgnore]
        [Browsable(false)]
        public BindingList<WfConnectionPoint> Points {
            get {
                if(points == null) {
                    points = new BindingList<WfConnectionPoint>();
                    UpdatePoints();
                }
                return points;
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public virtual bool IsStartNode { get { return Inputs.Count == 0; } }

        [XmlIgnore]
        [Browsable(false)]
        public abstract string VisualTemplateName { get; }

        [Browsable(false)]
        public float X { get; set; }
        [Browsable(false)]
        public float Y { get; set; }
        [Browsable(false)]
        public float Width { get; set; }
        [Browsable(false)]
        public float Height { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public WfNodeCollection OwnerCollection { get; internal set; }

        public virtual string Name { get; set; }
        public string Text { get; set; }
        [XmlIgnore]
        [Browsable(false)]
        public abstract string Type { get; }
        [XmlIgnore]
        [Browsable(false)]
        public string DisplayText { get { return Type; } }
        [Browsable(false)]
        [XmlIgnore]
        public string IconText { get { return DisplayText.Substring(0, 2).ToUpper(); } }
        [Browsable(false)]
        [XmlIgnore]
        public virtual string Header { get; }
        [XmlIgnore]
        [Browsable(false)]
        public virtual string Category { get { return "Default"; } }

        object dataContext;
        [XmlIgnore]
        [Browsable(false)]
        public object DataContext {
            get { return dataContext; }
            set {
                if(object.Equals(DataContext, value))
                    return;
                dataContext = value;
                OnPropertyChanged("DataContext");
            }
        }

        public string Description { get; set; }
        [Browsable(false)]
        public WfDocument Document { get { return OwnerCollection?.Document; } }

        Guid id;
        [Browsable(false)]
        public Guid Id {
            get { return id; }
            set {
                if(Id == value)
                    return;
                id = value;
                OnIdChanged();
            }
        }

        protected virtual void OnIdChanged() {
            OnPropertyChanged(nameof(Id));
        }

        protected bool SuppressPointsChanged { get; set; }

        protected virtual WfConnectionPointCollection CreateOutputCollection() {
            return new WfConnectionPointCollection(this, WfConnectionPointType.Out);
        }

        protected virtual WfConnectionPointCollection CreateInputCollection() {
            return new WfConnectionPointCollection(this, WfConnectionPointType.In);
        }

        [XmlIgnore]
        [Browsable(false)]
        protected WfDiagnosticHelper DiagnosticHelper { get; } = new WfDiagnosticHelper();
        [XmlIgnore]
        [Browsable(false)]
        public List<WfDiagnosticInfo> Diagnostic { get { return DiagnosticHelper.Diagnostics; } }

        WfConnectionPointCollection inputs, outputs;
        [Browsable(false)]
        public WfConnectionPointCollection Inputs {
            get {
                if(inputs == null) {
                    inputs = CreateInputCollection();
                    if(IsRestoringFromXml())
                        return inputs;
                    try {
                        SuppressPointsChanged = true;
                        inputs.Add(GetDefaultInputsCore());
                    }
                    finally {
                        SuppressPointsChanged = false;
                    }
                }
                return inputs;
            }
        }
        protected WfConnectionPointCollection InputsCore { get { return inputs; } }
        [Browsable(false)]
        public WfConnectionPointCollection Outputs {
            get {
                if(outputs == null) {
                    outputs = CreateOutputCollection();
                    if(IsRestoringFromXml())
                        return inputs;
                    try {
                        SuppressPointsChanged = true;
                        outputs.Add(GetDefaultOutputsCore());
                    }
                    finally {
                        SuppressPointsChanged = false;
                    }
                }
                return outputs;
            }
        }
        protected WfConnectionPointCollection OutputsCore { get { return outputs; } }

        public event EventHandler Changed;
        protected internal void RaiseChanged() {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool HasInputConnections {
            get {
                if(Inputs.Count == 0)
                    return false;
                for(int i = 0; i < Inputs.Count; i++) {
                    if(Inputs[i].Connectors.Count > 0)
                        return true;
                }
                return false;
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool HasOutputConnections {
            get {
                if(Outputs.Count == 0)
                    return false;
                for(int i = 0; i < Outputs.Count; i++) {
                    if(Outputs[i].Connectors.Count > 0)
                        return true;
                }
                return false;
            }
        }

        protected virtual bool AllowAddRunPoint { get { return true; } }
        protected abstract List<WfConnectionPoint> GetDefaultInputs();
        List<WfConnectionPoint> GetDefaultInputsCore() {
            List<WfConnectionPoint> res = GetDefaultInputs();
            if(AllowAddRunPoint)
                res.Insert(0, new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = RunConnectionPointName, Text = RunConnectionPointName, Requirement = WfRequirementType.Optional }) ;
            res.ForEach(p => { if(p.Requirement == WfRequirementType.Default) p.Requirement = WfRequirementType.Mandatory; });
            return res;
        }

        List<WfConnectionPoint> GetDefaultOutputsCore() {
            List<WfConnectionPoint> res = GetDefaultOutputs();
            res.ForEach(p => { if(p.Requirement == WfRequirementType.Default) p.Requirement = WfRequirementType.Optional; });
            return res;
        }

        public List<WfConnector> GetOutputConnectors() {
            List<WfConnector> conn = new List<WfConnector>();
            for(int i = 0; i < Outputs.Count; i++) {
                conn.AddRange(Outputs[i].Connectors);
            }
            return conn;
        }

        public List<WfConnector> GetInputConnectors() {
            List<WfConnector> conn = new List<WfConnector>();
            for(int i = 0; i < Inputs.Count; i++) {
                conn.AddRange(Inputs[i].Connectors);
            }
            return conn;
        }

        protected abstract List<WfConnectionPoint> GetDefaultOutputs();

        bool isInitialized;
        [XmlIgnore]
        [Browsable(false)]
        public bool IsInitialized {
            get { return isInitialized; }
            set {
                if(IsInitialized == value)
                    return;
                isInitialized = value;
                OnPropertyChanged(nameof(IsInitialized));
            }
        }

        bool hasErrors;
        [XmlIgnore]
        [Browsable(false)]
        public bool HasErrors {
            get { return hasErrors; }
            set {
                if(HasErrors == value)
                    return;
                hasErrors = value;
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        public bool OnInitialize(WfRunner runner) {
            try {
                VisitIndex = runner.VisitIndex;
                IsInitialized = OnInitializeCore(runner);
                HasErrors = Diagnostic.Count(d => d.Type == WfDiagnosticSeverity.Error) > 0;
            }
            catch(Exception e) {
                DiagnosticHelper.Error("Exception occurs while initialize node. " + e.ToString());
                HasErrors = true;
                return false;
            }
            for(int i = 0; i < Inputs.Count; i++) Inputs[i].OnInitialize(runner);
            for(int i = 0; i < Outputs.Count; i++) Outputs[i].OnInitialize(runner);
            return IsInitialized;
        }
        protected abstract bool OnInitializeCore(WfRunner runner);
        protected abstract void OnVisitCore(WfRunner runner);
        public void OnVisit(WfRunner runner) {
            try {
                OnVisitCore(runner);
                VisitIndex = runner.VisitIndex;
            }
            catch(Exception e) {
                DiagnosticHelper.Add(WfDiagnosticSeverity.Error, e.ToString());
                HasErrors = true;
            }
        }
               

        protected internal virtual void OnEndDeserialize() {

        }

        public WfNode Clone() {
            WfNode node = (WfNode)GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
            node.Assign(this);
            return node;
        }

        public virtual void Assign(WfNode wfNode) {
            Name = wfNode.Name;
            Text = wfNode.Text;
            Description = wfNode.Description;
        }

        public void Reset() {
            VisitIndex = -1;
            HasErrors = false;
            ResetCore();
            foreach(var point in Points) {
                point.Reset();
            }
        }

        protected virtual void ResetCore() { }
    }
}
