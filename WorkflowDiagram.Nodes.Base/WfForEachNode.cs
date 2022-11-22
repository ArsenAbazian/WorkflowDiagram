using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfForEachNode : WfVisualNodeBase {
        public override string VisualTemplateName => "ForEach";

        public override string Type => "For Each";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In", Text = "In" }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Out", Text = "Out" },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "ForEach", Text = "ForEach", SkipSubTree = true }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            IEnumerable en = Inputs["In"].Value as IEnumerable;
            if(en == null) {
                DataContext = en;
                Outputs["Out"].Visit(runner, en);
                return;
            }
            DataContext = en;
            List<object> result = new List<object>();
            foreach(object item in en) {
                Outputs["ForEach"].Value = item;
                object resultObject = null;
                if(runner.RunOnceSubTree(Outputs["ForEach"], out resultObject))
                    result.Add(resultObject); 
            }
            DataContext = result;
            Outputs["Out"].Visit(runner, result);
        }
    }
}
