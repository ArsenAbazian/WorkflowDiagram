using System.ComponentModel;
using System.Drawing;
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
        //WINFORM
        //protected internal override object CreateSeries() {
        //    object s = base.CreateSeries();
        //    s.ArgumentScaleType = ScaleType.Auto;
        //    BarSeriesView view = (BarSeriesView)s.View;
        //    view.Color = Color;
        //    view.AggregateFunction = SeriesAggregateFunction.Average;
        //    view.Border.Thickness = LineThickness;
        //    view.Border.Color = LineColor;
        //    view.BarWidth = BarWidth;
        //    view.ColorEach = ColorEach;
        //    view.FillStyle.FillMode = (FillMode)FillMode;
        //    view.Transparency = (byte)Transparency;
        //    return s;
        //}

        [Browsable(false)]
        [Category("Series Options")]
        public WfColor ColorCore { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        [XmlIgnore]
        [Category("Series Options")]
        public Color Color { get { return ColorCore.ToColor(); } set { ColorCore = value.ToWfColor(); } }

        [Browsable(false)]
        [Category("Series Options")]
        public WfColor LineColorCore { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        [XmlIgnore]
        [Category("Series Options")]
        public Color LineColor { get { return LineColorCore.ToColor(); } set { LineColorCore = value.ToWfColor(); } }

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
