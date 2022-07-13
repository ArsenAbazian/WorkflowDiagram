using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram {
    [Serializable]
    public abstract class WfNode : INotifyPropertyChanged {
        public WfNode() {
            Id = Guid.NewGuid();
            UpdatePoints();
        }

        protected internal void OnPropertyChanged(string name) {
            if(this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
        }

        event PropertyChangedEventHandler propertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
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

        internal void OnPointsChanged(WfConnectionPointCollection wfConnectionPointCollection) {
            UpdatePoints();
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
                    if(point.Connectors[j].From == null || !point.Connectors[j].From.Collection.Node.IsVisited(visitIndex))
                        return false;
                }
            }
            return true;
        }

        public List<WfNode> GetNextNodes() {
            List<WfNode> nodes = new List<WfNode>();
            foreach(WfConnectionPoint point in Outputs)
                nodes.AddRange(point.GetNextNodes());
            return nodes;
        }

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

        WfConnectionPointCollection inputs, outputs;
        [Browsable(false)]
        public WfConnectionPointCollection Inputs {
            get {
                if(inputs == null) {
                    inputs = new WfConnectionPointCollection(this, WfConnectionPointType.In);
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
        [Browsable(false)]
        public WfConnectionPointCollection Outputs {
            get {
                if(outputs == null) {
                    outputs = new WfConnectionPointCollection(this, WfConnectionPointType.Out);
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

        protected abstract List<WfConnectionPoint> GetDefaultInputs();
        List<WfConnectionPoint> GetDefaultInputsCore() {
            List<WfConnectionPoint> res = GetDefaultInputs();
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
        public virtual bool OnInitialize(WfRunner runner) {
            IsInitialized = OnInitializeCore(runner);
            return IsInitialized;
        }
        protected abstract bool OnInitializeCore(WfRunner runner);
        public abstract void OnVisit(WfRunner runner);
        protected virtual List<WfNode> GetNextNodesToVisit() {
            List<WfNode> list = new List<WfNode>();
            for(int i = 0; i < Outputs.Count; i++) {
                var point = Outputs[i];
                if(!AllowProcessOutput(point))
                    continue;
                for(int j = 0; j < point.Connectors.Count; j++) {
                    WfNode nextNode = point.Connectors[j].ToNode;
                    if(nextNode != null)
                        list.Add(nextNode);
                }
            }
            return list;
        }

        protected virtual bool AllowProcessOutput(WfConnectionPoint point) {
            return true;
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

        public virtual void Reset() {
            VisitIndex = -1;
            foreach(var point in Points) {
                point.Reset();
            }
        }
    }
}
