using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization.Managers;

namespace WokflowDiagram.Nodes.Visualization.Forms {
    public partial class TableForm : XtraForm {
        public TableForm() {
            InitializeComponent();
        }

        public ITableNode Node { get => this.tableUserControl1.Node; set => tableUserControl1.Node = value; }
        public object DataSource { get => this.tableUserControl1.DataSource; set => tableUserControl1.DataSource = value; }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            TableVisualizationManager.Default.InitializeTable(Node, this.tableUserControl1);
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.tableUserControl1.OnFormShown(this);
        }
    }
}
