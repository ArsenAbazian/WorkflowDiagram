using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Drawing;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public partial class ConnectionPointView {
        [Parameter]
        public ConnectionPointItem Point { get; set; }

        internal void OnMouseDown(MouseEventArgs e) {
            Point.Diagram.OnConnectionPointMouseDown(this, e);
        }
        internal void OnMouseUp(MouseEventArgs e) {
            Point.Diagram.OnConnectionPointMouseUp(this, e);
        }
        internal void OnMouseMove(MouseEventArgs e) {
            Point.Diagram.HotPoint = this;
        }
        internal void OnMouseOut(MouseEventArgs e) {
            Point.Diagram.HotPoint = null;
        }
    }
}
