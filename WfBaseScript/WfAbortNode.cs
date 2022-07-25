using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WfBaseScript {
    public class WfAbortNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Abort";

        public override string Type => "Abort";

        public bool Success { get; set; }

        public override void OnVisit(WfRunner runner) {
            Outputs[0].Value = Success;
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
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }
    }
}
