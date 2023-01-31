using System.ComponentModel;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(true)]
    public class WfFinancialSeriesNode : WfChartSeriesNode {
        public override string Type => "Financial Series";
        public override string VisualTemplateName => "Financial Series";

        public WfFinancialSeriesNode() {
            ReductionColor = WfColor.FromArgb(255, 255, 0, 0);
            Color = WfColor.FromArgb(255, 0, 255, 0);
        }

        [Category("Series Options")]
        public WfFinancialSeriesViewType ViewType { get; set; } = WfFinancialSeriesViewType.CandleStick;
        protected override WfChartSeriesViewType GetViewType() {
            return (WfChartSeriesViewType)ViewType;
        }
        protected override bool OnInitializeCore(WfRunner runner) {
            if(!base.OnInitializeCore(runner))
                return false;
            if(string.IsNullOrEmpty(OpenDataMember)) {
                OnError("OpenDataMember must be specified");
                return false;
            }
            if(string.IsNullOrEmpty(CloseDataMember)) {
                OnError("CloseDataMember must be specified");
                return false;
            }
            if(string.IsNullOrEmpty(HighDataMember)) {
                OnError("HighDataMember must be specified");
                return false;
            }
            if(string.IsNullOrEmpty(LowDataMember)) {
                OnError("LowDataMember must be specified");
                return false;
            }
            return true;
        }
        
        
        [Category("Series Options")]
        public WfColor Color { get; set; } = WfColor.FromArgb(255, 0, 255, 0);

        [Category("Series Options")]
        public WfColor ReductionColor { get; set; } = WfColor.FromArgb(255, 0, 255, 0);

        [Category("Series Options")]
        public WfColor LineColor { get; set; } = WfColor.FromArgb(255, 0, 255, 0);

        [Category("Series Options")]
        public int LineThickness { get; set; } = 1;
        [Category("Series Options")]
        public double LineLevelLength { get; set; } = 0.25;

        [Category("Data Members"), Browsable(false)]
        public override string ValueDataMember { get; set; } = "Value";

        [Category("Data Members")]
        public override string ArgumentDataMember { get; set; } = "Time";

        [Category("Data Members")]
        public virtual string OpenDataMember { get; set; } = "Open";
        [Category("Data Members")]
        public virtual string CloseDataMember { get; set; } = "Close";
        [Category("Data Members")]
        public virtual string HighDataMember { get; set; } = "High";
        [Category("Data Members")]
        public virtual string LowDataMember { get; set; } = "Low";
    }

    public enum WfFinancialSeriesViewType {
        Stock = 28,
        CandleStick = 29,
        //SideBySideRangeBar = 30,
        //RangeBar = 31,
    }

    public enum WfDateTimeMeasureUnit {
        Millisecond,
        Second,
        Minute,
        Hour,
        Day,
        Week,
        Month,
        Quarter,
        Year
    }
}
