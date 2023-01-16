using Microsoft.AspNetCore.Components;
using Org.BouncyCastle.Utilities;
using System.Collections;
using System.Reflection;
using WorkflowDiagram.UI.Blazor.PropertyGridComponent.ValueCellViews;

namespace WorkflowDiagram.UI.Blazor.NodeEditors {
    public partial class CollectionEditor {

        public CollectionEditor() {
            
        }

        [CascadingParameter]
        public CollectionEditorDialog Dialog { get; set; }

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

        [Parameter]
        public IList SourceCollection { get; set; }

        protected virtual void OnViewChanged() {
            IList coll = View.Item.Value.Value as IList;
            SourceCollection = coll;
        }
    }
}
