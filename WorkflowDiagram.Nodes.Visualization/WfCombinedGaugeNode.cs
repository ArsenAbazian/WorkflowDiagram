using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization {
    public class WfCombinedGaugeNode : WfGaugeNode {
        public override string VisualTemplateName => "Combined Gauge";
        public override string Type => "Combined Gauge";

        protected override WfConnectionPointCollection CreateInputCollection() {
            var res = base.CreateInputCollection();
            res.AllowedOperations |= WfEditOperation.Add | WfEditOperation.Edit | WfEditOperation.Remove;
            return res;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Gauge1", Text = "Gauge1", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        public override WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type) {
            var res = base.CreateConnectionPoint(type);
            if(type == WfConnectionPointType.In) {
                res.Name = "Gauge" + Inputs.Count;
                res.Text = "Gauge" + Inputs.Count;
            }
            return res;
        }

        protected override void OnVisitCore(WfRunner runner) {
            if(Gauges.Count == 0) {
                for(int i = 1; i < Inputs.Count; i++)
                    Gauges.Add(Inputs[i].Value as WfGaugeNode);
            }
            DataContext = this;
            Outputs["Gauge"].Visit(runner, this);
            Progress.Report(null);
        }

        protected internal override bool IsCombined { get { return true; } }

        protected internal override void UpdateValue() {
            //base.UpdateValue();
        }
    }
}
