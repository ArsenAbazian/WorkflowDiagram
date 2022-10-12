using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XmlSerialization;

namespace WorkflowDiagram {
    
    [Serializable]
    [AllowDynamicTypes]
    [XmlInclude(typeof(WfConnectionPoint))]
    [XmlInclude(typeof(WfConnector))]
    [XmlInclude(typeof(WfNode))]
    [XmlInclude(typeof(WfDataInfo))]
    public class WfDocument : INotifyPropertyChanged, ISupportSerialization {
        public WfDocument() {
            Id = Guid.NewGuid();

            Nodes = new WfNodeCollection(this);
            Connectors = new WfDocumentConnectorCollection(this);
            CreationTime = DateTime.Now;

            InitializeDefaultColors();
        }

        protected internal virtual void OnNodesCollectionChanged() {
            OnPropertyChanged(nameof(Nodes));
        }

        protected internal virtual void OnConnectorsCollectionChanged() {
            OnPropertyChanged(nameof(Connectors));
        }

        public WfNode AddNode(WfNode node) {
            Nodes.Add(node);
            return node;
        }

        [XmlIgnore]
        [Browsable(false)]
        public WfDocumentCollection Documents { get; internal set; }
        [XmlIgnore]
        [Browsable(false)]
        public WfProject Project { get { return Documents?.Project; } }

        [XmlIgnore]
        public IWfDocumentOwner Owner { get; set; }

        [Browsable(false)]
        public Guid Id { get; set; }
        [Browsable(false)]
        public string FileName { get; set; }

        [Browsable(false)]
        public List<WfDataInfo> Data { get; } = new List<WfDataInfo>();
        protected Dictionary<Type, WfDataInfo> DataDictionary { get; } = new Dictionary<Type, WfDataInfo>();

        [XmlIgnore]
        public WfDiagnosticHelper DiagnosticHelper { get; } = new WfDiagnosticHelper();
        [XmlIgnore]
        public List<WfDiagnosticInfo> Diagnostics { get { return DiagnosticHelper.Diagnostics; } }

        /// <summary>
        /// Settings, not related to scripts
        /// </summary>
        [Browsable(false)]
        public int FontSizeDelta { get; set; } = 0;

        public List<WfNode> GetStartNodes() {
            return Nodes.Where(n => !n.HasInputConnections).OrderBy(n => n.OrderIndex).ToList();
        }

        public List<WfNode> GetEndNodes() {
            return Nodes.Where(n => !n.HasOutputConnections).ToList();
        }

