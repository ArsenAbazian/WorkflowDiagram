using DevExpress.LookAndFeel;
using DevExpress.Skins;
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
    public class WfFinancialSeriesNode : WfChartSeriesNode {
        public override string Type => "Financial Series";
        public override string VisualTemplateName => "Financial Series";

        public WfFinancialSeriesNode() {
            ReductionColor = DXSkinColors.ForeColors.Critical;
            Color = DXSkinColors.FillColors.Success;
        }

        [Category("Series Options")]
        public WfFinancialSeriesViewType ViewType { get; set; } = WfFinancialSeriesViewType.CandleStick;
        protected override ViewType GetViewType() {
            return (DevExpress.XtraCharts.ViewType)ViewType;
        }
        protected override bool OnInitializeCore(WfRunner runner) {
            if(!base.OnInitializeCore(runner))
                return false;
            if(string.IsNullOrEmpty(OpenDataMember)) {
                DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "OpenDataMember must be specified");
                HasErrors = true;
                return false;
            }
            if(string.IsNullOrEmpty(CloseDataMember)) {
                DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "CloseDataMember must be specified");
                HasErrors = true;
                return false;
            }
            if(string.IsNullOrEmpty(HighDataMember)) {
                DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "HighDataMember must be specified");
                HasErrors = true;
                return false;
            }
            if(string.IsNullOrEmpty(LowDataMember)) {
                DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "LowDataMember must be specified");
                HasErrors = true;
                return false;
            }
            return true;
        }

        protected override void InitializeValueDataMembers(Series s) {
            s.ValueDataMembers.AddRange(LowDataMember, HighDataMember, OpenDataMember, CloseDataMember);
        }

        protected override Series CreateSeries() {
            Series s = base.CreateSeries();
            s.ArgumentScaleType = ScaleType.DateTime;
            s.CrosshairLabelPattern = "O={OV}\nH={HV}\nL={LV}\nC={CV}";
            s.ValueScaleType = ScaleType.Numerical;

            FinancialSeriesViewBase view = (FinancialSeriesViewBase)s.View;
            view.Color = Color;
            view.AggregateFunction = SeriesAggregateFunction.None; // SeriesAggregateFunction.Financial;
            view.LineThickness = (int)(LineThickness * DpiProvider.Default.DpiScaleFactor);
            view.LevelLineLength = LineLevelLength;

            view.ReductionOptions.Color = ReductionColor;
            view.ReductionOptions.Level = StockLevel.Open;
            view.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;

            if(view is CandleStickSeriesView)
                ((CandleStickSeriesView)view).ReductionOptions.FillMode = CandleStickFillMode.AlwaysFilled;

            return s;
        }

        [Category("Series Options")]
        [Browsable(false)]
        public WfColor ColorCore { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        [Category("Series Options")]
        [XmlIgnore]
        public Color Color { get { return ColorCore.ToColor(); } set { ColorCore = value.ToWfColor(); } }

        [Category("Series Options")]
        [Browsable(false)]
        public WfColor ReductionColorCore { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        [Category("Series Options")]
        [XmlIgnore]
        public Color ReductionColor { get { return ReductionColorCore.ToColor(); } set { ReductionColorCore = value.ToWfColor(); } }

        [Category("Series Options")]
        [Browsable(false)]
        public WfColor LineColorCore { get; set; } = WfColor.FromArgb(255, 0, 255, 0);
        [Category("Series Options")]
        [XmlIgnore]
        public Color LineColor { get { return LineColorCore.ToColor(); } set { LineColorCore = value.ToWfColor(); } }

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

        [Category("Data Members")]
        public DateTimeMeasureUnit ArgumentMeauseUnit { get; set; } = DateTimeMeasureUnit.Minute;
        [Category("Data Members")]
        public int MeasureUnitMultiplier { get; set; } = 1;
    }

    public enum WfFinancialSeriesViewType {
        Stock = 28,
        CandleStick = 29,
        //SideBySideRangeBar = 30,
        //RangeBar = 31,
    }
}
