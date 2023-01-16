using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Reflection;
using WorkflowDiagram.UI.Blazor.Components;

namespace WorkflowDiagram.UI.Blazor.NodeEditors {
    public partial class CollectionEditorDialog {
        public CollectionEditorDialog() {
            Collection = new List<object>();
        }

        protected Type ItemType { get; set; }

        IList sourceCollection;
        public IList SourceCollection {
            get { return sourceCollection; }
            set {
                if(SourceCollection == value)
                    return;
                sourceCollection = value;
                OnSourceCollectionChanged();
            }
        }

        private void OnSourceCollectionChanged() {
            Type tp = SourceCollection.GetType();
            if(!tp.IsGenericType)
                tp = tp.BaseType;
            if(!tp.IsGenericType) {
                ItemType = typeof(object);
            }
            else
                ItemType = tp.GetGenericArguments().Single();
            Mappings = new Dictionary<object, object>();
            List<object> items = new List<object>();
            if(SourceCollection != null) {
                foreach(var item in SourceCollection) {
                    ICloneable cloneable = item as ICloneable;
                    if(cloneable != null) {
                        var cloned = cloneable.Clone();
                        items.Add(cloned);
                        Mappings.Add(cloneable, cloned);
                    }
                    else {
                        Mappings.Add(item, item);
                        items.Add(item);
                    }
                }
            }
            Collection = items;
        }

        protected Dictionary<object, object> Mappings { get; set; }

        IList collection;
        public IList Collection {
            get { return collection; }
            set {
                if(Collection == value)
                    return;
                collection = value;
                OnCollectionChanged();
            }
        }

        protected virtual void OnCollectionChanged() {
        }

        protected virtual void SyncCollections() {
            if(SourceCollection == null)
                return;
            List<object> prev = new List<object>();
            foreach(object item in SourceCollection)
                prev.Add(item);

            SourceCollection.Clear();   
            foreach(var item in Collection) {
                object exisitngItem = null;
                Mappings.TryGetValue(item, out exisitngItem);
                if(exisitngItem != null) {
                    Assign(exisitngItem, item);
                    SourceCollection.Add(exisitngItem);
                }
                else
                    SourceCollection.Add(item);
            }
        }

        private void Assign(object dst, object source) {
            PropertyInfo[] props = source.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            for(int i = 0; i < props.Length; i++) {
                object val = props[i].GetValue(source);
                if(props[i].CanWrite)
                    props[i].SetValue(dst, val);
            }
        }
    }
}
