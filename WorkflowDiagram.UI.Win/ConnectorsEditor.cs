using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkflowDiagram.UI.Win {
    public partial class ConnectorsEditor : XtraUserControl {
        public ConnectorsEditor() {
            InitializeComponent();
        }

        WfConnector GetFocusedConnector() {
            return (WfConnector)this.gridView1.GetFocusedRow();
        }

        private void biMoveUp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            WfConnector sel = GetFocusedConnector();
            sel.From.Connectors.MoveUp(sel);
            this.gridView1.RefreshData();
            int index = sel.From.Connectors.IndexOf(sel);
            this.gridView1.FocusedRowHandle = this.gridView1.GetRowHandle(index);
        }

        private void biMoveDown_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            WfConnector sel = GetFocusedConnector();
            sel.From.Connectors.MoveDown(sel);
            this.gridView1.RefreshData();
            int index = sel.From.Connectors.IndexOf(sel);
            this.gridView1.FocusedRowHandle = this.gridView1.GetRowHandle(index);
        }
    }
}
