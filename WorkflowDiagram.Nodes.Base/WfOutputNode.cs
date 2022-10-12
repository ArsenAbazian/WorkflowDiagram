using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfOutputNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Output";

        public override string Type => "Output";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Out", Text = "Out", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new List<WfConnectionPoint>();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            Value = Inputs[Inputs.Count - 1].Value;
            DataContext = Value;
        }

        object _value;
        [Category("Value"), PropertyEditor("WorkflowDiagram.UI.Win.Editors", "WorkflowDiagram.UI.Win.Editors.RepositoryItemObjectValueEditor")]
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
