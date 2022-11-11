using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram.Nodes.Base;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfWebSocketClientNode : WfVisualNodeBase {
        public override string VisualTemplateName => "WebSocketClient";

        public override string Type => "WebSocket Client";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Client", Text = "Client" }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            Outputs[0].Visit(runner, null);
        }
    }
}
