using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public partial class ConnectorView {
        [Parameter]
        public ConnectorItem Connector { get; set; }

        internal void OnMouseDown(MouseEventArgs e) {
            Connector.Diagram.OnConnectorMouseDown(this, e);
        }
    }
}
