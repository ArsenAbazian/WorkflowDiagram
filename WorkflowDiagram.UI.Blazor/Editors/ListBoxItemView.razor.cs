using Microsoft.AspNetCore.Components.Web;

namespace WorkflowDiagram.UI.Blazor.Editors {
    public partial class ListBoxItemView {

        bool isSelected;
        public bool IsSelected {
            get { return isSelected; }
            set {
                if(IsSelected == value)
                    return;
                isSelected = value;
                OnIsSelectedChanged();
            }
        }

        bool shouldRender = true;
        protected override bool ShouldRender() {
            if(shouldRender) {
                shouldRender = false;
                return true;
            }

            return false;
        }

        protected internal void Refresh() {
            this.shouldRender = true;
            StateHasChanged();
        }

        private void OnIsSelectedChanged() {
            if(IsSelected)
                View.ListBoxComponent.SelectedItem = this;
            Refresh();
        }

        protected void OnMouseDown(MouseEventArgs e) {
            View.ListBoxComponent.OnItemMouseDown(this, e);
        }
        protected void OnMouseUp(MouseEventArgs e) {
            View.ListBoxComponent.OnItemMouseUp(this, e);
        }

        public string SelectionClass {
            get { return IsSelected ? "lb-item-selected" : ""; }
        }
    }
}
