using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WorkflowDiagram.Nodes.Base {
    public class WfCommentNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Comment";

        public override string Type => "Comment";
        public override string Header { get => ""; }

        public WfCommentNode() {
            Text = "This is comment.";
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            Outputs["Out"].Visit(runner, null);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "In", Text = "", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Out", Text = "", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override bool AllowAddRunPoint => false;
    }
}
    