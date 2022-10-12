using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(true)]
    public class WfLineSeriesNode : WfChartSeriesNode {
        public override string Type => "Line Series";
        public override string VisualTemplateName => "Line Series";

        [Category("Series Options")]
        public WfLineSeriesViewType ViewType { get; set; } = WfLineSeriesViewType.Line;
        protected override ViewType GetViewType() {
            return (DevExpress.XtraCharts.ViewType)ViewType;
        }
        protected internal override Series CreateSeries() {
            Series s = base.CreateSeries();
            s.ArgumentScaleType = ScaleType.Auto;
            LineSeriesView view = (LineSeriesView)s.View;
            view.Color = LineColor;
            view.AggregateFunction = SeriesAggregateFunction.Average;
            view.LineStyle.Thickness = LineThickness;
            view.LineStyle.DashStyle = (DashStyle)LineStyle;
            view.LineMarkerOptions.BorderColor = MarkerOptions.BorderColor.ToColor();
            view.LineMarkerOptions.BorderVisible = MarkerOptions.ShowBorder;
            view.LineMarkerOptions.Color = MarkerOptions.Color;
            view.LineMarkerOptions.FillStyle.FillMode = FillMode.Solid;
            view.LineMarkerOptions.Size = MarkerOptions.Size;
            view.LineMarkerOptions.Kind = (MarkerKind)MarkerOptions.Kind;
            return s;
        }

        [Browsable(false)]
        [Category("Series Options")]
        public WfColor LineColorCore { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        [XmlIgnore]
        [Category("Series Options")]
        public Color LineColor { get { return LineColorCore.ToColor(); } set { LineColorCore = value.ToWfColor(); } }
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
