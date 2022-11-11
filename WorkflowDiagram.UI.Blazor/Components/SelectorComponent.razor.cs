using Microsoft.AspNetCore.Components;

namespace WorkflowDiagram.UI.Blazor.Components {
    public partial class SelectorComponent {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        public List<SelectorItem> ItemsCore { get; } = new List<SelectorItem>();

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
}
