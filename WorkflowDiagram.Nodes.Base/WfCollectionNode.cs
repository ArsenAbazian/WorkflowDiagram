using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfCollectionNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Collection";

        public override string Type => "Collection";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Item", Text = "Item", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Collection", Text = "Collection", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Collection", Text = "Collection", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Item", Text = "Item", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        public List<object> Collection = new List<object>();
        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            object item = Inputs["Item"].Value;
            List<object> list = GetActualCollection();
            
            switch(Operation) {
                case WfCollectionOperation.AddLast:
                    if(!list.Contains(item))
                        list.Add(item);
                    break;
                case WfCollectionOperation.AddFirst:
                    if(!list.Contains(item))
                        list.Insert(0, item);
                    break;
                case WfCollectionOperation.Clear:
                    list.Clear();
                    break;
                case WfCollectionOperation.Remove:
                    if(list.Contains(item))
                        list.Remove(item);
                    break;
            }
            DataContext = list;
            Outputs["Collection"].OnVisit(runner, list);
            if(item != null)
                Outputs["Item"].OnVisit(runner, item);
            else
                Outputs["Item"].OnSkipVisit(runner, item);
        }

        private List<object> GetActualCollection() {
            List<object> inList = Inputs["Collection"].Value as List<object>;
            return inList == null ? Collection : inList;
        }

        public WfCollectionOperation Operation { get; set; } = WfCollectionOperation.AddLast;
    }

    public enum WfCollectionOperation {
        AddLast,
        AddFirst,
        Remove,
        Clear
    }
}
