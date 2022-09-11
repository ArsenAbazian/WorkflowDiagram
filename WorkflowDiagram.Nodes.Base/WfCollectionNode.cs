using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfCollectionNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Collection";

        public override string Type => "Collection";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "ItemIn", Text = "Item", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Collection", Text = "Collection", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Collection", Text = "Collection", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "ItemOut", Text = "Item", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        public List<object> Collection = new List<object>();
        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            object item = Inputs["ItemIn"].Value;
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
            Outputs["Collection"].Visit(runner, list);
            if(item != null)
                Outputs["ItemOut"].Visit(runner, item);
            else
                Outputs["ItemOut"].SkipVisit(runner, item);
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

    //public class TableList : List<object>, ITypedList {
    //    PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors) {
    //        PropertyDescriptorCollection pdc = new PropertyDescriptorCollection();
    //    }

    //    string ITypedList.GetListName(PropertyDescriptor[] listAccessors) {
    //        return typeof(TableList).Name;
    //    }
    //}

    //internal class SmartPropertyDescriptor : PropertyDescriptor {
    //    public override Type ComponentType => typeof(Dictionary<string, object>);

    //    public override bool IsReadOnly => throw new NotImplementedException();

    //    public override Type PropertyType => throw new NotImplementedException();

    //    public override bool CanResetValue(object component) {
    //        throw new NotImplementedException();
    //    }

    //    public override object GetValue(object component) {
    //        throw new NotImplementedException();
    //    }

    //    public override void ResetValue(object component) {
    //        throw new NotImplementedException();
    //    }

    //    public override void SetValue(object component, object value) {
    //        throw new NotImplementedException();
    //    }

    //    public override bool ShouldSerializeValue(object component) {
    //        throw new NotImplementedException();
    //    }
    //}
}
