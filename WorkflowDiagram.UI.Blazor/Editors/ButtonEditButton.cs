using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace WorkflowDiagram.UI.Blazor.Editors {
    public partial class ButtonEditButton : ComponentBase {
        public ButtonEditButton() { }

        protected internal ButtonEditButtonCollection Collection { get; set; }

        string text;
        [Parameter]
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

        string glyph;
        [Parameter]
        public string Glyph {
            get { return glyph; }
            set {
                if(Glyph == value)
                    return;
                glyph = value;
                OnGlyphChanged();
            }
        }
        protected virtual void OnGlyphChanged() {
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<ButtonEditButton> Click { get; set; }

        protected internal void OnClick(MouseEventArgs e) {
            Click.InvokeAsync(this);
        }

        bool showText;
        public bool ShowText {
            get { return showText; }
            set {
                if(ShowText == value)
                    return;
                showText = value;
                OnShowTextChanged();
            }
        }
        protected virtual void OnShowTextChanged() {
            StateHasChanged();
        }

        public string TextClass { get { return ShowText && !string.IsNullOrEmpty(Text) ? "" : "hidden"; } }
        public string GlyphClass { get { return !string.IsNullOrEmpty(Glyph) ? "" : "hidden"; } }
        public string BorderClass {
            get {
                if(ButtonEdit.Buttons == null)
                    return "be-button-middle";
                if(Alignment == ButtonEditButtonAlignment.Left) {
                    if(ButtonEdit.LeftButtons.Count > 0 && ButtonEdit.LeftButtons[0] == this)
                        return "be-button-left be-first";
                    return "be-button-left";
                }
                if(ButtonEdit.RightButtons.Count > 0 && ButtonEdit.RightButtons[ButtonEdit.RightButtons.Count - 1] == this)
                        return "be-button-right be-last";
                return "be-button-rigth";
            }
        }

        public ButtonEditButtonAlignment Alignment { get; set; } = ButtonEditButtonAlignment.Right;
        ButtonEdit buttonEdit;
        [CascadingParameter]
        public ButtonEdit ButtonEdit {
            get { return buttonEdit; }
            set {
                if(ButtonEdit == value)
                    return;
                buttonEdit = value;
                OnButtonEditChanged();
            }
        }

        protected virtual void OnButtonEditChanged() {
            if(Alignment == ButtonEditButtonAlignment.Left)
                ButtonEdit.LeftButtons.Add(this);
            else
                ButtonEdit.RightButtons.Add(this);
            ButtonEdit.Refresh();
        }
    }

    public enum ButtonEditButtonAlignment { Left, Right }
}
