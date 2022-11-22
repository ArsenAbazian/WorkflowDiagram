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
    [XmlInclude(typeof(WfDocument))]
    public class WfProject : ISupportSerialization, INotifyPropertyChanged {
        public WfProject() {
            Id = Guid.NewGuid();
            CreationTime = DateTime.Now;
        }

        public DateTime CreationTime { get; set; }

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

        public event EventHandler Changed;
        protected internal virtual void OnDocumentCollectionChanged() {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        string description;
        public string Description {
            get { return description; }
            set {
                if(Description == value)
                    return;
                description = value;
                OnPropertyChanged(nameof(Description));

            }
        }

        [Browsable(false)]
        public Guid Id { get; set; }
        [Browsable(false)]
        public string FileName { get; set; }

        public void OnBeginDeserialize() {
            
        }

        public void OnBeginSerialize() {
            Clear();
        }

        public void OnEndDeserialize() {
            
        }

        public void OnEndSerialize() {
            
        }

        public List<WfDocument> Documents { get; } = new List<WfDocument>();

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

        public event EventHandler Saved;
        public event EventHandler Loaded;

        public void Save() {
            if(string.IsNullOrEmpty(FullPath))
                return;
            Save(FullPath);
            if(Saved != null)
                Saved(this, EventArgs.Empty);
        }

        public void Save(string fullPath) {
            FileName = Path.GetFileName(fullPath);
            if(string.IsNullOrEmpty(FileName))
                return;
            string path = Path.GetDirectoryName(fullPath);
            Reset();
            SerializationHelper.Current.Save(this, GetType(), path);
        }

        public void Reset() {
            Documents.ForEach(d => d.Reset());
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

        public void Clear() {
            Documents.Clear();
            Name = string.Empty;
        }
    }
}
