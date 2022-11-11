using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfInputNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Input";

        public override string Type => "Input";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "In", Text = "In", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            DataContext = Value;
            Outputs[0].Visit(runner, Value);
        }

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
                OnPropertyChanged(nameof(Value));
            }
        }

        protected override bool AllowAddRunPoint => false;
    }
}
