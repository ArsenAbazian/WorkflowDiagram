using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace WorkflowDiagram.UI.Blazor.Editors {
    public partial class ButtonEdit : ComponentBase, IDisposable {
        private bool disposedValue;

        public ButtonEdit() {
            buttons = new ButtonEditButtonCollection(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                }
                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected int UpdateCount { get; set; }
        public bool IsLockUpdate { get { return UpdateCount > 0; } }
        public void BeginUpdate() {
            UpdateCount++;
        }
        public void CancelUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
        }
        public void EndUpdate() {
            if(UpdateCount == 0)
                return;
            UpdateCount--;
            if(UpdateCount == 0)
                StateHasChanged();
        }

        protected internal virtual void OnButtonCollectionChanged(ButtonEditButtonCollection buttonEditButtonCollection) {
            if(!IsLockUpdate)
                StateHasChanged();
        }

        ButtonEditButtonCollection buttons;
        [Parameter]
        public RenderFragment Buttons {
            get { return ChildContent; }
            set { ChildContent = value; }
        }

        internal List<ButtonEditButton> LeftButtons { get; } = new List<ButtonEditButton>();
        internal List<ButtonEditButton> RightButtons { get; } = new List<ButtonEditButton>();

        object editValue;
        [Parameter]
        public object EditValue {
            get { return editValue; }
            set {
                if(EditValue == value)
                    return;
                editValue = value;
                OnEditValueChanged();
            }
        }
        protected virtual void OnEditValueChanged() {
            UpdateText();
        }

        internal void Refresh() {
            StateHasChanged();
        }

        protected virtual void UpdateText() {
            if(EditValue == null)
                Text = string.Empty;
            string formatString = string.Format("{{0:{0}}}", DisplayFormat);
            Text = string.Format(formatString, EditValue);
        }

        string text;
        public string Text {
            get { return text; }
            set {
                if(Text == value)
                    return;
                text = value;
                OnTextChanged();
            }
        }
        protected virtual void OnTextChanged() {
            StateHasChanged();
        }

        [Parameter]
        public bool Readonly { get; set; }

        string displayFormat;
        public string DisplayFormat {
            get { return displayFormat; }
            set {
                if(DisplayFormat == value)
                    return;
                displayFormat = value;
                OnDisplayFormatChanged();
            }
        }
        protected virtual void OnDisplayFormatChanged() {
            UpdateText();
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }

    public class ButtonEditButtonCollection : ObservableCollection<ButtonEditButton> {
        public ButtonEditButtonCollection(ButtonEdit edit) {
            Owner = edit;
        }
        public ButtonEdit Owner { get; internal set; }
        
        protected override void InsertItem(int index, ButtonEditButton item) {
            base.InsertItem(index, item);
            Owner?.OnButtonCollectionChanged(this);
            item.Collection = this;
        }
        protected override void RemoveItem(int index) {
            this[index].Collection = null;
            base.RemoveItem(index);
            Owner?.OnButtonCollectionChanged(this);
        }
        protected override void SetItem(int index, ButtonEditButton item) {
            this[index].Collection = null;
            base.SetItem(index, item);
            item.Collection = this;
            Owner?.OnButtonCollectionChanged(this);
        }
        protected override void ClearItems() {
            foreach(var button in this)
                button.Collection = null;
            base.ClearItems();
            Owner?.OnButtonCollectionChanged(this);
        }
    }
}
