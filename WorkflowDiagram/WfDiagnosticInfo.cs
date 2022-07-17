using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class WfDiagnosticInfo {
        public WfDiagnosticSeverity Type { get; set; }
        public string Text { get; set; }
    }

    public enum WfDiagnosticSeverity { 
        Info = 0,
        Warning = 1,
        Error = 2
    } 
}
