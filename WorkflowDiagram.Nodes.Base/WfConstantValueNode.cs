using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WorkflowDiagram.Nodes.Base {
    public class WfConstantValueNode : WfVisualNodeBase {
        public override string VisualTemplateName => "ConstantValue";

        public override string Type => "Constant";
        public override string Header { get => Convert.ToString(GetValue()); }

        public WfConstantValueNode() { }
        public WfConstantValueNode(double value) {
            ConstantType = WfValueType.Decimal;
            Value = value;
        }
        public WfConstantValueNode(bool value) {
            ConstantType = WfValueType.Boolean;
            Value = value;
        }

        public WfConstantValueNode(string value) {
            ConstantType = WfValueType.String;
            Value = value;
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            Outputs[0].Visit(runner, Value);
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
        [Category("Value"), 
            WinPropertyEditor("WorkflowDiagram.UI.Win.Editors", "WorkflowDiagram.UI.Win.Editors.RepositoryItemObjectValueEditor"),
            BlazorPropertyEditor("WorkflowDiagram.UI.Blazor", "WorkflowDiagram.UI.Blazor.NodeEditors.ObjectValueEditor")]
        public object Value {
            get { return _value; }
            set {
                if(object.Equals(Value, value))
                    return;
                _value = value;
                OnValueChanged();
            }
        }

        protected virtual void OnValueChanged() {
            UpdateConstantType();
            OnPropertyChanged(nameof(Value));
        }

        protected virtual void UpdateConstantType() {
            if(Value == null)
                ConstantType = WfValueType.Decimal;
            else if(Value is double || Value is float || Value is int)
                ConstantType = WfValueType.Decimal;
            else if(Value is string)
                ConstantType = WfValueType.String;
            else if(Value is DateTime)
                ConstantType = WfValueType.DateTime;
        }

        public object GetValue() {
            return Value;
        }
    }

    public enum WfValueType {
        Decimal,
        Integer,
        Integer64,
        Boolean,
        String,
        DateTime
    }
}
