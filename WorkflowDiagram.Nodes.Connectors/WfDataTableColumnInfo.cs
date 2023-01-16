using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfDataTableColumnInfo : INotifyPropertyChanged, ICloneable {
        public WfDataTableColumnInfo() {
            Id = Guid.NewGuid();
        }

        [Browsable(false)]
        public Guid Id { get; set; }

        protected void OnPropertyChanged(string name) {
            if(this.propertyChanged != null)
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
            if(Collection != null)
                Collection.Owner.OnColumnInfoChanged(this);
        }

        public object Clone() {
            return MemberwiseClone();
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
         
        string lowCaseName;
        [Browsable(false)]
        public string LowCaseName {
            get {
                if(lowCaseName == null && Name != null)
                    lowCaseName = Name.ToLower();
                return lowCaseName;
            }
        }

        string text;
        public string Text {
            get { return text; }
            set {
                if(Text == value)
                    return;
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        string defaultValue = "";
        public string DefaultValue {
            get { return defaultValue; }
            set {
                value = value == null ? "" : value.Trim();
                if(DefaultValue == value)
                    return;
                defaultValue = value;
                OnPropertyChanged(nameof(DefaultValue));
            }
        }

        bool isNullable = true;
        public bool IsNullable {
            get {
                if(PrimaryKey)
                    return false;
                return isNullable; 
            }
            set {
                if(PrimaryKey)
                    value = false;
                if(IsNullable == value)
                    return;
                isNullable = value;
                OnPropertyChanged(nameof(IsNullable));
            }
        }

        WfDataTableColumnType type = WfDataTableColumnType.Integer;
        public WfDataTableColumnType Type {
            get { return type; }
            set {
                if(Type == value)
                    return;
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        int length = 1;
        public int Length {
            get { return length; }
            set {
                if(Length == value)
                    return;
                length = value;
                OnPropertyChanged(nameof(Length));
            }
        }

        protected internal WfDataTableColumnInfoCollection Collection { get; internal set; }
        public override string ToString() {
            if(!string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Text))
                return string.Format("{0} [{1}]", Text, Name);
            return GetType().Name;
        }

        bool primaryKey = false;
        public bool PrimaryKey {
            get { return primaryKey; }
            set {
                if(PrimaryKey == value)
                    return;
                primaryKey = value;
                OnPrimaryKeyChanged();
            }
        }

        protected virtual void OnPrimaryKeyChanged() {
            if(PrimaryKey)
                IsNullable = false;
        }
    }

    public enum WfDataTableColumnType {
        Boolean,
        VarChar,
        Text,
        Integer,
        Integer64,
        Double,
        DateTime
    }
}
