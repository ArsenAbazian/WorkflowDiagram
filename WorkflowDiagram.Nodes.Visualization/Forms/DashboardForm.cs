using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010.Views.Widget;
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

namespace WokflowDiagram.Nodes.Visualization.Forms {
    public partial class DashboardForm : XtraForm {
        public DashboardForm(WfDashboardFormNode node) : this() {
            Node = node;
        }
        public DashboardForm() {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            this.dashboardContainerControl1.OnFormLoad(this);
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.dashboardContainerControl1.OnFormShown(this);
        }

        public WfDashboardFormNode Node { get => this.dashboardContainerControl1.Node; set => this.dashboardContainerControl1.Node = value; }
        public WidgetView WidgetView { get => this.dashboardContainerControl1.WidgetView; }
    }
}
