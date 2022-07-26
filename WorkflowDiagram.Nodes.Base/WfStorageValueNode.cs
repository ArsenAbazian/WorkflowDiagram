using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base.Editors;
using System.Xml.Serialization;

namespace WorkflowDiagram.Nodes.Base {
    [XmlInclude(typeof(WfStorageDataInfo))]
    public class WfStorageValueNode : WfVisualNodeBase {
        public override string VisualTemplateName => "StorageValue";

        public override string Type => "Storage";
        public override string Header { get => ValueName; }

        protected override bool OnInitializeCore(WfRunner runner) {
            if(string.IsNullOrEmpty(ValueName)) {
                Diagnostic.Add(new WfDiagnosticInfo() { Type = WfDiagnosticSeverity.Error, Text = "Empty name for storage value is not allowed. Please specify unique name" });
                return false;
            }
            if(!HasInputConnections)
                SetValueToStorage(InitializeValue);
            return true;
        }

        public override void OnVisit(WfRunner runner) {
            Outputs[0].OnVisit(runner, GetValueFromStorage());
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Set", Text = "Set", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Get", Text = "Get",  }
            }.ToList();
        }

        string _name;
        [Category("Value")]
        public string ValueName {
            get { return _name; }
            set {
                if(value != null)
                    value = value.Trim();
                if(object.Equals(ValueName, value))
                    return;
                _name = value;
                OnPropertyChanged(nameof(ValueName));
            }
        }

        [Category("Value")]
        [Browsable(false)]
        public WfValueType ValueType { get; set; }
        object _value;
        [Category("Value"), PropertyEditor(typeof(RepositoryItemObjectValueEditor))]
        public object InitializeValue {
            get { return _value; }
            set {
                if(object.Equals(InitializeValue, value))
                    return;
                _value = value;
                OnPropertyChanged(nameof(InitializeValue));
            }
        }

        public object GetValueFromStorage() {
            return Document.GetData<WfStorageDataInfo>().GetStorageValue(ValueName);
        }
        public void SetValueToStorage(object value) {
            Document.GetData<WfStorageDataInfo>().SetStorageValue(ValueName, value);
        }
    }

    public class WfStorageSetConnectionPoint : WfConnectionPoint {
        public override void OnVisit(WfRunner runner, object value) {
            base.OnVisit(runner, value);
            WfStorageValueNode node = (WfStorageValueNode)Node;
            node.SetValueToStorage(Value);
        }
    }

    public class WfStorageDataInfo : WfDataInfo {
        public override string Name => "Storage";
        protected override object CreateData() {
            return new Dictionary<string, object>();
        }
        [XmlIgnore]
        public Dictionary<string, object> Storage { get { return (Dictionary<string, object>)Data; } }

        internal object GetStorageValue(string valueName) {
            object res = null;
            if(string.IsNullOrEmpty(valueName))
                return null;
            if(Storage.TryGetValue(valueName, out res))
                return res;
            return null;
        }

        internal void SetStorageValue(string valueName, object value) {
            Storage[valueName] = value;
        }
    }
}
