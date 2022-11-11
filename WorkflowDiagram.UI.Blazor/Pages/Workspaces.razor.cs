namespace WorkflowDiagram.UI.Blazor.Pages {
    public partial class Workspaces {
    }

    internal class WorkspacePresenter {
        public int Oid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProjectPresenter> Projects { get; set; }
    }

    internal class ProjectPresenter {
        public int Oid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreationDateString { get; set; }
        public int DocumentsCount { get; set; }
        public int DocumentCount { get; set; }
        public int OwnerId { get; set; }
    }
}
