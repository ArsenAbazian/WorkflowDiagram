using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Drawing;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public partial class PropertyGridView : IDisposable {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    if(refThis == null)
                        return;

                    //if(refElement.Id != null)
                    //    _ = JsRuntimeHelper.SubscribeResizes(JSRuntime, refThis, refElement);

                    refThis.Dispose();
                }
                disposedValue = true;
            }
        }

        void IDisposable.Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        [CascadingParameter]
        public PropertyGridComponent PropertyGrid { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        protected ElementReference refElement;
        private DotNetObjectReference<PropertyGridView> refThis;

        bool shouldRender = true;
        protected override bool ShouldRender() {
            if(shouldRender) {
                shouldRender = false;
                return true;
            }

            return false;
        }

        protected override void OnInitialized() {
            base.OnInitialized();
            if(PropertyGrid != null) {
                PropertyGrid.Changed += OnPropertyGridChanged;
            }
            this.refThis = DotNetObjectReference.Create(this);
        }

        protected virtual void OnPropertyGridChanged(object sender, EventArgs e) {
            shouldRender = true;
            StateHasChanged();
        }

        private void OnPropertyGridChanged() {
            shouldRender = true;
            StateHasChanged();
        }

        [Parameter]
        public string TextAreaWidth { get; set; } = "36%";
    }
}
