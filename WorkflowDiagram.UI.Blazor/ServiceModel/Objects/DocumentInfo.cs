using DevExpress.Xpo;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class DocumentInfo : XPObject {
		public DocumentInfo(Session session) : base(session) { }
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

        private ProjectInfo project;
        [DevExpress.Xpo.Association("Project-Documents")]
        [DisplayName("Project")]
        public ProjectInfo Project {
            get { return project; }
            set { SetPropertyValue<ProjectInfo>(nameof(Project), ref project, value); }
        }

        string code;
        [DisplayName("Code"), Size(SizeAttribute.Unlimited)]
        public string Code {
            get { return code; }
            set { SetPropertyValue<string>(nameof(Code), ref code, value); }
        }

        public string CreationDateString { get { return CreationTime.ToShortDateString(); } }
    }
}
