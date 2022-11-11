using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram.Nodes.Base;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfWebSocketServerNode : WfVisualNodeBase {
        public override string VisualTemplateName => "WebSocketServer";

        public override string Type => "WebSocket Server";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Server", Text = "Server" }
            }.ToList();
        }

        protected TcpListener TcpListener { get; set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            return CheckCreateTcpListener();
        }

        protected virtual bool CheckCreateTcpListener() {
            if(TcpListener != null)
                TcpListener.Stop();
            try {
                string ipString = GetActualIpString();
                TcpListener = new TcpListener(IPAddress.Parse(ipString), Port);
                TcpListener.Start(MaxConnections);
            }
            catch(Exception e) {
                DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "Error creating websocket server. " + e.ToString());
                HasErrors = true;
            }
            return true;
        }

        protected virtual string GetActualIpString() {
            if(string.IsNullOrEmpty(IpAddress))
                return "127.0.0.1";
            return IpAddress;
        }

        protected override void OnVisitCore(WfRunner runner) {
            Outputs[0].Visit(runner, this);
        }

        public string IpAddress { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 80;
        public int MaxConnections { get; set; } = int.MaxValue;
    }
}
