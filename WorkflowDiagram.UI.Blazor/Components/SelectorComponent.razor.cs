using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace WorkflowDiagram.UI.Blazor.Components {
    public partial class SelectorComponent {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        public SelectorItemCollection ItemsCore { get; private set; }

        public SelectorComponent() {
            ItemsCore = new SelectorItemCollection(this);
        }

        SelectorItem selectedItem;
        public SelectorItem SelectedItem {
            get { return selectedItem; }
            set {
                if(SelectedItem == value)
                    return;
                var prev = SelectedItem;
                selectedItem = value;
                OnSelectedItemChanged(prev);
            }
        }

        protected virtual void OnSelectedItemChanged(SelectorItem prevItem) {
            if(prevItem != null)
                prevItem.Selected = false;
            if(SelectedItem != null) {
                SelectedItem.Selected = true;
                SetSelectedValue(SelectedItem.Value);
            }
        }

        protected internal void SetSelectedValue(object value) {
            if(object.Equals(this.selectedValue, value))
                return;
            this.selectedValue = value;
            if(!SuppressEvents)
                SelectedValueChanged.InvokeAsync(value);
        }

        protected internal bool SuppressEvents { get; set; }

        [Parameter]
        public EventCallback<object> SelectedValueChanged { get; set; }

        [Parameter]
        public RenderFragment Items {
            get { return ChildContent; }
            set { ChildContent = value; }
        }

        [Parameter]
        public SelectorComponentOrientation Orientation { get; set; }

        internal string OrientationClass { get { return Orientation == SelectorComponentOrientation.Horizontal ? "sc-horizontal" : "sc-vertical"; } }

        object selectedValue;
        [Parameter]
        public object SelectedValue {
            get { return selectedValue; }
            set {
                if(object.Equals(SelectedValue, value))
                    return;
                selectedValue = value;
                OnSelectedValueChanged();
            }
        }

        protected virtual void OnSelectedValueChanged() {
            var item = GetItemByValue(SelectedValue);
            if(item != null)
                item.Selected = true;
            StateHasChanged();
        }

        private SelectorItem GetItemByValue(object value) {
            return ItemsCore.FirstOrDefault(i => object.Equals(i.Value, value));
        }
        protected override Task OnInitializedAsync() {
            return base.OnInitializedAsync().ContinueWith(t => {
                if(ItemsCore.Count > 0 && SelectedItem == null) {
                    SuppressEvents = true;
                    try {
                        SelectedItem = ItemsCore[0];
                    }
                    finally {
                        SuppressEvents = false;
                    }
                }
            });
        }
    }

    public enum SelectorComponentOrientation {
        Horizontal,
        Vertical
    }

    public class SelectorItemCollection : ObservableCollection<SelectorItem> {
        public SelectorItemCollection(SelectorComponent owner) {
            Owner = owner;
        }
        public SelectorComponent Owner { get; private set; }
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            base.OnCollectionChanged(e);
            if(Owner.SelectedItem != null && e.OldItems != null && e.OldItems.Contains(Owner.SelectedItem))
                Owner.SelectedItem = null;
            if(Owner.SelectedItem == null && Owner.ItemsCore.Count > 0)
                Owner.SelectedItem = Owner.ItemsCore.FirstOrDefault(i => object.Equals(i.Value, Owner.SelectedValue));
        }
    }
}
