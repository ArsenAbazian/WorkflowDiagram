using Microsoft.AspNetCore.Components;
using WorkflowDiagram.UI.Blazor.Helpers;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent.ValueCellViews {
    public partial class PgFlagsValueView {
        [Parameter]
        public PgValueItem Item { get; set; }

        public List<EnumInfo> Items { get; set; }
        public List<Enum> Values { get; set; }

        protected override void OnInitialized() {
            base.OnInitialized();
            Items = EnumHelper.GetItems(Item.Value.Property.PropertyType);
            Values = EnumHelper.GetValues(Item.Value.Property.PropertyType);
        }
    }
}
