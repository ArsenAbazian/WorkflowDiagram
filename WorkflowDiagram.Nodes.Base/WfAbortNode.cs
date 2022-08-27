using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WorkflowDiagram.Nodes.Base {
    public class WfAbortNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Abort";

        public override string Type => "Abort";

        public bool Success { get; set; }

        protected override void OnVisitCore(WfRunner runner) {
            Outputs[0].Value = Success;
            Outputs["Result"].Visit(runner, Inputs["In"].Value);
            runner.Success = Success;
            runner.Stop();
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In", Text = "In", Requirement = WfRequirementType.Mandatory },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "ResultCode", Text = "Result Code", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Result", Text = "Result", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }
    }
}
