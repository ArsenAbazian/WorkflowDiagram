using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public partial class ConnectorView {
        [Parameter]
        public ConnectorItem Connector { get; set; }

        protected internal void OnMouseDown(MouseEventArgs e) {
            Connector.Diagram.OnConnectorMouseDown(Connector, e);
        }

        protected internal virtual void OnKeyDown(KeyboardEventArgs e) {
            Connector.Diagram.OnKeyDown(this, e);
        }
    }
}
