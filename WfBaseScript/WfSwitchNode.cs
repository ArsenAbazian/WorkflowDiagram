using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WfBaseScript.Editors;

namespace WfBaseScript {
    public class WfSwitchNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Switch";

        public override string Type => "Switch";

        public override string Header => "";

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public override void OnVisit(WfRunner runner) {
            object result = CalcOperation();
            DataContext = result;
            bool foundCase = false;
            for(int i = 0; i < Outputs.Count; i++) {
                if(Outputs[i].Name == "Default")
                    continue;
                if(object.Equals(result, Outputs[i].Value)) {
                    Outputs[i].OnVisit(runner, result);
                    foundCase = true;
                }
                else
                    Outputs[i].OnVisit(runner, null);
            }
            if(!foundCase)
                Outputs["Default"].OnVisit(runner, result);
            else
                Outputs["Default"].OnVisit(runner, null);
        }

        protected virtual object CalcOperation() {
            return Inputs[0].Value;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In", Text = "In", Requirement = WfRequirementType.Mandatory }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Default", Text = "Default" },
            }.ToList();
        }

        protected override WfConnectionPointCollection CreateOutputCollection() {
            WfConnectionPointCollection res = base.CreateOutputCollection();
            res.AllowedOperations |= WfEditOperation.Add | WfEditOperation.Edit | WfEditOperation.Remove;
            return res;
        }

        public override WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type) {
            WfConnectionPoint pt = new WfSwitchOutputConnectionPoint();
            pt.Name = "Out" + (Outputs.Count - 1);
            pt.Text = pt.Name;
            pt.AllowedOperations = WfEditOperation.Edit | WfEditOperation.Remove;
            return pt;
        }
    }

    public class WfSwitchOutputConnectionPoint : WfConnectionPoint {
        [PropertyEditor(typeof(RepositoryItemObjectValueEditor))]
        public override object Value { get => base.Value; set => base.Value = value; }
    }
}
