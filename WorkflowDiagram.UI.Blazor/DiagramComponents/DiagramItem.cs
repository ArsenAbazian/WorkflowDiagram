namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public interface IDiagramItem {
        bool Selected { get; set; }
        void Move(float dx, float dy);
        public object DataItem { get; }
    }
}
