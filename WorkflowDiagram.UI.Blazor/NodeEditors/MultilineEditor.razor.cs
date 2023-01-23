using Microsoft.AspNetCore.Components;
using WorkflowDiagram.UI.Blazor.PropertyGridComponent.ValueCellViews;

namespace WorkflowDiagram.UI.Blazor.NodeEditors {
    public partial class MultilineEditor {
        PgCustomValueView view;
        [Parameter]
        public PgCustomValueView View {
            get { return view; }
            set {
                if(View == value)
                    return;
                view = value;
                OnViewChanged();
            }
        }

        string text = "";
        public string Text {
            get { return text.Trim(); }
            set {
                if(value == null)
                    value = "0";
                if(Text == value)
                    return;
                text = value.Replace("\n", "\r\n");
                OnCodeChanged();
            }
        }

        public Type GlobalsType {
            get;
            set;
        }

        protected virtual void OnCodeChanged() { }

        protected virtual void OnViewChanged() {
            Text = View.Item.Value.Value == null ? "" : Convert.ToString(View.Item.Value.Value);
        }
    }
}
