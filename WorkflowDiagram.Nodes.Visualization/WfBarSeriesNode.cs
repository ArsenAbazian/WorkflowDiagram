using System.ComponentModel;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(true)]
    public class WfBarSeriesNode : WfChartSeriesNode {
        public override string Type => "Bar Series";
        public override string VisualTemplateName => "Bar Series";

        [Category("Series Options")]
        public WfBarSeriesViewType ViewType { get; set; } = WfBarSeriesViewType.Bar;
        protected override WfChartSeriesViewType GetViewType() {
            return (WfChartSeriesViewType)ViewType;
        }
        
        [Category("Series Options")]
        public WfColor Color { get; set; } = WfColor.FromArgb(255, 0, 255, 0);

        [Category("Series Options")]
        public WfColor LineColor { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        
        [Category("Series Options")]
        public int LineThickness { get; set; } = 1;

        [Category("Series Options")]
        public WfFillMode FillMode { get; set; } = WfFillMode.Solid;
        [Category("Series Options")]
        public int Transparency { get; set; } = 0;

        [Category("Series Options")]
        public double BarWidth { get; set; } = 20;
        [Category("Series Options")]
        public bool ColorEach { get; set; } = false;
    }

    public enum WfBarSeriesViewType {
        Bar = 0,
        StackedBar = 1,
        FullStackedBar = 2,
        SideBySideStackedBar = 3,
        SideBySideFullStackedBar = 4,
    }
}
