using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Editors {
    public interface IPropertyEditor {
        void Initialize(object owner, string property, object value);
    }
}
