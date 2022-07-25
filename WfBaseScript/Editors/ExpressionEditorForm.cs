using DevExpress.XtraEditors;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfBaseScript.Editors {
    public partial class ExpressionEditorForm : XtraForm {
        public ExpressionEditorForm() {
            InitializeComponent();
        }

        public Type GlobalsType {
            get;
            set;
        }

        public string Expression {
            get { return this.memoEdit1.Text.Trim(); }
            set { this.memoEdit1.Text = value.Replace("\n", "\r\n"); }
        }

        private void sbOk_Click(object sender, EventArgs e) {
            if(!CheckIsExpressionValid())
                return;

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool CheckIsExpressionValid() {
            var diag = Compile();
            this.gridControl1.DataSource = diag;

            int warningCount = 0;
            int errorCount = 0;

            foreach(var diagnostic in diag) {
                if(diagnostic.Type == Microsoft.CodeAnalysis.DiagnosticSeverity.Error)
                    errorCount++;
                if(diagnostic.Type == Microsoft.CodeAnalysis.DiagnosticSeverity.Warning)
                    warningCount++;
            }
            if(errorCount > 0) {
                XtraMessageBox.Show("Your expression contains errors! Please fix them before.", "Errors");
                return false;
            }
            if(warningCount > 0) {
                if(XtraMessageBox.Show("Your expression contains warning! We recommend to fix them.", "Warnings", MessageBoxButtons.YesNoCancel) != DialogResult.No)
                    return false;
            }
            return true;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            var diag = Compile();
            this.gridControl1.DataSource = diag;
            if(diag.Count == 0)
                XtraMessageBox.Show("Expression compiled successfully!", "Succesfull");
        }

        private List<CompilationInfo> Compile() {
            Script<object> res = CSharpScript.Create(Expression, ScriptOptions.Default.WithImports("System.Math"), GlobalsType);

            var errorsAndWarnings = res.Compile();
            return errorsAndWarnings.Select(r => new CompilationInfo() {
                Location = r.Location.GetLineSpan().Span.Start,
                TextPosition = r.Location.SourceSpan.Start,
                TextLength = r.Location.SourceSpan.Length,
                Code = r.Id,
                Type = r.Severity,
                Description = r.ToString() }).ToList();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            CompilationInfo info = (CompilationInfo)this.gridView1.GetFocusedRow();
            if(info != null) {
                this.memoEdit1.SelectionStart = info.TextPosition;
                this.memoEdit1.SelectionLength = info.TextLength;
            }
        }
    }

    public class CompilationInfo {
        public Microsoft.CodeAnalysis.Text.LinePosition Location { get; set; }
        public int TextPosition;
        public int TextLength;
        public string Code { get; set; }
        public Microsoft.CodeAnalysis.DiagnosticSeverity Type { get; set; }
        public string Description { get; set; }
    }
}
