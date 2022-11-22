using Microsoft.AspNetCore.Components.Web;
using System.Drawing;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public partial class ToolboxItemView {
        protected internal void OnDragStart(DragEventArgs e) {
            Item.Diagram.DraggedItem = Item;
            e.DataTransfer.EffectAllowed = "copy";
            e.DataTransfer.Items = new DataTransferItem[1] { new DataTransferItem() { Kind = "string", Type = Item.Node.Id.ToString() } };
        }

        protected internal void OnDragEnd(DragEventArgs e) {
            if(e.DataTransfer.DropEffect == "copy") {
                PointF pt = Item.Diagram.ToDocument((float)e.PageX, (float)e.PageY);
                WfNode node = Item.Node.Clone();
                node.X = pt.X;
                node.Y = pt.Y;
                node.Width = 250;
                Item.Diagram.Document.AddNode(node);
            }
            Item.Diagram.DraggedItem = null;
        }

        protected PointF DownPoint { get; set; }
        protected internal void OnMouseDown(MouseEventArgs e) {
            DownPoint = new PointF((float)e.OffsetX, (float)e.OffsetY);
        }
    }
}
