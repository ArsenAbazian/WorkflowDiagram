namespace WorkflowDiagram.UI.Blazor.Pages {
    public partial class ProjectPage {
    }

    internal class DocumentPresenter {
        public int Oid { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string CreationDateString { get; set; }
        public string Description { get; set; }
    }
}
