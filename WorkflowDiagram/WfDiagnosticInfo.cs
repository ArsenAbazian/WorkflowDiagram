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

    public class WfDiagnosticHelper {
        static WfDiagnosticHelper defaultHelper;
        public static WfDiagnosticHelper Default {
            get {
                if(defaultHelper == null)
                    defaultHelper = new WfDiagnosticHelper();
                return defaultHelper;
            }
        }

        public List<WfDiagnosticInfo> Diagnostics { get; } = new List<WfDiagnosticInfo>();

        public void Clear() {
            Diagnostics.Clear();
            HasErrors = false;
        }

        public void Add(WfDiagnosticSeverity type, string text) {
            Diagnostics.Add(new WfDiagnosticInfo() { Type = type, Text = text });
        }

        public void Error(string text) {
            Add(WfDiagnosticSeverity.Error, text);
            HasErrors = true;
        }

        public void Warning(string text) {
            Add(WfDiagnosticSeverity.Warning, text);
        }

        public void Info(string text) {
            Add(WfDiagnosticSeverity.Info, text);
        }

        public bool HasErrors { get; set; }
    }
}
