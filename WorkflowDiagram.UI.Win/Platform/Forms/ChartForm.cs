using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WokflowDiagram.Nodes.Visualization.Forms
{
    public partial class ChartForm : XtraForm, IWfChartForm {
        public ChartForm() {
            InitializeComponent();
        }

        public IChartNode Node { get => this.chartUserControl1.Node; set => this.chartUserControl1.Node = value; }
        public object DataSource { get => this.chartUserControl1.ChartControl.DataSource; set => this.chartUserControl1.ChartControl.DataSource = value; }
        
        public ChartControl ChartControl { get => this.chartUserControl1.ChartControl; }
        

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            this.chartUserControl1.OnFormShown(this);
        }
    }
}
