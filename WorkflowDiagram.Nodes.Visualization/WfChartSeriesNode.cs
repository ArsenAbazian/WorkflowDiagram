using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(false)]
    public class WfChartSeriesNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Chart Series";
        public override string Type => "Chart Series";
        public override string Category => "Visualization";

        static int SeriesIndex = 0;
        public WfChartSeriesNode() {
            SeriesName = "Series" + GetNextSeriesIndex(); 
        }

        private static int GetNextSeriesIndex() {
            SeriesIndex++;
            return SeriesIndex;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In", Text = "Data In", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Series", Text = "Series", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            ChartService = Document.PlatformServices.GetService<IWfPlatformChartService>(this);
            IDisposable ds = PlatformImplSeries as IDisposable;
            if(ds != null)
                ds.Dispose();
            PlatformImplSeries = null;
            if(string.IsNullOrEmpty(ArgumentDataMember)) {
                OnError("ArgumentDataMember must be specified");
                return false;
            }
            if(string.IsNullOrEmpty(ValueDataMember)) {
                OnError("ValueDataMember must be specified");
                return false;
            }
            return true;
        }

        protected IWfPlatformChartService ChartService { get; set; }

        protected internal virtual object CreateSeries() {
            if(PlatformImplSeries != null)
                return PlatformImplSeries;
            WfChartSeriesViewType viewType = GetViewType();
            object s = CreateSeriesCore(SeriesName, viewType);
            PlatformImplSeries = s;
            return s;
        }

        static int creationIndex = 0;
        protected virtual object CreateSeriesCore(string name, WfChartSeriesViewType viewType) {
            if(name == null)
                name = viewType.ToString() + GetCreationIndex();
            return ChartService.CreateSeries(this, name, viewType);
        }

        private static string GetCreationIndex() {
            creationIndex++;
            return string.Format(" {0}", creationIndex);
        }

        protected virtual WfChartSeriesViewType GetViewType() {
            return WfChartSeriesViewType.Line;
        }

        protected virtual object PlatformImplSeries { get; set; }
        protected internal object DataSource { get; set; }
        protected override void OnVisitCore(WfRunner runner) {
            object dataSource = Inputs["In"].Value;
            DataSource = dataSource;
            DataContext = this;
            Outputs["Series"].Visit(runner, this);
        }
        
        [Category("Data Members")]
        public virtual string ArgumentDataMember { get; set; }
        [Category("Data Members")]
        public virtual string ValueDataMember { get; set; }
        
        [Category("Series Options")]
        public string SeriesName { get; set; }
        [Category("Series Options")]
        public string PaneName { get; set; }

        public override string ToString() {
            return SeriesName + " Series";
        }

        [Category("Data Members")]
        public WfDateTimeMeasureUnit ArgumentMeauseUnit { get; set; } = WfDateTimeMeasureUnit.Minute;
        [Category("Data Members")]
        public int MeasureUnitMultiplier { get; set; } = 1;
    }
    
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class WfSeriesMarkerOptions {
        public WfColor BorderColor { get; set; }
        public bool ShowBorder { get; set; }
        public WfColor Color { get; set; }
        public WfMarkerKind Kind { get; set; }
        public int Size { get; set; } = 10;
    }

    public enum WfMarkerKind {
        Square = 0,
        Diamond = 1,
        Triangle = 2,
        InvertedTriangle = 3,
        Circle = 4,
        Plus = 5,
        Cross = 6,
        Star = 7,
        Pentagon = 8,
        Hexagon = 9,
        ThinCross = 10
    }

    public enum WfDashStyle {
        Empty = 0,
        Solid = 1,
        Dash = 2,
        Dot = 3,
        DashDot = 4,
        DashDotDot = 5
    }

    public enum WfChartSeriesViewType {
        Bar = 0,
        StackedBar = 1,
        FullStackedBar = 2,
        SideBySideStackedBar = 3,
        SideBySideFullStackedBar = 4,
        Pie = 5,
        Doughnut = 6,
        NestedDoughnut = 7,
        Funnel = 8,
        Point = 9,
        Bubble = 10,
        Line = 11,
        StackedLine = 12,
        FullStackedLine = 13,
        StepLine = 14,
        Spline = 15,
        ScatterLine = 16,
        SwiftPlot = 17,
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
        Stock = 28,
        CandleStick = 29,
        SideBySideRangeBar = 30,
        RangeBar = 31,
        SideBySideGantt = 32,
        Gantt = 33,
        PolarPoint = 34,
        PolarLine = 35,
        ScatterPolarLine = 36,
        PolarArea = 37,
        PolarRangeArea = 38,
        RadarPoint = 39,
        RadarLine = 40,
        ScatterRadarLine = 41,
        RadarArea = 42,
        RadarRangeArea = 43,
        Bar3D = 44,
        StackedBar3D = 45,
        FullStackedBar3D = 46,
        ManhattanBar = 47,
        SideBySideStackedBar3D = 48,
        SideBySideFullStackedBar3D = 49,
        Pie3D = 50,
        Doughnut3D = 51,
        Funnel3D = 52,
        Line3D = 53,
        StackedLine3D = 54,
        FullStackedLine3D = 55,
        StepLine3D = 56,
        Area3D = 57,
        StackedArea3D = 58,
        FullStackedArea3D = 59,
        StepArea3D = 60,
        Spline3D = 61,
        SplineArea3D = 62,
        StackedSplineArea3D = 63,
        FullStackedSplineArea3D = 64,
        RangeArea3D = 65,
        BoxPlot = 66,
        Waterfall = 67,
        SwiftPoint = 68
    }

    //public enum WfPieSeriesViewType {
    //    Bar = 0,
    //    StackedBar = 1,
    //    FullStackedBar = 2,
    //    SideBySideStackedBar = 3,
    //    SideBySideFullStackedBar = 4,
    //    Pie = 5,
    //    Doughnut = 6,
    //    NestedDoughnut = 7,
    //    Funnel = 8,
    //    Point = 9,
    //    Bubble = 10,
    //    Line = 11,
    //    StackedLine = 12,
    //    FullStackedLine = 13,
    //    StepLine = 14,
    //    Spline = 15,
    //    ScatterLine = 16,
    //    SwiftPlot = 17,
    //    Area = 18,
    //    StepArea = 19,
    //    SplineArea = 20,
    //    StackedArea = 21,
    //    StackedStepArea = 22,
    //    StackedSplineArea = 23,
    //    FullStackedArea = 24,
    //    FullStackedSplineArea = 25,
    //    FullStackedStepArea = 26,
    //    RangeArea = 27,
    //    Stock = 28,
    //    CandleStick = 29,
    //    SideBySideRangeBar = 30,
    //    RangeBar = 31,
    //    SideBySideGantt = 32,
    //    Gantt = 33,
    //    PolarPoint = 34,
    //    PolarLine = 35,
    //    ScatterPolarLine = 36,
    //    PolarArea = 37,
    //    PolarRangeArea = 38,
    //    RadarPoint = 39,
    //    RadarLine = 40,
    //    ScatterRadarLine = 41,
    //    RadarArea = 42,
    //    RadarRangeArea = 43,
    //    Bar3D = 44,
    //    StackedBar3D = 45,
    //    FullStackedBar3D = 46,
    //    ManhattanBar = 47,
    //    SideBySideStackedBar3D = 48,
    //    SideBySideFullStackedBar3D = 49,
    //    Pie3D = 50,
    //    Doughnut3D = 51,
    //    Funnel3D = 52,
    //    Line3D = 53,
    //    StackedLine3D = 54,
    //    FullStackedLine3D = 55,
    //    StepLine3D = 56,
    //    Area3D = 57,
    //    StackedArea3D = 58,
    //    FullStackedArea3D = 59,
    //    StepArea3D = 60,
    //    Spline3D = 61,
    //    SplineArea3D = 62,
    //    StackedSplineArea3D = 63,
    //    FullStackedSplineArea3D = 64,
    //    RangeArea3D = 65,
    //    BoxPlot = 66,
    //    Waterfall = 67,
    //    SwiftPoint = 68
    //}
}
