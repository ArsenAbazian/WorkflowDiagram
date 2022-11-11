using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent.ValueCellViews {
    public class PgCustomValueView : ComponentBase, IDisposable {
        private bool disposedValue;

        protected ElementReference element;
        protected DotNetObjectReference<PgCustomValueView> refThis;

        [Parameter]
        public PgValueItem Item { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            var viewType = Item.Value.GetCustomEditorType();
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "custom-value");
            builder.AddAttribute(2, "value-id", Item.Id);

            builder.AddElementReferenceCapture(13, value => this.element = value);
            builder.OpenComponent(4, viewType);
            builder.AddAttribute(5, "View", this);
            builder.CloseComponent();
            builder.CloseElement();
        }

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    this.refThis?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
