using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram {
    public static class SerializationHelper {
        public static void AssignValueProperties(object src, object dst) {
            PropertyInfo[] props = src.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var prop in props) {
                if(!prop.CanRead)
                    continue;
                if(prop.PropertyType.IsValueType || prop.PropertyType.IsEnum || prop.PropertyType == typeof(string)) {
                    if(prop.CanWrite && prop.GetAccessors().Length == 2)
                        prop.SetValue(dst, prop.GetValue(src, null));
                }
            }
        }
        public static bool Load(ISupportSerialization obj, Type t, string fileName) {
            object res = FromFile(fileName, t);
            if(res == null)
                return false;

            obj.OnStartDeserialize();
            PropertyInfo[] props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var prop in props) {
                if(!prop.CanRead)
                    continue;
                if(prop.GetCustomAttribute(typeof(XmlIgnoreAttribute)) != null)
                    continue;
                if(prop.CanWrite && prop.GetAccessors().Length == 2) {
                    prop.SetValue(obj, prop.GetValue(res, null));
                }
                else {
                    object value = prop.GetValue(res, null);
                    if(value is IList) {
                        IList srcList = (IList)value;
                        IList dstList = (IList)prop.GetValue(obj, null);
                        dstList.Clear();
                        for(int i = 0; i < srcList.Count; i++) {
                            dstList.Add(srcList[i]);
                        }
                    }
                    else if(value is IDictionary) {
                        IDictionary srcDict = (IDictionary)value;
                        IDictionary dstDict = (IDictionary)prop.GetValue(obj, null);
                        dstDict.Clear();
                        foreach(object key in srcDict.Keys) {
                            dstDict.Add(key, srcDict[key]);
                        }
                    }
                }
            }
            obj.OnEndDeserialize();
            return true;
        }
        public static ISupportSerialization FromFile(string fileName, Type t) {
            if(string.IsNullOrEmpty(fileName))
                return null;
            if(!File.Exists(fileName))
                return null;
            var extra = GetExtraTypes(t);
            XmlSerializer formatter = new XmlSerializer(t, extra);
            try {
                ISupportSerialization obj = null;
                using(FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate)) {
                    obj = (ISupportSerialization)formatter.Deserialize(fs);
                }
                obj.FileName = fileName;
                obj.OnEndDeserialize();
                return obj;
            }
            catch(Exception) {
                return null;
            }
        }

        public static Type[] GetExtraTypes(Type t) {
            var allow = t.GetCustomAttribute<AllowDynamicTypesAttribute>();
            if(allow == null || !allow.Allow)
                return new Type[0];
            var attr = t.GetCustomAttributes<XmlIncludeAttribute>().ToList();
            if(attr.Count == 0)
                return new Type[0];
            var asms = Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList(); // AppDomain.CurrentDomain.GetAssemblies();
            asms.Add(Assembly.GetEntryAssembly().GetName());
            List<Type> extra = new List<Type>();
            foreach(var aname in asms) {
                try {
                    if(aname.Name.StartsWith("DevExpress"))
                        continue;
                    Assembly assembly = Assembly.Load(aname);
                    foreach(Type tp in assembly.GetTypes()) {
                        if(!tp.IsClass && tp.IsAbstract)
                            continue;
                        foreach(var a in attr) {
                            if(a.Type.IsAssignableFrom(tp))
                                extra.Add(tp);
                        }
                    }
                }
                catch(Exception) { }
            }
            return extra.ToArray();
        }

        public static bool Save(ISupportSerialization obj, Type t, string fullName) {
            string path = Path.GetDirectoryName(fullName);
            string file = Path.GetFileName(fullName);
            string tmpFile = Path.GetFileNameWithoutExtension(fullName) + ".tmp";
            if(!string.IsNullOrEmpty(path))
                tmpFile = path + "\\" + tmpFile;

            if(string.IsNullOrEmpty(file))
                return false;
            try {
                XmlSerializer formatter = new XmlSerializer(t, GetExtraTypes(t));
                using(FileStream fs = new FileStream(tmpFile, FileMode.Create)) {
                    formatter.Serialize(fs, obj);
                }
                if(File.Exists(fullName))
                    File.Delete(fullName);
                File.Move(tmpFile, fullName);
            }
            catch(Exception) {
                return false;
            }

            return true;
        }
    }

    public interface ISupportSerialization {
        string FileName { get; set; }
        void OnStartDeserialize();
        void OnEndDeserialize();
    }

    public class AllowDynamicTypesAttribute : Attribute { 
        public AllowDynamicTypesAttribute() : this(true) { }
        public AllowDynamicTypesAttribute(bool allow) {
            Allow = allow;
        }
        public bool Allow { get; private set; }
    }
}
