using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram {
    public class WfConnector : INotifyPropertyChanged
        //, ISupportSearch, ISelectedItemsOwner 
        {
        public WfConnector() {
            Id = Guid.NewGuid();
        }

        [Browsable(false)]
        public Guid Id { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int VisitIndex { get; set; } = -1;

        [XmlIgnore]
        [Browsable(false)]
        public Guid FromNodeId { get { return From == null || From.Node == null? Guid.Empty : From.Node.Id; } }
        [XmlIgnore]
        [Browsable(false)]
        public Guid ToNodeId { get { return To == null || To.Node == null? Guid.Empty : To.Node.Id; } }

        [Browsable(false)]
        public int FromIndex { get { return FromNode == null ? -1 : FromNode.Points.IndexOf(From); } }
        [Browsable(false)]
        public int ToIndex { get { return ToNode == null ? -1 : ToNode.Points.IndexOf(To); } }

        public void Detach() {
            if(From != null)
                From.Connectors.Remove(this);
            if(To != null)
                To.Connectors.Remove(this);
            Document.Connectors.Remove(this);
        }

        protected internal virtual void OnRemoved() {
            From = null;
            To = null;
            Document.RemoveUnusedConnectors();
        }

        Guid fromId;
        [Browsable(false)]
        public Guid FromId {
            get { return fromId; }
            set {
                if(FromId == value)
                    return;
                fromId = value;
                OnPropertyChanged(nameof(FromId));
            }
        }

        Guid toId;
        [Browsable(false)]
        public Guid ToId {
            get { return toId; }
            set {
                if(ToId == value)
                    return;
                toId = value;
                OnPropertyChanged(nameof(ToId));
            }
        }

        protected internal WfDocument Document { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public string FromNodeName { get { return FromNode?.Name; } }

        [XmlIgnore]
        [Browsable(false)]
        public string ToNodeName { get { return ToNode?.Name; } }

        [XmlIgnore]
        [Browsable(false)]
        public string FromPointName { get { return From?.Text; } }

        [XmlIgnore]
        [Browsable(false)]
        public string ToPointName { get { return To?.Text; } }

        WfConnectionPoint from;
        [XmlIgnore]
        [Browsable(false)]
        public WfConnectionPoint From {
            get {
                if(from == null && Document != null && FromId != Guid.Empty) {
                    from = Document.FindConnectionPoint(FromId);
                    if(from != null)
                        from.Connectors.Add(this);
                }
                return from;
            }
            set {
                if(From == value)
                    return;
                from = value;
                OnFromChanged();
            }
        }

        protected virtual void OnFromChanged() {
            FromId = this.from == null ? Guid.Empty : From.Id;
            if(From != null && !From.Connectors.Contains(this))
                From.Connectors.Add(this);
        }

        [XmlIgnore]
        [Browsable(false)]
        public WfNode ToNode { get { return To?.Collection.Node; } }

        [XmlIgnore]
        [Browsable(false)]
        public WfNode FromNode { get { return From?.Collection.Node; } }

        WfConnectionPoint to;
        [XmlIgnore]
        [Browsable(false)]
        public WfConnectionPoint To {
            get {
                if(to == null && Document != null && ToId != Guid.Empty) {
                    to = Document.FindConnectionPoint(ToId);
                    if(to != null)
                        to.Connectors.Add(this);
                }
                return to;
            }
            set {
                if(To == value)
                    return;
                to = value;
                OnToChanged();
            }
        }

        protected virtual void OnToChanged() {
            ToId = this.to == null ? Guid.Empty : To.Id;
            if(To != null && !To.Connectors.Contains(this))
                To.Connectors.Add(this);
        }

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

        internal void SetFromIdInternal(Guid id) {
            this.fromId = id;
        }

        public virtual void Reset() {
            VisitIndex = -1;
        }
        [XmlIgnore]
        public bool LastVisited { get; private set; }
        public void Visit(WfRunner runner, object value) {
            VisitIndex = runner.VisitIndex;
            LastVisited = true;
            if(To != null)
                To.Visit(runner, value);
        }

        public void SkipVisit(WfRunner runner, object value) {
            VisitIndex = runner.VisitIndex;
            LastVisited = false;
        }
    }
}
