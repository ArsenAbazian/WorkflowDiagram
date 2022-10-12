using System.ComponentModel;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public class PropertyGridObjectValueRow : PropertyGridValueRow {
        public PropertyGridObjectValueRow(PropertiesDataModel owner, object[] objects, PropertyDescriptor prop) : base(owner, objects, prop) {
            object[] values = objects.Select(o => prop.GetValue(o)).ToArray();
            Children = Owner.GetRowsFor(values);
        }
    }
}
