using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfIndexerNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Indexer";

        public override string Type => "Indexer";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In", Text = "In" },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Index", Text = "Index" }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Item", Text = "Item", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public int Index { get; set; }
        public WfIndex Access { get; set; } = WfIndex.Index;
        protected override void OnVisitCore(WfRunner runner) {
            IList list = Inputs["In"].Value as IList;
            if(list == null) {
                DataContext = null;
                Outputs["Item"].SkipVisit(runner, null);
                return;
            }
            int index = GetIndex();
            DataContext = list[index];
            Outputs["Item"].Visit(runner, list[index]);
        }

        private int GetIndex() {
            if(Inputs["Index"].Value == null) {
                IList list = Inputs["In"].Value as IList;
                if(Access == WfIndex.First)
                    return 0;
                if(Access == WfIndex.Last)
                    return list.Count - 1;
                return Index;
            }
            return Convert.ToInt32(Inputs["Index"].Value);
        }
    }

    public enum WfIndex {
        Index,
        First,
        Last,
    }
}
