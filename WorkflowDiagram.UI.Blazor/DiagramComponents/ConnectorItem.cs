using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System.Drawing;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public class ConnectorItem : ComponentBase, IDiagramItem {
        [Parameter]
        public WfConnector Connector { get; set; }

        [Parameter]
        public string Class { get; set; }

        bool selected;
        public bool Selected {
            get { return selected; }
            set {
                if(Selected == value)
                    return;
                selected = value;
                OnSelectedChanged();
            }
        }

        static readonly string DefaultSelectionClass = "";
        static readonly string SelectedClass = "connector-item-selected";

        [Parameter]
        public string SelectionClass { get; set; } = DefaultSelectionClass;

        protected virtual void OnSelectedChanged() {
            Diagram.SelectedItemChanged(this);
            SelectionClass = Selected ? SelectedClass : DefaultSelectionClass;
            Refresh();
        }

        public Point Start { get; set; }
        public Point End { get; set; }

        protected ElementReference element;
        protected DotNetObjectReference<ConnectorItem> refThis;

        [CascadingParameter]
        public WfDiagramComponent Diagram { get; set; }

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
            Diagram.ConnectorItems[Connector] = this;

            ConnectionPointItem startPoint = Diagram.GetConnectionPointItem(Connector.From);
            ConnectionPointItem endPoint = Diagram.GetConnectionPointItem(Connector.To);

            if(startPoint == null && endPoint == null)
                return;

            Start = startPoint == null? endPoint.Bounds.GetCenter(): startPoint.Bounds.GetCenter();
            End = endPoint == null ? startPoint.Bounds.GetCenter() : endPoint.Bounds.GetCenter();
        }


        public void MoveStart(float dx, float dy) {
            var p = Start;
            p.Offset((int)dx, (int)dy);
            Start = p;
            Refresh();
        }

        public void MoveEnd(float dx, float dy) {
            var p = End;
            p.Offset((int)dx, (int)dy);
            End = p;
            Refresh();
        }

        public void Move(float dx, float dy) {
            MoveStart(dx, dy);
            MoveEnd(dx, dy);
        }

        protected Point GetCenterPoint(RectangleF bounds) {
            int x = (int)(bounds.X + bounds.Width / 2);
            int y = (int)(bounds.Y + bounds.Height / 2);
            return new Point(x, y);
        }

        public float Strength { get { return 0.5f; } }

        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            var viewType = Diagram.CreateViewTypeFor(this);
            builder.OpenElement(0, "path");
            builder.AddAttribute(1, "class", "connector");
            builder.AddAttribute(2, "connector-id", Connector.Id);

            int length = Math.Max(50, (int)(Strength * (End.X - Start.X)));

            Point c1 = new Point(Start.X + length, Start.Y);
            if(c1.X == Start.X) c1.X++;
            Point c2 = new Point(End.X - length, End.Y);
            if(c2.X == End.X) c2.X--;

            string pathString = string.Format("M{0},{1} C{2},{3} {4},{5} {6},{7}", Start.X, Start.Y, c1.X, c1.Y, c2.X, c2.Y, End.X, End.Y);
            builder.AddAttribute(3, "d", pathString);

            builder.AddElementReferenceCapture(13, value => this.element = value);
            builder.OpenComponent(4, viewType);
            builder.AddAttribute(5, "Connector", this);
            builder.CloseComponent();
            builder.CloseElement();
        }

        public void SetPositions(Point start, Point end) {
            if(Start == start && End == end)
                return;
            Start = start;
            End = end;
            Refresh();
        }

        public object DataItem { get { return Connector; } }

        //protected override async Task OnAfterRenderAsync(bool firstRender) {
        //    await base.OnAfterRenderAsync(firstRender);

        //    if(firstRender || _becameVisible) {
        //        _becameVisible = false;
        //        await JsRuntime.ObserveResizes(_element, _reference);
        //    }
        //}
    }
}
