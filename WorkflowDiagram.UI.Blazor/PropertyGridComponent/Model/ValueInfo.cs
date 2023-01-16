using System.ComponentModel;
using System.Collections;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public class PropertyGridValueInfo {
        public PropertyGridValueInfo(PropertyGridValueRow row, object owner) {
            Owner = owner;
            Row = row;
            Value = Property.GetValue(Owner);
            SubscribeEvents();
        }

        public Type GetCustomEditorType() {
            return Row.GetAttribute<BlazorPropertyEditorAttribute>()?.EditorType;
        }

        public bool Readonly { 
            get { return Property.IsReadOnly; } 
        }

        public bool IsDecimal {
            get { return Property.PropertyType == typeof(decimal); }
        }

        public bool IsDouble {
            get { return Property.PropertyType == typeof(double); }
        }

        public bool IsFloat {
            get { return Property.PropertyType == typeof(float); }
        }

        public bool IsInteger {
            get { return Property.PropertyType == typeof(int); }
        }

        public decimal DecimalValue { get { return Convert.ToDecimal(Value); } set { Value = value; } }
        public double DoubleValue { get { return Convert.ToDouble(Value); } set { Value = value; } }
        public int IntValue { get { return Convert.ToInt32(Value); } set { Value = value; } }
        public float FloatValue { get { return (float)Value; } set { Value = value; } }
        public bool BooleanValue { get { return Convert.ToBoolean(Value); } set { Value = value; } }
        public IList CollectionValue { get { return Value as IList; } }

        protected void SubscribeEvents() {
            if(Owner is INotifyPropertyChanged)
                ((INotifyPropertyChanged)Owner).PropertyChanged += OnOwnerPropertyChanged;
        }

        protected void UnsubscribeEvents() {
            if(Owner is INotifyPropertyChanged)
                ((INotifyPropertyChanged)Owner).PropertyChanged += OnOwnerPropertyChanged;
        }

        protected virtual void OnOwnerPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName != Property.Name)
                return;
            Row.OnValueChanged(this);
        }

        public PropertyGridValueRow Row { get; private set; }
        public PropertyDescriptor Property { get { return Row.Property; } }
        public object Owner { get; set; }
        public object Value { get { return Property.GetValue(Owner); } set { Property.SetValue(Owner, value); } }
        public string DisplayValue { get { return Value == null ? "" : Value.ToString(); } set { Value = Convert.ChangeType(value, Property.PropertyType); } }

        public void Clear() {
            UnsubscribeEvents();
        }
    }
}
