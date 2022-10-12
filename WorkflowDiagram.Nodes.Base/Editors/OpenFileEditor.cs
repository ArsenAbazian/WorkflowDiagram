using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base.Editors {
    public class RepositoryItemOpenFileEditor : RepositoryItemButtonEdit {
        public RepositoryItemOpenFileEditor() {
            ButtonClick += OnOpenFileButtonClick;
        }

        private void OnOpenFileButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            XtraOpenFileDialog dlg = new XtraOpenFileDialog();
            dlg.Filter = "Workflow Xml Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if(dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            if(OwnerEdit != null)
                OwnerEdit.EditValue = dlg.FileName;
        }
    }
}
