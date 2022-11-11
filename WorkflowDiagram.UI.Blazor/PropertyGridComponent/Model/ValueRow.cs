using System.ComponentModel;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public class PropertyGridValueRow : PropertyGridRowBase {
        public PropertyGridValueRow(PropertiesDataModel owner, object[] objects, PropertyDescriptor property) : base(owner) {
            Property = property;
            Values = objects.Select(o => CreateValue(this, o)).ToList();
        }

        public PropertyDescriptor Property { get; private set; }
        public override string Name { get { return string.IsNullOrEmpty(Property.DisplayName) ? Property.Name : Property.DisplayName; } set { } }

        public List<PropertyGridValueInfo> Values { get; }

        protected internal void OnValueChanged(PropertyGridValueInfo valueInfo) {
            Owner.OnRowChanged(this);
        }

        protected internal virtual PropertyGridValueInfo CreateValue(PropertyGridValueRow row, object owner) {
            return new PropertyGridValueInfo(row, owner);
        }

        public T GetAttribute<T>() where T : Attribute {
            for(int i = 0; i < Property.Attributes.Count; i++) {
                var a = Property.Attributes[i].GetType();
                if(typeof(T).IsSubclassOf(a) || typeof(T) == a)
                    return Property.Attributes[i] as T;
            }
            return null;
        }

        public void SetValue(object value) {
            Values.ForEach(v => v.Value = value);
        }
    }
}
