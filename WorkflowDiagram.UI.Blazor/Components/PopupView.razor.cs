using Microsoft.AspNetCore.Components;

namespace WorkflowDiagram.UI.Blazor.Components {
    public partial class PopupView : ComponentBase {
        public void Refresh() {
            StateHasChanged();
        }
    }
}
