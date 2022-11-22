using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram.Nodes.Base {
    public class WfCounterNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Counter";

        public override string Type => "Counter";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Value", Text = "Value", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            Value = From;
            return true;
        }

        [XmlIgnore]
        [Browsable(false)]
        public double Value { get; set; }
        [XmlIgnore]
        [Browsable(false)]
        public int Direction { get; set; } = 1;
        protected override void OnVisitCore(WfRunner runner) {
            Value += Delta * Direction;
            if(Value >= To) {
                if(CountType == WfCountType.Circle)
                    Value = From;
                else if(CountType == WfCountType.PingPong) {
                    Value = To;
                    Direction = -1;
                }
            }
            else if(Value <= From) {
                if(CountType == WfCountType.Circle)
                    Value = To;
                else if(CountType == WfCountType.PingPong) {
                    Value = From;
                    Direction = +1;
                }
            }
            Outputs["Value"].Visit(runner, Value);
        }

        public double From { get; set; } = 0;
        public double To { get; set; } = double.MaxValue;
        public double Delta { get; set; } = 1.0;
        public WfCountType CountType { get; set; } = WfCountType.Default;

    }
    public enum WfCountType {
        Default,
        PingPong,
        Circle
    }
}
