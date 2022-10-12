using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Drawing;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public class ConnectionPointItem : ComponentBase, IDisposable {

        protected ElementReference element;
        protected DotNetObjectReference<ConnectionPointItem> refThis;

        [CascadingParameter]
        public WfDiagramComponent Diagram { get; set; }

        [Parameter]
        public WfConnectionPoint Point { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        public RectangleF Bounds { get; set; }

        bool shouldRender = true;
        protected override bool ShouldRender() {
            if(shouldRender) {
                shouldRender = false;
                return true;
            }
            return false;
        }

        protected void Refresh() {
            this.shouldRender = true;
            StateHasChanged();
        }

        public void Dispose() {
            if(Point != null)
                Point.PropertyChanged -= OnPointChanged;

            if(this.element.Id != null)
                _ = JsRuntimeHelper.SubscribeResizes(JsRuntime, this.refThis, this.element);

            this.refThis?.Dispose();
        }

        protected void OnPointChanged(object sender, PropertyChangedEventArgs e) {
            Refresh();
        }

        protected override void OnInitialized() {
            base.OnInitialized();

            this.refThis = DotNetObjectReference.Create(this);
            if(Point != null)
                Point.PropertyChanged += OnPointChanged;
            Diagram.PointItems[Point] = this;
        }

        public void Move(float dx, float dy) {
            var r = Bounds;
            r.Offset(dx, dy);
            Bounds = r;
            UpdateBoundsAsync();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            var viewType = Diagram.CreateViewTypeFor(this);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "point");
            builder.AddAttribute(2, "point-id", Point.Id);

            builder.AddElementReferenceCapture(13, value => this.element = value);
            builder.OpenComponent(4, viewType);
            builder.AddAttribute(5, "Point", this);
            builder.CloseComponent();
            builder.CloseElement();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            await base.OnAfterRenderAsync(firstRender);

            await UpdateBoundsAsync().ContinueWith(t => {
                UpdateConnectorsPositions();
            });
            //RectangleF r = await JsRuntimeHelper.GetBoundingClientRect(JsRuntime, this.element);
            //r.Offset(-Diagram.Viewport.ViewportBounds.X, -Diagram.Viewport.ViewportBounds.Y);
            //Bounds = r;
            //if(firstRender || _becameVisible) {
            //    _becameVisible = false;
            //    await JsRuntime.ObserveResizes(_element, _reference);
            //}
        }

        private void UpdateConnectorsPositions() {
            //foreach(WfConnector c in Point.Connectors) {
            //    ConnectorItem c Diagram.GetConnector(c)
            //        if(Point.Type == WfConnectionPointType.In)
            //}
        }

        internal Task UpdateBoundsAsync() {
            return JsRuntimeHelper.GetBoundingClientRect(JsRuntime, this.element).ContinueWith(t => {
                RectangleF r = t.Result;
                //r.Offset(- Diagram.Origin.X, - Diagram.Origin.Y);
                //r = r.Scale(1 / Diagram.ZoomFactor);
                r.Offset(-Diagram.Viewport.ViewportBounds.X, -Diagram.Viewport.ViewportBounds.Y);
                Bounds = r;
            });
        }
    }
}
