using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Browsable(false)]
        [Category("Series Options")]
        public WfColor Color { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        
        [Browsable(false)]
        [Category("Series Options")]
        public WfColor LineColor { get; set; } = WfColor.FromArgb(255, 0, 255, 0);

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
