using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class PropertyEditorAttribute : Attribute {
        public PropertyEditorAttribute(Type editorType) {
            EditorType = editorType;
        }
        public PropertyEditorAttribute(string assemblyName, string typeName) {
            AssemblyName = assemblyName;
            TypeName = typeName;
        }
        Type editorType;
        public Type EditorType {
            get {
                if(editorType == null)
                    editorType = LoadEditor();
                return editorType; 
            }
            set {
                editorType = value;
            }
        }

        private Type LoadEditor() {
            if(string.IsNullOrEmpty(AssemblyName) || string.IsNullOrEmpty(TypeName))
                return null;
            try {
                Assembly asm = Assembly.Load(AssemblyName);
                if(asm == null)
                    return null;
                return asm.GetType(TypeName);
            }
            catch(Exception) {
                return null;
            }
        }

        public string AssemblyName { get; private set; }
        public string TypeName { get; private set; }
    }

    public class WinPropertyEditorAttribute : PropertyEditorAttribute {
        public WinPropertyEditorAttribute(Type editorType) : base(editorType) { }
        public WinPropertyEditorAttribute(string assemblyName, string typeName) : base(assemblyName, typeName) { }
    }

    public class BlazorPropertyEditorAttribute : PropertyEditorAttribute {
        public BlazorPropertyEditorAttribute(Type editorType) : base(editorType) { }
        public BlazorPropertyEditorAttribute(string assemblyName, string typeName) : base(assemblyName, typeName) { }
    }
}
