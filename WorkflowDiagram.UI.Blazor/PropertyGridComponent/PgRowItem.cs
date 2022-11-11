using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System.Drawing;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public class PgRowItem : ComponentBase {
        public PgRowItem() {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        [Parameter]
        public PropertyGridRowBase Row { get; set; }

        [Parameter]
        public PropertyGridView View { get; set; }

        [Parameter]
        public string Class { get; set; }

        protected ElementReference element;
        protected DotNetObjectReference<PgRowItem> refThis;

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
           this.refThis?.Dispose();
        }

        protected override void OnInitialized() {
            base.OnInitialized();
            this.refThis = DotNetObjectReference.Create(this);
        }
        

        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            var viewType = PropertyGrid.CreateViewTypeFor(this);
            builder.OpenElement(0, "div");
            builder.AddAttribute(2, "row-id", Row.Id);

            builder.AddElementReferenceCapture(13, value => this.element = value);
            builder.OpenComponent(4, viewType);
            builder.AddAttribute(5, "Row", this);
            builder.CloseComponent();
            builder.CloseElement();
        }

        static readonly string ExpandedStyle = "pg-expanded";
        static readonly string CollapedStyle = "pg-collapsed";
        public string ExpandButtonStyle { get { return Row.Expanded ? ExpandedStyle : CollapedStyle; } }

        public string CellTextStyle { get { return string.Format("width: {0};", View.TextAreaWidth); } }

        //protected override async Task OnAfterRenderAsync(bool firstRender) {
        //    await base.OnAfterRenderAsync(firstRender);

        //    if(firstRender || _becameVisible) {
        //        _becameVisible = false;
        //        await JsRuntime.ObserveResizes(_element, _reference);
        //    }
        //}
    }
}
