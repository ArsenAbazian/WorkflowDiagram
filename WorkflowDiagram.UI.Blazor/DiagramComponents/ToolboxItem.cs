using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public class ToolboxItem : NodeItem {
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            var viewType = typeof(ToolboxItemView);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "item");
            builder.AddAttribute(2, "item-id", Item.Id);
            builder.AddElementReferenceCapture(13, value => this.element = value);
            builder.OpenComponent(4, viewType);
            builder.AddAttribute(5, "Item", this);
            builder.CloseComponent();
            builder.CloseElement();
        }

        [Parameter]
        public WfNode Item { get { return Node; } set { Node = value; } }
    }
}
