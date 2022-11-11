using Microsoft.AspNetCore.Components;
using WorkflowDiagram.Nodes.Base;
using WorkflowDiagram.UI.Blazor.PropertyGridComponent;
using WorkflowDiagram.UI.Blazor.PropertyGridComponent.ValueCellViews;

namespace WorkflowDiagram.UI.Blazor.NodeEditors {
    public partial class ObjectValueEditor {
        public ObjectValueEditor() {
        }

        double doubleValue;
        internal double DoubleValue {
            get { return doubleValue; }
            set {
                if(DoubleValue == value)
                    return;
                doubleValue = value;
                UpdateValue();
            }
        }

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

        protected virtual void OnViewChanged() {
            Value = View.Item.Value.Value;
        }

        protected bool SuppressUpdateValue { get; set; }
        protected virtual void UpdateValue() {
            if(SuppressUpdateValue)
                return;
            SuppressUpdateValue = true;
            try {
                if(ValueType == WfValueType.Decimal)
                    Value = DoubleValue;
                else if(ValueType == WfValueType.Boolean)
                    Value = BooleanValue;
                else if(ValueType == WfValueType.String)
                    Value = StringValue;
                else if(ValueType == WfValueType.DateTime)
                    Value = DateTimeValue;
            }
            finally {
                SuppressUpdateValue = false;
            }
        }

        bool booleanValue;
        internal bool BooleanValue {
            get { return booleanValue; }
            set {
                if(BooleanValue == value)
                    return;
                booleanValue = value;
                UpdateValue();
            }
        }
        string strValue;
        internal string StringValue {
            get { return strValue; }
            set {
                if(StringValue == value)
                    return;
                strValue = value;
                UpdateValue();
            }
        }
        DateTime dtValue;
        internal DateTime DateTimeValue {
            get { return dtValue; }
            set {
                if(DateTimeValue == value)
                    return;
                dtValue = value;
                UpdateValue();
            }
        }

        internal object ValueTypeCore { get { return ValueType; } set { ValueType = (WfValueType)value; } }

        WfValueType valueType = WfValueType.Decimal;
        public WfValueType ValueType {
            get { return valueType; }
            set {
                if(ValueType == value)
                    return;
                valueType = value;
                UpdateValue();
                UpdateValues();
            }
        }

        protected virtual void UpdateValues() {
            if(SuppressUpdateValue)
                return;
            SuppressUpdateValue = true;
            try {
                if(ValueType == WfValueType.Decimal)
                    DoubleValue = Convert.ToDouble(Value);
                else if(ValueType == WfValueType.Boolean)
                    BooleanValue = Convert.ToBoolean(Value);
                else if(ValueType == WfValueType.String)
                    StringValue = Convert.ToString(Value);
                else if(ValueType == WfValueType.DateTime)
                    DateTimeValue = Convert.ToDateTime(Value);
            }
            finally {
                SuppressUpdateValue = false;
                InvokeAsync(StateHasChanged);
            }
        }

        object val;
        public object Value {
            get { return val; }
            set {
                if(Value == value)
                    return;
                val = value;
                UpdateValueType();
                UpdateValues();
            }
        }

        protected virtual void UpdateValueType() {
            if(Value == null)
                ValueType = WfValueType.Decimal;
            else if(Value is double || Value is float || Value is int)
                ValueType = WfValueType.Decimal;
            else if(Value is bool)
                ValueType = WfValueType.Boolean;
            else if(Value is string)
                ValueType = WfValueType.String;
        }

        public string NumericVisibilityClass { get { return ValueType == WfValueType.Decimal ? "" : "collapsed"; } }
        public string BooleanVisibilityClass { get { return ValueType == WfValueType.Boolean ? "" : "collapsed"; } }
        public string StringVisibilityClass { get { return ValueType == WfValueType.String ? "" : "collapsed"; } }
        public string DateTimeVisibilityClass { get { return ValueType == WfValueType.DateTime ? "" : "collapsed"; } }
    }
}
