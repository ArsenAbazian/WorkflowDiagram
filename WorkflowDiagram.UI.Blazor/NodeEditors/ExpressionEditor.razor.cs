using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using WorkflowDiagram.UI.Blazor.PropertyGridComponent.ValueCellViews;

namespace WorkflowDiagram.UI.Blazor.NodeEditors {
    public partial class ExpressionEditor {
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

        string code = "0";
        public string Code {
            get { return code.Trim(); }
            set {
                if(value == null)
                    value = "0";
                if(Code == value)
                    return;
                code = value.Replace("\n", "\r\n");
                OnCodeChanged();
            }
        }

        public Type GlobalsType {
            get;
            set;
        }

        protected virtual void OnCodeChanged() { }

        protected virtual void OnViewChanged() {
            IGlobalsTypeProvider provider = View.Item.Value.Row.Owner.Objects[0] as IGlobalsTypeProvider;
            GlobalsType = provider == null? View.Item.Value.Row.Owner.Objects[0].GetType(): provider.GetGlobalsType();
            Code = View.Item.Value.Value == null? "": Convert.ToString(View.Item.Value.Value);
        }

        private List<CompilationInfo> Compile() {
            Script<object> res = CSharpScript.Create(Code, ScriptOptions.Default.WithImports("System.Math"), GlobalsType);

            var errorsAndWarnings = res.Compile();
            return errorsAndWarnings.Select(r => new CompilationInfo() {
                Location = r.Location.GetLineSpan().Span.Start,
                TextPosition = r.Location.SourceSpan.Start,
                TextLength = r.Location.SourceSpan.Length,
                Code = r.Id,
                Type = r.Severity,
                Description = r.ToString()
            }).ToList();
        }

        private bool CheckIsExpressionValid() {
            var diag = Compile();
            Errors = diag;

            int warningCount = 0;
            int errorCount = 0;

            foreach(var diagnostic in diag) {
                if(diagnostic.Type == Microsoft.CodeAnalysis.DiagnosticSeverity.Error)
                    errorCount++;
                if(diagnostic.Type == Microsoft.CodeAnalysis.DiagnosticSeverity.Warning)
                    warningCount++;
            }
            if(errorCount > 0) {
                this.message = "Your expression contains errors! Please fix them before.";
                this.messageBox.Title = "Errors detected!";
                this.messageBox.ShowPopup();
                return false;
            }
            else if(warningCount > 0) {
                this.message = "Your expression contains warning!We recommend to fix them.";
                this.messageBox.Title = "Warnings detected.";
                this.messageBox.ShowPopup();
                return true;
            }
            else {
                this.message = "Your expression contains no errros.";
                this.messageBox.Title = "Successfull.";
                this.messageBox.ShowPopup();
                return true;
            }
        }

        internal List<CompilationInfo> Errors { get; set; } = new List<CompilationInfo>();
    }

    public class CompilationInfo {
        public Microsoft.CodeAnalysis.Text.LinePosition Location { get; set; }
        public int TextPosition;
        public int TextLength;
        public string Code { get; set; }
        public Microsoft.CodeAnalysis.DiagnosticSeverity Type { get; set; }
        public string Description { get; set; }
    }

    public interface IGlobalsTypeProvider {
        Type GetGlobalsType();
    }
}
