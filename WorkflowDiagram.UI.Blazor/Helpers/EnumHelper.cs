using System.ComponentModel;
using System.Reflection;

namespace WorkflowDiagram.UI.Blazor.Helpers {
    public static class EnumHelper {
        public static List<Enum> GetValues(Type enumType) {
            List<Enum> enumValues = new List<Enum>();
            var values = Enum.GetValues(enumType);
            foreach(object value in values) {
                enumValues.Add((Enum)value);
            }
            return enumValues;
        }
        public static List<EnumInfo> GetItems(Type enumType) {
            var values = Enum.GetValues(enumType);
            List<EnumInfo> list = new List<EnumInfo>(values.Length);
            foreach(object value in values) {
                string name = Enum.GetName(enumType, value);
                string desc = GetEnumDisplayName(enumType, (Enum)value);
                string text = string.IsNullOrEmpty(desc) ? name : desc;
                list.Add(new EnumInfo() { Text = text, Value = (Enum)value });
            }
            return list;
        }
        public static List<EnumInfo> GetItems(this Enum en) {
            Type enumType = en.GetType();
            return GetItems(enumType);
        }

        public static string GetEnumDisplayName(Type enumType, Enum value) {
            FieldInfo fi = enumType.GetField(value.ToString());
            if(fi == null)
                return "Undefined Enum Value";
            DescriptionAttribute attr = fi.GetCustomAttribute<DescriptionAttribute>();
            if(attr == null)
                return "";
            return attr.Description;
        }
    }

    public class EnumInfo {
        public string Text { get; set; }
        public Enum Value { get; set; }
    }
}
