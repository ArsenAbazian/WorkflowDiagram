using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Drawing;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public partial class NodeView {
        [Parameter]
        public NodeItem Node { get; set; }

        protected internal virtual void OnMouseDown(MouseEventArgs e) {
            Node.Diagram.OnNodeMouseDown(this, e);
        }

        protected internal virtual void OnKeyDown(KeyboardEventArgs e) {
            Node.Diagram.OnKeyDown(this, e);
        }

        protected internal virtual void OnLeftResizingAreaMouseDown(MouseEventArgs e) {
            Node.Diagram.OnLeftResizingAreaMouseDown(this, e);
        }
        protected internal virtual void OnRightResizingAreaMouseDown(MouseEventArgs e) {
            Node.Diagram.OnRightResizingAreaMouseDown(this, e);
        }
        protected internal virtual void OnResizingAreaMouseUp(MouseEventArgs e) {
            Node.Diagram.OnResizingAreaMouseUp(this, e);
        }
    }
}