        string name;
        public string Name {
            get { return name; }
            set {
                if(Name == value)
                    return;
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description { get; set; }
        public DateTime CreationTime { get; set; }

        string ISupportSerialization.FileName { get => FileName; set => FileName = value; }
        public event EventHandler Saved;
        public event EventHandler Loaded;

        public void Save() {
            if(string.IsNullOrEmpty(FullPath))
                return;
            Save(FullPath);
            if(Saved != null)
                Saved(this, EventArgs.Empty);
        }

        internal WfConnector FindConnector(Guid connectorId) {
            return Connectors.FirstOrDefault(c => c.Id == connectorId);
        }

        public void InitializeVisualData() {
            if(QueryNodeVisualData == null)
                return;
            foreach(var node in Nodes) {
                RaiseQueryVisualData(node);
            }
        }

        protected virtual void RaiseQueryVisualData(WfNode node) {
            if(QueryNodeVisualData != null)
                QueryNodeVisualData.Invoke(this, new WfNodeEventArgs(node));
        }

        public void Save(string fullPath) {
            FileName = Path.GetFileName(fullPath);
            if(string.IsNullOrEmpty(FileName))
                return;
            string path = Path.GetDirectoryName(fullPath);
            Reset();
            SerializationHelper.Current.Save(this, GetType(), path);
        }

        [XmlIgnore, Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string FullPath { get; set; }

        public bool Load(string fileName) {
            Clear();
            FullPath = fileName;
            if(SerializationHelper.Current.Load(this, GetType(), fileName)) {
                FileName = Path.GetFileName(fileName);
                if(Loaded != null)
                    Loaded(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        public bool LoadFromString(string text) {
            Clear();
            if(SerializationHelper.Current.LoadFromString(this, GetType(), text)) {
                if(Loaded != null)
                    Loaded(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        public void Clear() {
            Nodes.Clear();
            Connectors.Clear();
            Name = string.Empty;
        }

        void ISupportSerialization.OnBeginSerialize() { }
        void ISupportSerialization.OnEndSerialize() { }

        void ISupportSerialization.OnEndDeserialize() {
            DataDictionary.Clear();
            foreach(WfDataInfo info in Data) {
                DataDictionary.Add(info.GetType(), info);
            }
            foreach(var node in Nodes) {
                node.OwnerCollection = Nodes;
                node.OnEndDeserialize();
                RaiseQueryVisualData(node);
            }
            for(int i = 0; i < Connectors.Count; i++) {
                WfConnector conn = Connectors[i];

                conn.Document = this;
                conn.To = FindConnectionPoint(conn.ToId);
                if(conn.To != null)
                    conn.To.Connectors.Add(conn);
                conn.From = FindConnectionPoint(conn.FromId);
                if(conn.From != null)
                    conn.From.Connectors.Add(conn);
            }
            RemoveUnusedConnectors();
        }

        public T GetData<T>() where T: WfDataInfo, new() {
            WfDataInfo info = null;
            if(DataDictionary.TryGetValue(typeof(T), out info))
                return (T)info;

            info = new T();
            DataDictionary.Add(typeof(T), info);
            return (T)info;
        }

        public void RemoveUnusedConnectors() {
            for(int ni= 0; ni < Nodes.Count; ni++) {
                WfNode node = Nodes[ni];
                for(int pi = 0; pi < node.Points.Count; pi++) {
                    WfConnectionPoint point = node.Points[pi];
                    for(int i = 0; i < point.Connectors.Count;) {
                        if(point.Connectors[i].From == null || point.Connectors[i].To == null) {
                            Connectors.Remove(point.Connectors[i]);
                            point.Connectors.RemoveAt(i);
                        }
                        else i++;
                    }
                }
            }
        }

        void ISupportSerialization.OnBeginDeserialize() {
            Clear();
        }

        [Browsable(false)]
        public WfNodeCollection Nodes { get; private set; }

        [Browsable(false)]
        public WfDocumentConnectorCollection Connectors { get; private set; }

        protected void OnPropertyChanged(string name) {
            if(this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
            RaiseChanged();
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

        [Browsable(false)]
        public List<WfColor> ItemColors { get; set; }

        protected virtual void InitializeDefaultColors() {
            ItemColors = new List<WfColor>();

            //ItemColors.Add(WfColor.FromArgb(nameof(ScriptActionMessage), 255, 45, 105, 5));
            //ItemColors.Add(WfColor.FromArgb(nameof(ScriptActionWait), 255, 239, 241, 245));
            //ItemColors.Add(WfColor.FromArgb(nameof(ScriptSetPropertyAction), 255, 255, 198, 0));
            //ItemColors.Add(WfColor.FromArgb(nameof(ScriptInvokeMethodAction), 255, 9, 94, 159));
            //ItemColors.Add(WfColor.FromArgb(nameof(ScriptActionActivateScript), 255, 218, 12, 12));
        }

        public WfConnectionPoint FindConnectionPoint(Guid pointId) {
            if(pointId == Guid.Empty)
                return null;
            foreach(var node in Nodes) {
                WfConnectionPoint pt = node.Inputs.FirstOrDefault(i => i.Id == pointId);
                if(pt != null)
                    return pt;
                pt = node.Outputs.FirstOrDefault(i => i.Id == pointId);
                if(pt != null)
                    return pt;
            }
            return null;
        }

        public List<Type> GetAvailableNodeTypes() {
            Type nodeBase = typeof(WfNode);
            var res = SerializationHelper.Current.GetExtraTypes(GetType())
                .Where(t => !t.IsAbstract && nodeBase.IsAssignableFrom(t))
                .ToList();
            return res;
        }
        public List<WfNode> GetAvailableToolbarItems() {
            var types = GetAvailableNodeTypes().Where(t => IsToolboxVisible(t)).ToList();
            var res = types.Select(t => (WfNode)t.GetConstructor(new Type[] { }).Invoke(new object[] { })).ToList();
            return res;
        }

        private bool IsToolboxVisible(Type t) {
            var attr = t.GetCustomAttributes(typeof(WfToolboxVisibleAttribute), true);
            if(attr == null || attr.Length == 0)
                return true;
            return ((WfToolboxVisibleAttribute)attr[0]).Visible;
        }

        public event WfNodeEventHandler QueryNodeVisualData;

        public WfConnector AddConnector(WfConnector c) {
            Connectors.Add(c);
            c.Document = this;
            return c;
        }

        public virtual void Reset() {
            Diagnostics.Clear();
            DataDictionary.Clear();
            foreach(WfConnector connector in Connectors)
                connector.Reset();
            foreach(WfNode node in Nodes) {
                node.Reset();
            }
            if(Owner != null)
                Owner.OnReset(this);
        }

        public void RemoveNode(WfNode node) {
            List<WfConnector> input = node.GetInputConnectors();
            List<WfConnector> output = node.GetOutputConnectors();
            Nodes.Remove(node);
            input.ForEach(i => i.Detach());
            output.ForEach(o => o.Detach());
        }

        [XmlIgnore]
        public IWfDocumentResourcesProvider ResourcesProvider { get; set; }

        public event EventHandler Changed;
        
        protected internal virtual void RaiseChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }
    }

    public class WfToolboxVisibleAttribute : Attribute {
        public WfToolboxVisibleAttribute(bool visible) {
            Visible = visible;
        }
        public bool Visible { get; private set; }

    }

    public interface IWfDocumentResourcesProvider {
        public object GetNodeImage(WfNode node);
    }
}
