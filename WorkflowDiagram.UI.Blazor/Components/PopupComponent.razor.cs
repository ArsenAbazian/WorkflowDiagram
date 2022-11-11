using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WorkflowDiagram.UI.Blazor.Components {
    public partial class PopupComponent : IDisposable {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: dispose managed state (managed objects)
                }

                disposedValue = true;
            }
        }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        string title;
        public string Title {
            get { return title; }
            set {
                if(Title == value)
                    return;
                title = value;
            }
        }

        [Parameter]
        public RenderFragment Content { get; set; }

        public List<PopupButton> Buttons { get; set; } = new List<PopupButton>();
        public bool ShowOkButton { get; set; } = true;
        public bool ShowCancelButton { get; set; } = false;
        public bool ShowCloseButton { get; set; } = false;

        PopupButton okButton;
        protected internal PopupButton OkButton {
            get {
                if(okButton == null)
                    okButton = CreateOkButton();
                return okButton;
            }
        }

        protected virtual PopupButton CreateOkButton() {
            var res = new PopupButton() { Text = "OK" };
            res.Click += OnOkButtonClick;
            return res;
        }

        protected virtual void OnOkButtonClick(object sender, EventArgs e) {
            OnOk?.Invoke(this, EventArgs.Empty);
            PopupVisible = "hidden";
        }

        PopupButton cancelButton;
        protected internal PopupButton CancelButton {
            get {
                if(cancelButton == null)
                    cancelButton = CreateCancelButton();
                return cancelButton;
            }
        }

        protected virtual PopupButton CreateCancelButton() {
            var res = new PopupButton() { Text = "Cancel" };
            res.Click += OnCancelButtonClick;
            return res;
        }

        protected virtual void OnCancelButtonClick(object sender, EventArgs e) {
            OnCancel?.Invoke(this, EventArgs.Empty);
            PopupVisible = "hidden";
        }

        protected internal void CloseButtonClick(MouseEventArgs e) {
            HidePopup();
        }

        public event EventHandler OnOk;
        public event EventHandler OnCancel;

        public string PopupVisible { get; set; } = "hidden";
        public void ShowPopup() {
            PopupVisible = "";
            View.Refresh();
        }
        public void HidePopup() {
            PopupVisible = "hidden";
            View.Refresh();
        }

        protected internal PopupView View { get; set; }
    }

    public class PopupButton {
        public string Text { get; set; }
        public string Icon { get; set; }
        public event EventHandler Click;
        internal void ButtonClick(MouseEventArgs e) {
            RaiseClick();
        }
        internal void RaiseClick() {
            Click?.Invoke(this, EventArgs.Empty);
        }
    }
}
