using DevExpress.Xpo;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {
    public class ParticipantInfo : XPObject {
        public ParticipantInfo(Session session) : base(session){
        }

        UserInfo user;
        [DisplayName("User")]
        public UserInfo User {
            get { return user; }
            set { SetPropertyValue<UserInfo>(nameof(User), ref user, value); }
        }

        ParticipantRole role;
        [DisplayName("Role")]
        public ParticipantRole Role {
            get { return role; }
            set { SetPropertyValue<ParticipantRole>(nameof(Role), ref role, value); }
        }

        WorkspaceInfo workspace;
        [DisplayName("Workspace")]
        [DevExpress.Xpo.Association("Workspace-Participants")]
        public WorkspaceInfo Workspace {
            get { return workspace; }
            set { SetPropertyValue<WorkspaceInfo>(nameof(Workspace), ref workspace, value); }
        }
    }

    public enum ParticipantRole {
        [System.ComponentModel.Description("Owner")]
        Owner,
        [System.ComponentModel.Description("Administrator")]
        Administrator,
        [System.ComponentModel.Description("User")]
        User
    }
}
