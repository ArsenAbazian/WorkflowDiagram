using DevExpress.Xpo;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class ProjectInfo : XPObject {
		public ProjectInfo(Session session) : base(session) { }
		string name;
		[DisplayName("Name")]
		public string Name {
			get { return name; }
			set { SetPropertyValue<string>(nameof(Name), ref name, value); }
		}
		
		string description;
		[DisplayName("Description")]
		public string Description {
			get { return description; }
			set { SetPropertyValue<string>(nameof(Description), ref description, value); }
		}

        DateTime dateTime;
		[DisplayName("CreationTime")]
        public DateTime CreationTime {
            get { return dateTime; }
            set { SetPropertyValue<DateTime>(nameof(CreationTime), ref dateTime, value); }
        }

		private WorkspaceInfo workspace;
		[DevExpress.Xpo.Association("Workspace-Projects")]
		[DisplayName("Workspace")]
		public WorkspaceInfo Workspace {
			get { return workspace; }
			set { SetPropertyValue<WorkspaceInfo>(nameof(Workspace), ref workspace, value); }
		}

		[DisplayName("Documents")]
		[DevExpress.Xpo.Association("Project-Documents")]
		public XPCollection<DocumentInfo> Documents {
			get { return GetCollection<DocumentInfo>(nameof(Documents)); }
		}

		[NotMapped]
		public string CreationDateString { get { return CreationTime.ToShortDateString(); } }
	}
}
