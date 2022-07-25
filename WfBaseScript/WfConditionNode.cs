using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WfBaseScript {
    public class WfConditionNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Conditional";

        public override string Type => "Conditional";

        public override string Header => GetOperationDescription();

        private string GetOperationDescription() {
            switch(Operation) {
                case WfConditionalOperation.Equal:
                    return "In1 == In2";
                case WfConditionalOperation.NonEqual:
                    return "In1 != In2";
                case WfConditionalOperation.Less:
                    return "In1 < In2";
                case WfConditionalOperation.LessOrEqual:
                    return "In1 <= In2";
                case WfConditionalOperation.Greater:
                    return "In1 > In2";
                case WfConditionalOperation.GreaterOrEqual:
                    return "In1 >= In2";
            }
            return string.Empty;
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public override void OnVisit(WfRunner runner) {
            bool result = CalcOperation();
            DataContext = result;
            if(result) {
                Outputs["True"].OnVisit(runner, true);
                Outputs["False"].Value = null;
            }
            else {
                Outputs["False"].OnVisit(runner, false);
                Outputs["True"].Value = null;
            }
        }

        protected virtual bool CalcOperation() {
            object value1 = Inputs[0].Value;
            object value2 = Inputs[1].Value;

            switch(Operation) {
                case WfConditionalOperation.Equal:
                    return object.Equals(value1, value2);
                case WfConditionalOperation.NonEqual:
                    return !object.Equals(value1, value2);
                case WfConditionalOperation.Less:
                    return Convert.ToDouble(value1) < Convert.ToDouble(value2);
                case WfConditionalOperation.LessOrEqual:
                    return Convert.ToDouble(value1) <= Convert.ToDouble(value2);
                case WfConditionalOperation.Greater:
                    return Convert.ToDouble(value1) > Convert.ToDouble(value2);
                case WfConditionalOperation.GreaterOrEqual:
                    return Convert.ToDouble(value1) >= Convert.ToDouble(value2);
            }
            return false;
        }

        WfConditionalOperation operation;
        [Category("Operation")]
        public WfConditionalOperation Operation {
            get { return operation; }
            set {
                if(Operation == value)
                    return;
                operation = value;
                OnPropertyChanged(nameof(Operation));
            }
        }

        protected override bool AllowProcessOutput(WfConnectionPoint point) {
            if((bool)DataContext)
                return point.Name == "True";
            return point.Name == "False";
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In1", Text = "In1", Requirement = WfRequirementType.Mandatory },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In2", Text = "In2", Requirement = WfRequirementType.Mandatory }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "True", Text = "True", Value = true  },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "False", Text = "False", Value = false  }
            }.ToList();
        }
    }

    public enum WfConditionalOperation {
        [Description("==")]
        Equal,
        [Description("!=")]
        NonEqual,
        [Description("<")]
        Less,
        [Description("<=")]
        LessOrEqual,
        [Description(">")]
        Greater,
        [Description(">=")]
        GreaterOrEqual
    }
}
