using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System.Drawing;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public class PgValueItem : ComponentBase {
        public PgValueItem() {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        [Parameter]
        public PropertyGridValueInfo Value { get; set; }

        [Parameter]
        public string Class { get; set; }

        protected ElementReference element;
        protected DotNetObjectReference<PgValueItem> refThis;

        [CascadingParameter]
        public PropertyGridComponent PropertyGrid { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        bool shouldRender = true;
        protected override bool ShouldRender() {
            if(shouldRender) {
                shouldRender = false;
                return true;
            }
            return false;
        }

        public void Refresh() {
            this.shouldRender = true;
            StateHasChanged();
        }

        public void Dispose() {
            if(this.element.Id != null)
                _ = JsRuntimeHelper.SubscribeResizes(JsRuntime, this.refThis, this.element);

            this.refThis?.Dispose();
        }

        protected override void OnInitialized() {
            base.OnInitialized();
            this.refThis = DotNetObjectReference.Create(this);
        }
        

        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            var viewType = PropertyGrid.CreateViewTypeFor(this);
            if(viewType == null) {
                base.BuildRenderTree(builder);
                return;
            }
            builder.OpenElement(0, "div");
            builder.AddAttribute(2, "value-id", Id);

            builder.AddElementReferenceCapture(13, value => this.element = value);
            builder.OpenComponent(4, viewType);
            builder.AddAttribute(5, "Item", this);
            builder.CloseComponent();
            builder.CloseElement();
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender) {
        //    await base.OnAfterRenderAsync(firstRender);

        //    if(firstRender || _becameVisible) {
        //        _becameVisible = false;
        //        await JsRuntime.ObserveResizes(_element, _reference);
        //    }
        //}
    }
}
