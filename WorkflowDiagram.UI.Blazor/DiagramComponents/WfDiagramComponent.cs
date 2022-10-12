using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Drawing;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public class WfDiagramComponent : IDisposable {
        private bool disposedValue;

        public WfDiagramComponent() {
            SelectedItems = new WfSelectedItemsCollection(this);
        }

        public event SelectionChangedEventHandler SelectionChanged;
        protected internal virtual void OnSelectedItemsChanged() {
            RaiseSelectionChanged();
        }

        protected virtual void RaiseSelectionChanged() {
            SelectionChangedEventArgs e = new SelectionChangedEventArgs();
            e.Selection = SelectedItems.ToList();
            SelectionChanged?.Invoke(this, e);
        }

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    if(Document != null) Document.Changed -= OnDocumentPropertyChanged;
                }
                disposedValue = true;
            }
        }

        protected internal virtual void OnResizingAreaMouseUp(NodeView nodeView, MouseEventArgs e) {
            State = WdDiagramState.Default;
            EditingNodeItem = null;
        }

        protected internal virtual void OnRightResizingAreaMouseDown(NodeView nodeView, MouseEventArgs e) {
            EditingNodeItem = nodeView.Node;
            PrevPoint = ToViewport(e);
            State = WdDiagramState.DragNodeRightEdge;
        }

        protected internal virtual void OnLeftResizingAreaMouseDown(NodeView nodeView, MouseEventArgs e) {
            EditingNodeItem = nodeView.Node;
            PrevPoint = ToViewport(e);
            State = WdDiagramState.DragNodeLeftEdge;
        }


        WdDiagramState state;
        public WdDiagramState State {
            get { return state; }
            protected set {
                if(State == value)
                    return;
                state = value;
                OnStateChanged();
            }
        }

        protected virtual void OnStateChanged() {

        }

        protected PointF PrevPoint { get; set; }

        protected WfSelectedItemsCollection SelectedItems { get; private set; }

        void IDisposable.Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        WfDocument document;
        public WfDocument Document {
            get { return document; }
            set {
                if(Document == value)
                    return;
                WfDocument prev = Document;
                document = value;
                OnDocumentChanged(prev);
            }
        }

        public ConnectionPointItem GetConnectionPointItem(WfConnectionPoint point) {
            if(point == null)
                return null;
            ConnectionPointItem res = null;
            PointItems.TryGetValue(point, out res);
            return res;
        }

        protected virtual void OnDocumentChanged(WfDocument prev) {
            if(prev != null)
                prev.Changed -= OnDocumentPropertyChanged;
            if(Document != null)
                Document.Changed += OnDocumentPropertyChanged;
            ToolboxItems = null;
            if(Document != null)
                ToolboxItems = GetToolboxItems();
        }

        protected virtual List<WfNode> GetToolboxItems() {
            var res = Document.GetAvailableToolbarItems();
            return res;
        }

        public Type CreateViewTypeFor(NodeItem item) {
            return typeof(NodeView);
        }

        public Type CreateViewTypeFor(ConnectorItem item) {
            return typeof(ConnectorView);
        }

        public Type CreateViewTypeFor(ConnectionPointItem item) {
            return typeof(ConnectionPointView);
        }

        public event EventHandler Changed;
        protected virtual void RaiseDiagramChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        protected virtual void OnDocumentPropertyChanged(object sender, EventArgs e) {
            RaiseDiagramChanged();
        }

        public List<WfNode> ToolboxItems { get; set; } = new List<WfNode>();
        public ToolboxItem DraggedItem { get; internal set; }

        protected internal virtual void SelectedItemChanged(IDiagramItem item) {
            if(item.Selected)
                SelectedItems.Add(item);
            else
                SelectedItems.Remove(item);
        }

        public event EventHandler MouseDown;
        public event EventHandler MouseMove;
        public event EventHandler MouseUp;
        public event EventHandler KeyDown;
        public event EventHandler MouseWheel;
        public event EventHandler MouseClick;
        public event EventHandler MouseDoubleClick;
        public event EventHandler TouchStart;
        public event EventHandler TouchMove;
        public event EventHandler TouchEnd;

        protected internal virtual void OnMouseDown(object sender, MouseEventArgs e) {
            if(sender == Viewport)
                SelectedItems.Clear();
            if(sender == Viewport) {
                PrevPoint = ToViewport(e);
                State = WdDiagramState.Pan;
            }
            MouseDown?.Invoke(sender, e);
        }

        protected internal virtual void OnDragEnd(WfDiagramView wfDiagramView, DragEventArgs e) {

        }

        protected internal virtual void OnDragEnter(WfDiagramView wfDiagramView, DragEventArgs e) {
            if(DraggedItem != null)
                e.DataTransfer.DropEffect = "copy";
        }

        protected internal virtual void OnDragOver(WfDiagramView wfDiagramView, DragEventArgs e) {
            if(DraggedItem != null)
                e.DataTransfer.DropEffect = "copy";
        }

        protected ConnectionPointItem DownPoint { get; set; }
        protected WfConnector EditingConnector { get; set; }
        protected WfNode EditingNode { get { return EditingNodeItem?.Node; } }
        protected NodeItem EditingNodeItem { get; set; }
        protected internal ConnectionPointView HotPoint { get; set; }
        protected internal virtual void OnConnectionPointMouseDown(ConnectionPointView view, MouseEventArgs e) {
            DownPoint = view.Point;
            State = WdDiagramState.CreateConnector;
            WfConnector connector = CreateConnectorForPoint(view.Point);
            EditingConnector = connector;
        }

        protected virtual void OnFinishCreatingConnector(ConnectionPointView view, MouseEventArgs e) {
            var hitPoint = ToDocument(e);
            ConnectionPointItem point = view?.Point;
            ConnectorItem item = GetConnectorItem(EditingConnector);
            if(!IsValidConnectionPoint(item, point)) {
                EditingConnector.From = null;
                EditingConnector.To = null;
                if(ConnectorItems.ContainsKey(item.Connector))
                    ConnectorItems.Remove(item.Connector);
                Document.Connectors.Remove(EditingConnector);
                State = WdDiagramState.Default;
                return;
            }
            point.UpdateBoundsAsync().ContinueWith(t => {
                if(point.Point.Type == WfConnectionPointType.In)
                    item.End = point.Bounds.GetCenter();
                else
                    item.Start = point.Bounds.GetCenter();
            });
            point.Point.Connectors.Add(EditingConnector);
            State = WdDiagramState.Default;
        }

        protected internal virtual void OnConnectionPointMouseUp(ConnectionPointView view, MouseEventArgs e) {
            OnFinishCreatingConnector(view, e);
        }

        protected virtual bool IsValidConnectionPoint(ConnectorItem item, ConnectionPointItem point) {
            if(point == null)
                return false;
            if(item.Connector.From != null && item.Connector.From.Type == point.Point.Type)
                return false;
            if(item.Connector.To != null && item.Connector.To.Type == point.Point.Type)
                return false;
            return true;
        }

        protected virtual WfConnector CreateConnectorForPoint(ConnectionPointItem point) {
            WfConnector c = new WfConnector();
            Document.AddConnector(c);
            point.Point.Connectors.Add(c);
            return c;
        }

        public virtual void Refresh() => Changed?.Invoke(this, EventArgs.Empty);

        protected internal virtual void OnConnectorMouseDown(ConnectorView connectorView, MouseEventArgs e) {
            if(!e.CtrlKey && !connectorView.Connector.Selected)
                SelectedItems.Clear();

            connectorView.Connector.Selected = true;
            PrevPoint = new Point((int)e.PageX, (int)e.PageY);
        }

        protected internal void OnNodeMouseDown(NodeView view, MouseEventArgs e) {
            State = WdDiagramState.DragNode;

            if(!e.CtrlKey && !view.Node.Selected)
                SelectedItems.Clear();

            view.Node.Selected = true;
            PrevPoint = new Point((int)e.PageX, (int)e.PageY);
        }

        protected internal virtual void OnMouseMove(object sender, MouseEventArgs e) {
            if(State == WdDiagramState.Pan)
                OnPan(e);
            else if(State == WdDiagramState.DragNode)
                OnDragNodesMouseMove(sender, e);
            else if(State == WdDiagramState.CreateConnector)
                OnConnectorStateMouseMove(sender, e);
            else if(State == WdDiagramState.DragNodeRightEdge)
                OnResizeNodeRight(e);
            else if(State == WdDiagramState.DragNodeLeftEdge)
                OnResizeNodeLeft(e);
            MouseMove?.Invoke(sender, e);
        }

        protected virtual void OnPan(MouseEventArgs e) {
            PointF current = ToViewport(e);
            PointF delta = new PointF(current.X - PrevPoint.X, current.Y - PrevPoint.Y);
            PrevPoint = current;
            Origin = new PointF(Origin.X - delta.X, Origin.Y - delta.Y);
        }

        protected virtual void OnResizeNodeLeft(MouseEventArgs e) {

        }

        protected virtual void OnResizeNodeRight(MouseEventArgs e) {

        }

        public PointF ToDocument(MouseEventArgs e) {
            return ToDocument((float)e.PageX, (float)e.PageY);
        }

        public PointF ToDocument(float x, float y) {
            PointF loc = ToViewport(x, y);
            return new PointF((float)((loc.X + Origin.X) / ZoomFactor), (float)((loc.Y + Origin.Y) / ZoomFactor));
            //return new PointF((float)(loc.X / ZoomFactor) + Origin.X, (float)(loc.Y / ZoomFactor) + Origin.Y);
        }

        protected internal PointF ToDocumentNoScale(float x, float y) {
            PointF loc = ToViewport(x, y);
            return new PointF(loc.X + Origin.X, loc.Y + Origin.Y);
        }

        protected internal PointF ToDocumentNoScale(MouseEventArgs e) {
            return ToDocumentNoScale((float)e.PageX, (float)e.PageY);
        }

        protected PointF ToViewport(float x, float y) {
            return new PointF(x - Viewport.ViewportBounds.X, y - Viewport.ViewportBounds.Y);
        }
        
        protected PointF ToViewport(MouseEventArgs e) {
            return ToViewport((float)e.PageX, (float)e.PageY);
        }

        protected virtual void OnConnectorStateMouseMove(object sender, MouseEventArgs e) {
            PointF vp = ToViewport(e);
            PointF client = ToDocument(e); 
            ConnectorItem item = GetConnectorItem(EditingConnector);
            if(item == null)
                return;
            item.End = new Point((int)client.X, (int)client.Y);
            item.Refresh();
        }

        protected virtual ConnectorItem GetConnectorItem(WfConnector connector) {
            if(connector == null)
                return null;
            ConnectorItem item = null;
            ConnectorItems.TryGetValue(connector, out item);
            return item;
        }

        protected virtual void OnDragNodesMouseMove(object sender, MouseEventArgs e) {
            PointF delta = new PointF((float)(e.PageX - PrevPoint.X), (float)(e.PageY - PrevPoint.Y));
            foreach(var node in SelectedItems)
                node.Move((float)(delta.X), (float)(delta.Y));
            PrevPoint = new Point((int)e.PageX, (int)e.PageY);
        }

        protected internal virtual void OnMouseUp(object sender, MouseEventArgs e) {
            if(State == WdDiagramState.CreateConnector)
                OnFinishCreatingConnector(HotPoint, e);
            
            State = WdDiagramState.Default;
            MouseUp?.Invoke(sender, e);
        }

        public ConnectionPointItem GetHitTestPoint(MouseEventArgs e) {
            var local = ToViewport(e);
            foreach(var point in PointItems.Values) {
                if(point.Bounds.Contains(local))
                    return point;
            }
            return null;
        }

        public double[] ZoomFactors { get; set; } = new double[] { 0.1, 0.11, 0.125, 0.15, 0.175, 0.2, 0.225, 0.25, 0.275, 0.30, 0.33, 0.36, 0.40, 0.45, 0.5, 0.55, 0.60, 0.67, 0.75, 0.8, 0.9, 1.0, 1.1, 1.25, 1.5, 1.75, 2.0, 2.25, 2.50, 2.75, 3.0, 3.5, 4.0, 4.5, 5.0 };
        protected virtual void OnZoom(WheelEventArgs e) {
            PointF ptDoc = ToDocument(e);
            PointF ptLocal = ToViewport(e);

            double delta = (float)(e.DeltaY != 0 ? e.DeltaY : e.DeltaX);
            if(delta < 0)
                ZoomFactor = ZoomIn(ZoomFactor);
            else
                ZoomFactor = ZoomOut(ZoomFactor);
            Origin = new PointF(ptDoc.X - (float)(ptLocal.X / ZoomFactor), ptDoc.Y - (float)(ptLocal.Y / ZoomFactor));
        }

        protected virtual double ZoomIn(double zoom) {
            if(zoom > ZoomFactors[ZoomFactors.Length - 1])
                return zoom;
            return ZoomFactors.FirstOrDefault(zf => zf > zoom);
        }

        protected virtual double ZoomOut(double zoom) {
            if(zoom < ZoomFactors[0])
                return zoom;
            return ZoomFactors.LastOrDefault(zf => zf < zoom);
        }

        protected internal virtual void OnKeyDown(object sender, KeyboardEventArgs e) => KeyDown?.Invoke(sender, e);
        protected internal virtual void OnMouseWheel(object sender, WheelEventArgs e) {
            if(!e.CtrlKey)
                OnZoom(e);
            
            MouseWheel?.Invoke(sender, e);
        }
        protected internal virtual void OnMouseClick(object sender, MouseEventArgs e) => MouseClick?.Invoke(sender, e);
        protected internal virtual void OnMouseDoubleClick(object sender, MouseEventArgs e) => MouseDoubleClick?.Invoke(sender, e);
        protected internal virtual void OnTouchStart(object sender, TouchEventArgs e) => TouchStart?.Invoke(sender, e);
        protected internal virtual void OnTouchMove(object sender, TouchEventArgs e) => TouchMove?.Invoke(sender, e);
        protected internal virtual void OnTouchEnd(object sender, TouchEventArgs e) => TouchEnd?.Invoke(sender, e);

        Point discrete = new Point(0, 0);
        PointF origin = new PointF(0.0f, 0.0f);
        public PointF Origin {
            get { return origin; }
            set {
                origin = value;
                Refresh();
                //Point newDiscrete = new Point((int)(value.X), (int)(value.Y));
                //if(IsPointDiffers(newDiscrete, discrete)) {
                //    discrete = newDiscrete;
                //    Refresh();
                //}
            }
        }

        protected virtual bool IsPointDiffers(Point p1, Point p2) {
            return p1 != p2;
        }

        double zoom = 1.0f;
        public double ZoomFactor {
            get { return zoom; }
            set {
                if(ZoomFactor == value)
                    return;
                zoom = value;
                Refresh();
            }
        }
        public double ZoomFactorPercent { get { return ZoomFactor * 100; } }
        public WfDiagramView Viewport { get; internal set; }
        public Dictionary<WfConnector, ConnectorItem> ConnectorItems { get; } = new Dictionary<WfConnector, ConnectorItem>();
        public Dictionary<WfConnectionPoint, ConnectionPointItem> PointItems { get; } = new Dictionary<WfConnectionPoint, ConnectionPointItem>();
        public Dictionary<WfNode, NodeItem> NodeItems { get; } = new Dictionary<WfNode, NodeItem>();
    }

    public enum WdDiagramState {
        Default,
        DragNode,
        CreateConnector,
        DragNodeRightEdge,
        DragNodeLeftEdge,
        Pan
    }
}

