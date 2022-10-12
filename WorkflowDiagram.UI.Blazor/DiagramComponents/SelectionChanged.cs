namespace WorkflowDiagram.UI.Blazor.DiagramComponents {
    public class SelectionChangedEventArgs : EventArgs {
        public List<IDiagramItem> Selection { get; set; }
    }

    public delegate void SelectionChangedEventHandler(object sender, SelectionChangedEventArgs e);
}
