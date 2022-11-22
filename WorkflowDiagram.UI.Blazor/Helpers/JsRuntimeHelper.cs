using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;

namespace WorkflowDiagram.UI.Blazor.Helpers {
    public static class JsRuntimeHelper {
        public static async Task<RectangleF> GetBoundingClientRect(IJSRuntime jsRuntime, ElementReference element) {
            return await jsRuntime.InvokeAsync<RectangleF>("WfDiagramViewport.getBoundingClientRect", element);
        }

        public static async Task SubscribeResizes<T>(IJSRuntime jsRuntime, DotNetObjectReference<T> reference, ElementReference element) where T : class {
            await jsRuntime.InvokeVoidAsync("WfDiagramViewport.observe", element, reference, element.Id);
        }

        public static async Task UnsubscribeResizes(IJSRuntime jsRuntime, ElementReference element) {
            await jsRuntime.InvokeVoidAsync("WfDiagramViewport.unobserve", element, element.Id);
        }
    }
}
