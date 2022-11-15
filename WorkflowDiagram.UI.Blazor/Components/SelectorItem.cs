using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace WorkflowDiagram.UI.Blazor.Components {
    public class SelectorItem : ComponentBase, IDisposable {
        public SelectorItem() {

        }

        private bool disposedValue;

        protected ElementReference element;
        protected DotNetObjectReference<SelectorItem> refThis;

        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            if(this.disposedValue)
                return;
            var viewType = typeof(SelectorItemView);
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "selector-item");
            builder.AddElementReferenceCapture(13, value => this.element = value);
            builder.OpenComponent(4, viewType);
            builder.AddAttribute(5, "Item", this);
            builder.CloseComponent();
            builder.CloseElement();
        }

        protected internal void SetSelected(bool sel) {
            this.selected = sel;
            //StateHasChanged();
        }

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                }

                disposedValue = true;
            }
        }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        bool selected;
        public bool Selected {
            get { return selected; }
            set {
                if(Selected == value)
                    return;
                selected = value;
                OnSelectedChanged();
            }
        }

        protected virtual void OnSelectedChanged() {
            if(Selected && SelectorComponent != null) {
                SelectorComponent.SelectedItem = this;
                SelectorComponent.SetSelectedValue(Value);
            }
            StateHasChanged();
        }

        protected virtual string SelectedClassName { get { return "si-selected"; } }
        protected virtual string NonSelectedClassName { get { return ""; } }
        public string SelectionClass { get { return Selected ?  SelectedClassName: NonSelectedClassName; } }

        SelectorComponent selectorComponent;
        [CascadingParameter]
        public SelectorComponent SelectorComponent {
            get { return selectorComponent; }
            set {
                if(SelectorComponent == value)
                    return;
                selectorComponent = value;
                OnSelectorComponentChanged();
            }
        }

        protected virtual void OnSelectorComponentChanged() {
            if(SelectorComponent != null) {
                SelectorComponent.ItemsCore.Add(this);
            }
        }

        protected internal virtual void OnMouseDown(MouseEventArgs e) {
            Selected = true;
        }

        //protected override Task OnInitializedAsync() {
        //    return base.OnInitializedAsync().ContinueWith(t => {
        //        if(SelectorComponent.SelectedItem != null)
        //            return;
        //        InvokeAsync(() => {
        //            SelectorComponent.SuppressEvents = true;
        //            try {
        //                SelectorComponent.SelectedItem = this;
        //            }
        //            finally {
        //                SelectorComponent.SuppressEvents = false;
        //            }
        //        });
        //    });
        //}

        [Parameter]
        public string Text { get; set; }
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public object Value { get; set; }
    }
}
