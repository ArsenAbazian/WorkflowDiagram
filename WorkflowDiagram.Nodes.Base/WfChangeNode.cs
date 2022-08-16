using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WorkflowDiagram.Nodes.Base {
    public class WfChangeNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Change";

        public override string Type => "When Changed";

        public override string Header => "";

        protected object PreviousValue { get; set; }

        protected override bool OnInitializeCore(WfRunner runner) {
            PreviousValue = null;
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            DataContext = Inputs["In1"].Value;
            bool changed = !object.Equals(DataContext, PreviousValue);
            if(changed) {
                Outputs["Yes"].OnVisit(runner, DataContext);
                Outputs["No"].OnSkipVisit(runner, null);
            }
            else {
                Outputs["No"].OnVisit(runner, DataContext);
                Outputs["Yes"].OnSkipVisit(runner, null);
            }
        }

        protected override bool AllowProcessOutput(WfConnectionPoint point) {
            if((bool)DataContext)
                return point.Name == "Yes";
            return point.Name == "No";
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In1", Text = "In", Requirement = WfRequirementType.Mandatory },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Yes", Text = "Yes", Value = true  },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "No", Text = "No", Value = false  }
            }.ToList();
        }
    }
}
