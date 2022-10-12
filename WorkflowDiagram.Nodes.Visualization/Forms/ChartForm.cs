using DevExpress.XtraCharts;
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
    public partial class ChartForm : XtraForm {
        public ChartForm() {
            InitializeComponent();
        }

        public IChartNode Node { get => this.chartUserControl1.Node; set => this.chartUserControl1.Node = value; }
        public ChartControl ChartControl { get => this.chartUserControl1.ChartControl; }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.chartUserControl1.OnFormShown(this);
        }
    }
}
