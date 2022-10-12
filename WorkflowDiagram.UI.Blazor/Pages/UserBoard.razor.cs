using WorkflowDiagram.UI.Blazor.ServiceModel;
using Microsoft.AspNetCore.Identity;

namespace WorkflowDiagram.UI.Blazor.Pages {
    public partial class UserBoard {

        public UserBoard() {
        }

        protected async override Task OnInitializedAsync() {
            base.OnInitialized();
            UserInfo info = await UserManager.GetUserAsync(HttpContextAccessor.HttpContext.User);
        
        }

        public List<WorkspaceInfo> Workspaces { get; set; } = new List<WorkspaceInfo>(); 
    }
}
