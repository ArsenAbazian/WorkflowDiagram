using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Drawing;
using System.Globalization;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public partial class WfDiagramView : IDisposable {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: dispose managed state (managed objects)

                    if(refThis == null)
                        return;

                    //if(refElement.Id != null)
                    //    _ = JsRuntimeHelper.SubscribeResizes(JSRuntime, refThis, refElement);

                    refThis.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~WfDiagramView()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        [CascadingParameter]
        public WfDiagramComponent Diagram { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        protected ElementReference refElement;
        private DotNetObjectReference<WfDiagramView> refThis;

        bool shouldRender = true;
        protected override bool ShouldRender() {
            if(shouldRender) {
                shouldRender = false;
                return true;
            }

            return false;
        }

        protected override void OnInitialized() {
            base.OnInitialized();
            if(Diagram != null) {
                Diagram.Viewport = this;
                Diagram.Changed += OnDiagramChanged;
            }
            this.refThis = DotNetObjectReference.Create(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            await base.OnAfterRenderAsync(firstRender);
            if(firstRender) {
                ViewportBounds = await JsRuntimeHelper.GetBoundingClientRect(JSRuntime, this.refElement);
                await JsRuntimeHelper.SubscribeResizes(JSRuntime, this.refThis, this.refElement);
            }
        }

        [JSInvokable]
        public void OnResize(RectangleF rect) => Diagram.Viewport.ViewportBounds = rect;

        protected virtual void OnDiagramChanged(object sender, EventArgs e) {
            shouldRender = true;
            StateHasChanged();
        }

        private void OnDiagramChanged() {
            shouldRender = true;
            StateHasChanged();
        }

        private void OnMouseMove(MouseEventArgs e) => Diagram.OnMouseMove(this, e);
        private void OnMouseDown(MouseEventArgs e) => Diagram.OnMouseDown(this, e);
        private void OnMouseUp(MouseEventArgs e) => Diagram.OnMouseUp(this, e);
        private void OnWheel(WheelEventArgs e) => Diagram.OnMouseWheel(this, e);

        private void OnKeyDown(KeyboardEventArgs e) => Diagram.OnKeyDown(this, e);

        private void OnTouchStart(TouchEventArgs e) => Diagram.OnTouchStart(this, e);
        private void OnTouchMove(TouchEventArgs e) => Diagram.OnTouchMove(this, e);
        private void OnTouchEnd(TouchEventArgs e) => Diagram.OnTouchEnd(this, e);

        private void OnDragEnd(DragEventArgs e) => Diagram.OnDragEnd(this, e);
        private void OnDragEnter(DragEventArgs e) => Diagram.OnDragEnter(this, e);
        private void OnDragOver(DragEventArgs e) => Diagram.OnDragOver(this, e);

        public RectangleF ViewportBounds { get; internal set; }

        public string ViewportInfoString {
            get {
                string x = (-Diagram.Origin.X).ToString(CultureInfo.InvariantCulture);
                string y = (-Diagram.Origin.Y).ToString(CultureInfo.InvariantCulture);
                string zoom = Diagram.ZoomFactor.ToString(CultureInfo.InvariantCulture);
                string res = string.Format($"transform: translate({x}px, {y}px) scale({zoom});");
                return res;
            }
        }

        public string GridLayerString { 
            get {
                string x = (-Diagram.Origin.X).ToString(CultureInfo.InvariantCulture);
                string y = (-Diagram.Origin.Y).ToString(CultureInfo.InvariantCulture);
                string zoom = Diagram.ZoomFactor.ToString(CultureInfo.InvariantCulture);
                string res = string.Format($"transform: translate({x}px, {y}px) scale({zoom});");
                return res;
            } 
        }

        public string OriginString {
            get { return string.Format(CultureInfo.InvariantCulture, "{0:0.0}, {1:0.0}", Diagram.Origin.X, Diagram.Origin.Y); }
        }

        public string ZoomFactorPercentString {
            get { return string.Format("{0:.##}", Diagram.ZoomFactorPercent); }
        }
    }
}
