using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class PropertyEditorAttribute : Attribute {
        public PropertyEditorAttribute(Type editorType) {
            EditorType = editorType;
        }
        public Type EditorType { get; private set; }
    }
}
