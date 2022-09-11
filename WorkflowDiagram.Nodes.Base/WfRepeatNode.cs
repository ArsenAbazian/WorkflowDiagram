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
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Repeat", Text = "Repeat", SkipSubTree = true },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Index", Text = "Index", SkipSubTree = true },
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public int Count { get; set; }
        public int Start { get; set; } = 0;

        protected override void OnVisitCore(WfRunner runner) {
            object item = Inputs["In"].Value;
            WfObjectList result = new WfObjectList();
            int end = Start + Count;
            for(int i = Start; i < end; i++) {
                Outputs["Index"].Value = i; 
                Outputs["Repeat"].Value = item;
                object itemRes = null;
                
                if(runner.RunOnceSubTree(new WfConnectionPoint[] { Outputs["Index"] }, Outputs["Repeat"], out itemRes))
                    result.Add(itemRes);
            }
            DataContext = result;
            Outputs["Out"].Visit(runner, result);
        }
    }

    public class WfObjectList : List<object> {
        public override string ToString() {
            return "Items " + Count.ToString();
        }
    }
}
