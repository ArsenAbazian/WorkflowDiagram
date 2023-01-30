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
    public class WfAreaSeriesNode : WfChartSeriesNode {
        public override string Type => "Area Series";
        public override string VisualTemplateName => "Area Series";

        [Category("Series Options")]
        public WfAreaSeriesViewType ViewType { get; set; } = WfAreaSeriesViewType.Area;
        protected override WfChartSeriesViewType GetViewType() {
            return (WfChartSeriesViewType)ViewType;
        }
        //WINFORM
        //protected internal override object CreateSeries() {
        //    object s = base.CreateSeries();
        //    s.ArgumentScaleType = ScaleType.Auto;
        //    AreaSeriesView view = (AreaSeriesView)s.View;
        //    view.Color = Color;
        //    view.AggregateFunction = SeriesAggregateFunction.Average;
        //    view.Border.Thickness = LineThickness;
        //    view.Border.Color = LineColor;
        //    view.MarkerOptions.BorderColor = MarkerOptions.BorderColor.ToColor();
        //    view.MarkerOptions.BorderVisible = MarkerOptions.ShowBorder;
        //    view.MarkerOptions.Color = MarkerOptions.Color;
        //    view.MarkerOptions.FillStyle.FillMode = (DevExpress.XtraCharts.FillMode)WfFillMode.Solid;
        //    view.MarkerOptions.Size = MarkerOptions.Size;
        //    view.MarkerOptions.Kind = (MarkerKind)MarkerOptions.Kind;
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
        [Category("Series Options"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public WfSeriesMarkerOptions MarkerOptions { get; set; } = new WfSeriesMarkerOptions();

        [Category("Series Options")]
        public WfFillMode FillMode { get; set; } = WfFillMode.Solid;
        [Category("Series Options")]
        public int Transparency { get; set; } = 0;
        [Category("Series Options")]
        public bool ColorEach { get; set; } = false;
    }

    public enum WfFillMode {
        Empty = 0,
        Solid = 1,
        //Gradient = 2,
        //Hatch = 3
    }

    public enum WfAreaSeriesViewType {
        Area = 18,
        StepArea = 19,
        SplineArea = 20,
        StackedArea = 21,
        StackedStepArea = 22,
        StackedSplineArea = 23,
        FullStackedArea = 24,
        FullStackedSplineArea = 25,
        FullStackedStepArea = 26,
        RangeArea = 27,
    }
}
