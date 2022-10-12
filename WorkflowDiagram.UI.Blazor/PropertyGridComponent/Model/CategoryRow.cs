using System.ComponentModel;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public class PropertyGridCategoryRow : PropertyGridRowBase {
        public PropertyGridCategoryRow(PropertiesDataModel owner, object[] objects, string category, PropertyDescriptor[] properties) : base(owner) {
            Name = category;
            Children = properties.Select(pd => Owner.CreateRow(objects, pd)).ToList();
        }
    }
}
