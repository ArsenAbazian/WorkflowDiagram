using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public object Value {
            get { return _value; }
            set {
                if(object.Equals(Value, value))
                    return;
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        
        public string Name { get; set; }
        public string Text { get; set; }
        public string ColorString { get; set; }
        public WfRequirementType Requirement { get; set; } = WfRequirementType.Default;

        protected void OnPropertyChanged(string name) {
            if(this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
        }

        event PropertyChangedEventHandler propertyChanged;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
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

        public WfConnectionPointType Type { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int VisitIndex { get; set; } = -1;

        [XmlIgnore]
        [Browsable(false)]
        public string DisplayText {
            get {
                if(Value == null) 
                    return "null";
                return Value.ToString();
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

        public void OnVisit(WfRunner runner, object value) {
            Value = value;
            if(Type == WfConnectionPointType.In)
                return;
            foreach(WfConnector c in Connectors)
                c.OnVisit(runner, value);
        }

        public List<WfNode> GetNextNodes() {
            List<WfNode> nodes = new List<WfNode>();
            foreach(WfConnector connector in Connectors)
                nodes.Add(connector.ToNode);
            return nodes;
        }

        protected virtual void OnCollectionChanged(WfConnectionPointCollection prev) {
        
        }

        public virtual void Reset() {
            VisitIndex = -1;
        }
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
}