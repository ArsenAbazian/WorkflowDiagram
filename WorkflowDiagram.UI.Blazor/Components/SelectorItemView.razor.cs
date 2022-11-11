using Microsoft.AspNetCore.Components;

namespace WorkflowDiagram.UI.Blazor.Components {
    public partial class SelectorItemView : ComponentBase {
        public SelectorItemView() {

        }

        [Parameter]
        public SelectorItem Item { get; set; }
    }
}
