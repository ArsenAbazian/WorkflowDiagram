using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Reflection;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent.ValueCellViews {
    public partial class PgEnumValueView {
        [Parameter]
        public PgValueItem Item { get; set; }

        public List<EnumInfo> Items { get; set; }

        protected override void OnInitialized() {
            base.OnInitialized();
            Items = EnumHelper.GetItems(Item.Value.Property.PropertyType);
        }
    }
}
