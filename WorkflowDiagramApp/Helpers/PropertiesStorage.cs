using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WorkflowDiagramApp.Helpers {
    [Serializable]
    [XmlInclude(typeof(PropertyStoreInt))]
    [XmlInclude(typeof(PropertyStoreFloat))]
    [XmlInclude(typeof(PropertyStoreString))]
    [XmlInclude(typeof(PropertyStoreBool))]
    [XmlInclude(typeof(PropertyStoreDouble))]
    public class PropertyStorage : ISupportSerialization {
        Dictionary<string, PropertyStoreBase> items = new Dictionary<string, PropertyStoreBase>();

        [XmlIgnore]
        public object this[string key] {
            get {
                PropertyStoreBase v = null;
                if(this.items.TryGetValue(key, out v))
                    return v.ValueCore;
                return null;
            }
            set {
                PropertyStoreBase v = null;
                if(this.items.TryGetValue(key, out v))
                    v.ValueCore = value;
                PropertyStoreBase s = CreatePropertyStore(value);
                s.Key = key;
                s.ValueCore = value;
                //this.serializableItems = null;
                this.items.Add(key, s);
                if(this.serializableItems != null)
                    this.serializableItems.Add(s);
            }
        }

        public bool Contains(string propertyName) {
            return this.items.ContainsKey(propertyName);
        }

        public void Add(PropertyStoreBase property) {
            PropertyStoreBase v = null;
            if(this.items.TryGetValue(property.Key, out v) && v.GetType() == property.GetType()) {
                v.Description = property.Description;
                v.ValueCore = property.ValueCore;
                return;
            }
            this.items.Add(property.Key, property);
            if(this.serializableItems != null)
                this.serializableItems.Add(property);
        }

        public void SetBool(string key, bool value) {
            this[key] = value;
        }
        public bool GetBool(string key) {
            object res = this[key];
            return res == null? false: (bool)res;
        }

        public void SetInt(string key, bool value) {
            this[key] = value;
        }
        public int GetInt(string key) {
            object res = this[key];
            return res == null ? 0: (int)res;
        }

        public void SetFloat(string key, bool value) {
            this[key] = value;
        }
        public float GetFloat(string key) {
            object res = this[key];
            return res == null ? 0.0f : (float)res;
        }

        public void SetDouble(string key, bool value) {
            this[key] = value;
        }
        public double GetDouble(string key) {
            object res = this[key];
            return res == null ? 0 : (double)res;
        }

        public void SetString(string key, bool value) {
            this[key] = value;
        }
        public string GetString(string key) {
            object res = this[key];
            return res == null ? string.Empty : (string)res;
        }

        private PropertyStoreBase CreatePropertyStore(object value) {
            Type t = value.GetType();
            if(t == typeof(string))
                return new PropertyStoreString();
            if(t == typeof(int))
                return new PropertyStoreInt();
            if(t == typeof(float))
                return new PropertyStoreFloat();
            if(t == typeof(double))
                return new PropertyStoreDouble();
            if(t == typeof(bool))
                return new PropertyStoreBool();
            return new PropertyStoreInt() { Value = 0 };
        }

        public void Remove(PropertyStoreBase property) {
            if(this.items.ContainsKey(property.Key))
                this.items.Remove(property.Key);
            if(this.serializableItems != null)
                this.serializableItems.Remove(property);
        }

        List<PropertyStoreBase> serializableItems;
        public List<PropertyStoreBase> SerializableItems {
            get {
                if(this.serializableItems == null) {
                    this.serializableItems = this.items.Values.ToList();
                }
                return this.serializableItems; 
            }
        }

        public void Save() {
            if(string.IsNullOrEmpty(FullPath))
                return;
            Save(FullPath);
        }
        public void Save(string fullPath) {
            FileName = Path.GetFileName(fullPath);
            if(string.IsNullOrEmpty(FileName))
                return;
            string path = Path.GetDirectoryName(fullPath);
            SerializationHelper.Save(this, typeof(PropertyStorage), fullPath);
        }

        void ISupportSerialization.OnStartDeserialize() {
            this.serializableItems = new List<PropertyStoreBase>();
            this.items.Clear();
        }

        void ISupportSerialization.OnEndDeserialize() {
            foreach(PropertyStoreBase s in this.serializableItems) {
                this.items.Add(s.Key, s);
            }
        }

        internal string FullPath { get; set; }
        [Browsable(false)]
        public string FileName { get; set; }
        string ISupportSerialization.FileName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public abstract class PropertyStoreBase {
        public string Key { get; set; }
        public string Description { get; set; }
        [XmlIgnore]
        public object ValueCore { get; set; }

        public PropertyStoreBase Clone() {
            return (PropertyStoreBase)MemberwiseClone();
        }
        public abstract ScriptPropertyType Type { get; }
    }

    public class PropertyStoreInt : PropertyStoreBase {
        public int Value { get { return ValueCore == null ? 0 : (int)ValueCore; } set { ValueCore = value; } }
        public override ScriptPropertyType Type => ScriptPropertyType.Integer;
    }

    public class PropertyStoreDouble : PropertyStoreBase {
        public double Value { get { return ValueCore == null ? 0 : (double)ValueCore; } set { ValueCore = value; } }
        public override ScriptPropertyType Type => ScriptPropertyType.Double;
    }

    public class PropertyStoreString : PropertyStoreBase {
        public string Value { get { return (string)ValueCore; } set { ValueCore = value; } }
        public override ScriptPropertyType Type => ScriptPropertyType.String;
    }

    public class PropertyStoreFloat : PropertyStoreBase {
        public float Value { get { return ValueCore == null ? 0.0f : (float)ValueCore; } set { ValueCore = value; } }
        public override ScriptPropertyType Type => ScriptPropertyType.Float;
    }

    public class PropertyStoreBool : PropertyStoreBase {
        public bool Value { get { return ValueCore == null? false: (bool)ValueCore; } set { ValueCore = value; } }
        public override ScriptPropertyType Type => ScriptPropertyType.Boolean;
    }

    public enum ScriptPropertyType {
        Boolean,
        Integer,
        Float,
        Double,
        String
    }
}
