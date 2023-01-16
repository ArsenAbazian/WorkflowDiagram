using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections;

namespace WorkflowDiagram.UI.Blazor.Editors {
    public partial class ListBoxComponent {

        IList itemsSource;
        [Parameter]
        public IList ItemsSource {
            get { return itemsSource; }
            set {
                if(itemsSource == value)
                    return;
                itemsSource = value;
                OnItemsSourceChanged();
            }
        }

        protected virtual void OnItemsSourceChanged() {
            
        }

        public ListBoxView View { get; set; }

        ListBoxItemView selectedItem;
        public ListBoxItemView SelectedItem {
            get { return selectedItem; }
            set {
                if(SelectedItem == value)
                    return;
                var prev = SelectedItem;
                selectedItem = value;
                OnSelectedItemChanged(prev);
            }
        }

        protected virtual void OnSelectedItemChanged(ListBoxItemView prev) {
            if(prev != null)
                prev.IsSelected = false;
            if(SelectedItem != null)
                SelectedItem.IsSelected = true;
            RaiseSelectionChanged();
        }

        int selectedItemIndex = -1;
        public int SelectedItemIndex {
            get { return selectedItemIndex; }
            set {
                value = Math.Max(-1, value);
                if(ItemsSource != null)
                    value = Math.Min(ItemsSource.Count - 1, value);
                if(SelectedItemIndex == value)
                    return;
                selectedItemIndex = value;
                OnSelectedItemIndexChanged();
            }
        }

        protected virtual void OnSelectedItemIndexChanged() {
            object item = SelectedItemIndex == -1 ? null : ItemsSource[SelectedItemIndex];
            if(item == null) {
                SelectedItem = null;
                return;
            }
            SelectedItem = View.Items.FirstOrDefault(i => i.Item == item);
        }

        protected override Task OnInitializedAsync() {
            
            return base.OnInitializedAsync();
        }

        protected internal void OnItemMouseDown(ListBoxItemView listBoxItemView, MouseEventArgs e) {
            SelectedItem = listBoxItemView;
        }

        protected internal void OnItemMouseUp(ListBoxItemView listBoxItemView, MouseEventArgs e) {
        }

        public event EventHandler SelectedItemChanged;
        protected internal virtual void OnSelectedItemsChanged() {
            RaiseSelectionChanged();
        }

        protected virtual void RaiseSelectionChanged() {
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
