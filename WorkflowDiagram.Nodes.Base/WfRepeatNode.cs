using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfRepeatNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Repeat";

        public override string Type => "Repeat";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In", Text = "In", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Out", Text = "Out" },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Repeat", Text = "Repeat", SkipSubTree = true }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public int Count { get; set; }

        protected override void OnVisitCore(WfRunner runner) {
            object item = Inputs["In"].Value;
            List<object> result = new List<object>();
            for(int i = 0; i < Count; i++) {
                Outputs["Repeat"].Value = item;
                object itemRes = null;
                if(runner.RunOnceSubTree(Outputs["Repeat"], out itemRes))
                    result.Add(itemRes);
            }
            DataContext = result;
            Outputs["Out"].OnVisit(runner, result);
        }
    }
}
