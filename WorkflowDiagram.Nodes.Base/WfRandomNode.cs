using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram.Nodes.Base {
    public class WfRandomNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Random";

        public override string Type => "Random";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Value", Text = "Value", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected Random Random { get; set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            Random = new Random(DateTime.Now.Millisecond);
            return true;
        }

        [XmlIgnore]
        [Browsable(false)]
        public double Value { get; set; }
        protected override void OnVisitCore(WfRunner runner) {
            double nextValue = From + (Random.NextDouble() * (To - From));
            Value = nextValue;
            Outputs["Value"].Visit(runner, Value);
        }

        public double From { get; set; } = 0;
        public double To { get; set; } = 1;
    }
}
