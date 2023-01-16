using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        protected internal void OnDocumentCursorChanged() {
            OnDiagramChanged();
        }

        public RectangleF ViewportBounds { get; internal set; }

        public string ViewportInfoString {
            get {
                string x = (-Diagram.Origin.X).ToString(CultureInfo.InvariantCulture);
                string y = (-Diagram.Origin.Y).ToString(CultureInfo.InvariantCulture);
                string zoom = Diagram.ZoomFactor.ToString(CultureInfo.InvariantCulture);
                //string transition = "";// EnableAnimation ? "transition: transform 0.1s;" : "";
                string res = string.Format($"transform: scale({zoom}) translate({x}px, {y}px);");
                return res;
            }
        }

        public string GridLayerString { 
            get {
                string x = (-Diagram.Origin.X).ToString(CultureInfo.InvariantCulture);
                string y = (-Diagram.Origin.Y).ToString(CultureInfo.InvariantCulture);
                string zoom = Diagram.ZoomFactor.ToString(CultureInfo.InvariantCulture);
                //string transition = "";// EnableAnimation ? "transition: transform 0.1s;" : "";
                string res = string.Format($"transform: scale({zoom}) translate({x}px, {y}px);");
                return res;
            } 
        }

        public string OriginString {
            get { return string.Format(CultureInfo.InvariantCulture, "{0:0.0}, {1:0.0}", Diagram.Origin.X, Diagram.Origin.Y); }
        }

        public string DocumentCursorString {
            get { return string.Format(CultureInfo.InvariantCulture, "{0:0.0}, {1:0.0}", Diagram.DocumentCursor.X, Diagram.DocumentCursor.Y); }
        }

        public string ZoomFactorPercentString {
            get { return string.Format("{0:.##}", Diagram.ZoomFactorPercent); }
        }

        public bool EnableAnimation { get; internal set; }

        List<GridLine> gridLines;
        public List<GridLine> GridLines { 
            get {
                //if(gridLines == null)
                    gridLines = CreateGridLines();
                return gridLines;
            } 
        }

        public double MinGridLineStep { get; set; } = 5;
        public double GridLineStep { get; set; } = 20;
        protected virtual List<GridLine> CreateGridLines() {
            double width = ViewportBounds.Width > 0? ViewportBounds.Width: 4000;
            double height = ViewportBounds.Height > 0? ViewportBounds.Height: 3000;

            int countX = (int)(width / MinGridLineStep + 0.5) + 2;
            int countY = (int)(height / MinGridLineStep + 0.5) + 2;
            List<GridLine> list = new List<GridLine>(countX + countY);

            PointF pt = Diagram.LocalToDocument(0.0f, 0.0f);
            int lineStartX = (int)(pt.X / GridLineStep) - 1;
            int lineStartY = (int)(pt.Y / GridLineStep) - 1;

            PointF start = Diagram.FromDocument(new PointF((float)(lineStartX * GridLineStep), (float)(lineStartY * GridLineStep)));
            double step = GridLineStep * Diagram.ZoomFactor;
            while(step < MinGridLineStep)
                step += GridLineStep * Diagram.ZoomFactor;
            
            double x = start.X;
            double y = start.Y;
            for(int i = 0; i < countX; i++, x += step) {
                list.Add(new GridLine() { X1 = (int)x, Y1 = (int)(start.Y - step), X2 = (int)x, Y2 = (int)height });
            }
            for(int i = 0; i < countY; i++, y += step) {
                list.Add(new GridLine() { X1 = (int)(start.X -  step), Y1 = (int)y, X2 = (int)width, Y2 = (int)y });
            }
            return list;
        }
    }

    public class GridLine {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
    }
}
