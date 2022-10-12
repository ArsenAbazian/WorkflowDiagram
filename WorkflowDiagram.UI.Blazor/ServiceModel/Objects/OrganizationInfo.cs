using DevExpress.Xpo;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class WorkspaceInfo : XPObject {
		public WorkspaceInfo(Session session) : base(session) { }
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

		private UserInfo owner;
		[DevExpress.Xpo.Association("Owner-Workspaces")]
		[DisplayName("Owner")]
		public UserInfo Owner {
			get { return owner; }
			set { SetPropertyValue<UserInfo>(nameof(Owner), ref owner, value); }
		}

		[DisplayName("Projects")]
		[DevExpress.Xpo.Association("Workspace-Projects")]
		public XPCollection<ProjectInfo> Projects {
			get { return GetCollection<ProjectInfo>(nameof(Projects)); }
		}

		[DisplayName("Participants")]
		[DevExpress.Xpo.Association("Workspace-Participants")]
		public XPCollection<ParticipantInfo> Participants {
			get { return GetCollection<ParticipantInfo>(nameof(Participants)); }
		}
	}
}
