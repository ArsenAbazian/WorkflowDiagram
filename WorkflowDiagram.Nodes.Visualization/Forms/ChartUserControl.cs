using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization.Managers;
using WokflowDiagram.Nodes.Visualization.Utils;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization.Forms {
    public partial class ChartUserControl : XtraUserControl, IWfDashboardControl {
        public ChartUserControl() {
            InitializeComponent();
        }

        public ChartControl ChartControl { get { return this.chartControl1; } }
        public IChartNode Node { get; set; }
        protected Dictionary<string, object> DataSources { get; } = new Dictionary<string, object>();

        void IWfDashboardControl.OnInitialized() {
            InitializeChart();
        }

        protected virtual void InitializeChart() {
            ChartVisualizationManager.Default.InitializeChart(Node, ChartControl);
            foreach(Series s in ChartControl.Series) {
                DataSources.Add(s.Name, s.DataSource);
            }
        }

        protected internal virtual void OnFormShown(ChartForm chartForm) {
            
        }

        void IWfDashboardControl.OnApplyWorkspace() {
            foreach(var pair in DataSources) {
                Series s = ChartControl.GetSeriesByName(pair.Key);
                if(s != null)
                    s.DataSource = pair.Value;
            }
        }
    }
}
