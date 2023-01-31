using System.ComponentModel;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(true)]
    public class WfLineSeriesNode : WfChartSeriesNode {
        public override string Type => "Line Series";
        public override string VisualTemplateName => "Line Series";

        [Category("Series Options")]
        public WfLineSeriesViewType ViewType { get; set; } = WfLineSeriesViewType.Line;
        protected override WfChartSeriesViewType GetViewType() {
            return (WfChartSeriesViewType)ViewType;
        }
        
        [Category("Series Options")]
        public WfColor LineColor { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        [Category("Series Options")]
        public int LineThickness { get; set; } = 1;
        [Category("Series Options")]
        public WfDashStyle LineStyle { get; set; } = WfDashStyle.Solid;
        [Category("Series Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public WfSeriesMarkerOptions MarkerOptions { get; set; } = new WfSeriesMarkerOptions();
    }

    public enum WfLineSeriesViewType {
        Line = 11,
        StackedLine = 12,
        FullStackedLine = 13,
        StepLine = 14,
        Spline = 15,
        ScatterLine = 16,
        SwiftPlot = 17
    }
}
