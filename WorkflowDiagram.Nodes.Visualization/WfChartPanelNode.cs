using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization.Forms;
using WokflowDiagram.Nodes.Visualization.Managers;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(true)]
    public class WfChartPanelNode : WfDashboardPanelNode, IChartNode {
        public override string VisualTemplateName => "Chart Panel";

        public override string Type => "Chart Panel";

        public bool Rotated { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<WfDiagramPane> Panes { get; set; } = new List<WfDiagramPane>();

        public int PaneDistance { get; set; } = 10;
        public PaneLayoutDirection PaneLayoutDirection { get; set; } = PaneLayoutDirection.Vertical;

        object IChartNode.SeriesSource { get { return Inputs["In"].Value; } }
        protected override Control CreateVisualizationControl(object seriesSource) {
            ChartUserControl control = new ChartUserControl();
            ChartVisualizationManager.Default.InitializeChart(this, control.ChartControl);
            return control;
        }

        public override string ToString() {
            return "Chart Panel";
        }
    }
}
