using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace WorkflowDiagram {
    public class WfConnectionPoint : INotifyPropertyChanged {
        public WfConnectionPoint() {
            Id = Guid.NewGuid();
            Connectors = new WfConnectorCollection(this);
        }

        [Browsable(false)]
        public Guid Id { get; set; }

        [XmlIgnore]
        public WfNode Node { get { return Collection?.Node; } }

        object _value;
        [Category("Value")]
        [XmlIgnore]
        public virtual object Value {
            get { return _value; }
            set {
                if(object.Equals(Value, value))
                    return;
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        string name = "";
        public string Name {
            get { return name; }
            set {
                if(Name == value)
                    return;
                name = value;
                lowCaseName = null;
                OnPropertyChanged(nameof(Name));
            }
        }

        string lowCaseName;
        [Browsable(false)]
        public string LowCaseName {
            get {
                if(lowCaseName == null && Name != null)
                    lowCaseName = Name.ToLower();
                return lowCaseName;
            }
        }

        string text = "";
        public string Text {
            get { return text; }
            set {
                if(Text == value)
                    return;
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public object Tag { get; set; }

        public void ConnectTo(WfNode node, string inputName) {
            ConnectTo(node.Inputs[inputName]);
        }
        public void ConnectTo(WfConnectionPoint toPoint) {
            if(toPoint == null) {
                throw new ArgumentNullException("toPoint");
            }
            WfConnector connector = new WfConnector();
            Document.Connectors.Add(connector);
            connector.From = this;
            connector.To = toPoint;
        }

        public string ColorString { get; set; }
        public WfRequirementType Requirement { get; set; } = WfRequirementType.Default;

        protected void OnPropertyChanged(string name) {
            if(this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
        }

        event PropertyChangedEventHandler propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged {
            add {
                this.propertyChanged += value;
            }
            remove {
                this.propertyChanged -= value;
            }
        }

        [XmlIgnore]
        public WfDocument Document { get { return Collection?.Node.OwnerCollection?.Document; } }

        [XmlIgnore]
        [Browsable(false)]
        public WfConnectorCollection Connectors { get; private set; }

        public virtual bool IsVisited(int visitIndex) {
            return VisitIndex == visitIndex;
        }

        public bool IsVisitedByRunner {
            get { return Runner != null && IsVisited(Runner.VisitIndex); }
        }

        public WfConnectionPointType Type { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int VisitIndex { get; set; } = -1;

        [XmlIgnore]
        [Browsable(false)]
        public string DisplayText {
            get {
                if(Value == null)
                    return "";
                string res = Value.ToString();
                if(res == null)
                    return string.Empty;
                return res.Length > 16 ? res.Substring(0, 16) : res;
            }
        }

        WfConnectionPointCollection collection;
        [XmlIgnore, Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public WfConnectionPointCollection Collection {
            get { return collection; }
            set {
                if(Collection == value)
                    return;
                WfConnectionPointCollection prev = Collection;
                collection = value;
                OnCollectionChanged(prev);
            }
        }

        protected bool ValueCalculated { get; set; }
        [XmlIgnore]
        public bool LastVisited { get; protected set; }
        [XmlIgnore]
        public bool Skipped { get; protected set; }
        public virtual void SkipVisit(WfRunner runner, object value) {
            Value = value;
            ValueCalculated = true;
            LastVisited = false;
            Skipped = true;
            VisitIndex = runner.VisitIndex;
            if(Type == WfConnectionPointType.In)
                return;
            foreach(WfConnector c in Connectors)
                c.SkipVisit(runner, value);
        }
        public virtual void Visit(WfRunner runner, object value) {
            Value = value;
            ValueCalculated = true;
            VisitIndex = runner.VisitIndex;
            LastVisited = true;
            Skipped = false;
            if(Type == WfConnectionPointType.In)
                return;
            foreach(WfConnector c in Connectors)
                c.Visit(runner, value);
        }

        public List<WfNode> GetNextNodes() {
            List<WfNode> nodes = new List<WfNode>();
            foreach(WfConnector connector in Connectors)
                nodes.Add(connector.ToNode);
            return nodes;
        }

        public List<WfNode> GetPrevNodes() {
            List<WfNode> nodes = new List<WfNode>();
            foreach(WfConnector connector in Connectors)
                nodes.Add(connector.FromNode);
            return nodes;
        }

        protected virtual void OnCollectionChanged(WfConnectionPointCollection prev) {
        
        }

        public virtual void Reset() {
            VisitIndex = -1;
            Value = null;
            ValueCalculated = false;
            Skipped = false;
        }

        public virtual void OnInitialize(WfRunner runner) {
            Value = null;
            VisitIndex = runner.VisitIndex;
            Runner = runner;
        }

        protected internal bool GetHasInvalidConnectors() {
            return Connectors.Any(c => c.From == null || c.To == null);
        }

        public WfEditOperation AllowedOperations { get; set; } = WfEditOperation.None;
        public bool SkipSubTree { get; set; }
        protected WfRunner Runner { get; set; }
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool AllowEditName { get; set; } = false;
    }

    public enum WfConnectionPointType {
        In,
        Out
    }

    public enum WfRequirementType {
        Default,
        Mandatory,
        Optional
    }
    
    [Flags]
    public enum WfEditOperation {
        None = 0x0,
        Add = 0x1,
        Remove = 0x2,
        Edit = 0x4
    }
}