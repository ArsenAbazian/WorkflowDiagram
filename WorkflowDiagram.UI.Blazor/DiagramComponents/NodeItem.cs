using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System.ComponentModel;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public class NodeItem : ComponentBase, IDiagramItem, IDisposable {

        protected ElementReference element;
        protected DotNetObjectReference<NodeItem> refThis;

        [CascadingParameter]
        public WfDiagramComponent Diagram { get; set; }

        [Parameter]
        public WfNode Node { get; set; }

        [Parameter]
        public string Class { get; set; }

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

        protected void Refresh() {
            this.shouldRender = true;
            StateHasChanged();
        }

        public void Dispose() {
            if(Node != null)
                Node.PropertyChanged -= OnNodeChanged;

            if(this.element.Id != null)
                _ = JsRuntimeHelper.SubscribeResizes(JsRuntime, this.refThis, this.element);

            this.refThis?.Dispose();
        }

        public void Move(float dx, float dy) {
            Node.X += dx;
            Node.Y += dy;
            MoveNodeContent(dx, dy);
            Refresh();
        }

        protected virtual void MoveNodeContent(float dx, float dy) {
            foreach(var point in Diagram.PointItems.Values) {
                if(point.Point.Node != Node)
                    continue;
                point.Move(dx, dy);
            }

            foreach(var connector in Diagram.ConnectorItems.Values) {
                if(connector.Connector.From?.Node == Node)
                    connector.MoveStart(dx, dy);
                if(connector.Connector.To?.Node == Node)
                    connector.MoveEnd(dx, dy);
            }
        }

        public void SetPosition(float x, float y) {
            float dx = x - Node.X;
            float dy = y - Node.Y;
            Node.X = x;
            Node.Y = y;
            MoveNodeContent(dx, dy);
            Refresh();
        }

        protected void OnNodeChanged(object sender, PropertyChangedEventArgs e) {
            Refresh();
        }

        protected override void OnInitialized() {
            base.OnInitialized();

            this.refThis = DotNetObjectReference.Create(this);
            if(Node != null)
                Node.PropertyChanged += OnNodeChanged;
            Diagram.NodeItems[Node] = this;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            var viewType = Diagram.CreateViewTypeFor(this);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "node");
            builder.AddAttribute(2, "node-id", Node.Id);

            int x = Convert.ToInt32(Node.X); 
            int y = Convert.ToInt32(Node.Y);
            int w = Convert.ToInt32(Node.Width);

            builder.AddAttribute(3, "style", $"top: {y}px; left: {x}px; width: {w}px;");
            builder.AddAttribute(3, "transform", $"translate({x} {y})");
            builder.AddElementReferenceCapture(13, value => this.element = value);
            builder.OpenComponent(4, viewType);
            builder.AddAttribute(5, "Node", this);
            builder.CloseComponent();
            builder.CloseElement();
        }

        static readonly string DefaultSelectionClass = "";
        static readonly string SelectedClass = "node-item-selected";

        [Parameter]
        public string SelectionClass { get; set; } = DefaultSelectionClass;

        bool isSelected;
        public bool Selected {
            get { return isSelected; }
            set {
                if(Selected == value)
                    return;
                isSelected = value;
                OnSelectionChanged();
            }
        }

        protected virtual void OnSelectionChanged() {
            Diagram.SelectedItemChanged(this);
            SelectionClass = Selected ? SelectedClass : DefaultSelectionClass;
            Refresh();
        }

        public object DataItem { get { return Node; } }

        //protected override async Task OnAfterRenderAsync(bool firstRender) {
        //    await base.OnAfterRenderAsync(firstRender);

        //    if(firstRender || _becameVisible) {
        //        _becameVisible = false;
        //        await JsRuntime.ObserveResizes(_element, _reference);
        //    }
        //}
    }
}
