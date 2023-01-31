using System.Collections.Generic;
using System.ComponentModel;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(true)]
    public class WfChartPanelNode : WfDashboardPanelNode, IChartNode {
        public override string VisualTemplateName => "Chart Panel";

        public override string Type => "Chart Panel";

        public bool Rotated { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<WfDiagramPane> Panes { get; set; } = new List<WfDiagramPane>();

        public int PaneDistance { get; set; } = 10;
        public WfChartPaneLayoutDirection PaneLayoutDirection { get; set; } = WfChartPaneLayoutDirection.Vertical;

        object IChartNode.SeriesSource { get { return Inputs["In"].Value; } }
        protected IWfPlatformChartService ChartService { get; set; }

        protected override object CreateVisualizationControl(object seriesSource) {
            ChartService = Document.PlatformServices.GetService<IWfPlatformChartService>(this);
            object control = ChartService.CreateChartUserControl(this);
            ChartService.InitializeChart(this, control);
            return control;
        }

        public override string ToString() {
            return "Chart Panel";
        }
    }
}
