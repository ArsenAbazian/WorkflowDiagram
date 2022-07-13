using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagramApp.Editors;

namespace WorkflowDiagramApp.StrategyDocument {
    public class WfStrategyConstantValue : WfStrategyNodeBase {
        public override string VisualTemplateName => "ConstantValue";

        public override string Type => "Constant";
        public override string Header { get => Convert.ToString(GetValue()); }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public override void OnVisit(WfRunner runner) {
            Outputs[0].Value = Value;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Value", Text = "Value",  }
            }.ToList();
        }

        [Category("Value")]
        [Browsable(false)]
        public WfValueType ConstantType { get; set; }
        object _value;
        [Category("Value"), PropertyEditor(typeof(RepositoryItemObjectValueEditor))]
        public object Value {
            get { return _value; }
            set {
                if(object.Equals(Value, value))
                    return;
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public object GetValue() {
            return Value;
        }
    }

    public enum WfValueType {
        Decimal,
        Boolean,
        String
    }
}
