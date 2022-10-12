using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraCharts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using WokflowDiagram.Nodes.Visualization;
using WokflowDiagram.Nodes.Visualization.Forms;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace WokflowDiagram.Nodes.Visualization {
    public class WfChartFormNode : WfVisualNodeBase, IChartNode {
        public override string VisualTemplateName => "ChartForm";

        public override string Type => "ChartForm";

        public override string Category => "Visualization";

        public WfChartFormNode() {

        }

        protected override void OnVisitCore(WfRunner runner) {
            DataContext = Inputs["In"].Value;
            Progress.Report(null);
            Outputs[0].Visit(runner, DataContext);
        }

        //private void ShowChart() {

        //    Application.OpenForms[0].BeginInvoke(new MethodInvoker(() => {
        //        IEnumerable en = DataContext as IEnumerable;
        //        if(en == null)
        //            return;

        //        var enumer = en.GetEnumerator();
        //        if(!enumer.MoveNext())
        //            return;

        //        ChartForm form = new ChartForm();
        //        ChartControl c = form.ChartControl;

        //        c.BeginInit();
        //        XYDiagram diagram = new XYDiagram();
        //        diagram.EnableAxisXZooming = diagram.EnableAxisYZooming = true;
        //        diagram.EnableAxisYScrolling = diagram.EnableAxisXScrolling = true;
        //        c.Diagram = diagram;

        //        object item = enumer.Current;
        //        if(item is Series)
        //            c.Series.Add((Series)item);
        //        //if(item is TradeInfoItem) {
        //        //    ((XYDiagram)c.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
        //        //    Series s = CreateLineSeries("Trades", nameof(TradeInfoItem.Time), nameof(TradeInfoItem.Rate));
        //        //    c.Series.Add(s);
        //        //}
        //        //else if(item is WfTimeSeriesItemInfo) {
        //        //    ((XYDiagram)c.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
        //        //    Series s = CreateLineSeries("Time Series", nameof(WfTimeSeriesItemInfo.Time), nameof(WfTimeSeriesItemInfo.Value));
        //        //    c.Series.Add(s);
        //        //}
        //        //else if(item is CandleStickData) {
        //        //    ((XYDiagram)c.Diagram).AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Second;
        //        //    Series s = CreateCandleStickSeries("Candlestick Series");
        //        //    c.Series.Add(s);
        //        //}
        //        c.EndInit();
        //        form.Show();
        //    }));
        //}

        //private Series CreateCandleStickSeries(string name) {
        //    Series s = new Series(name, ViewType.CandleStick);
        //    s.ArgumentDataMember = "Time";
        //    s.ArgumentScaleType = ScaleType.DateTime;
        //    s.ValueDataMembers.AddRange("Low", "High", "Open", "Close");
        //    s.ValueScaleType = ScaleType.Numerical;

        //    CandleStickSeriesView view = new CandleStickSeriesView();

        //    view.LineThickness = (int)(GraphWidth * DpiProvider.Default.DpiScaleFactor);
        //    view.LevelLineLength = 0.25;
        //    view.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;
        //    view.ReductionOptions.FillMode = CandleStickFillMode.AlwaysFilled;
        //    view.Color = DXSkinColors.ForeColors.Information;
        //    view.ReductionOptions.Color = DXSkinColors.ForeColors.Critical;

        //    view.ReductionOptions.Level = StockLevel.Open;
        //    view.ReductionOptions.Visible = true;
        //    view.AggregateFunction = SeriesAggregateFunction.Financial;
        //    view.LineThickness = (int)(1 * DpiProvider.Default.DpiScaleFactor);
        //    view.LevelLineLength = 0.25;

        //    s.View = view;
        //    s.CrosshairLabelPattern = "O={OV}\nH={HV}\nL={LV}\nC={CV}";

        //    object dataSource = DataContext;
        //    //IResizeableArray array = dataSource as IResizeableArray;
        //    s.DataSource = dataSource;
        //    //else {
        //    //    SeriesPoint[] points = new SeriesPoint[array.Count];
        //    //    PropertyInfo ap = array.GetItem(0).GetType().GetProperty(s.ArgumentDataMember, BindingFlags.Public | BindingFlags.Instance);
        //    //    PropertyInfo lp = array.GetItem(0).GetType().GetProperty("Low", BindingFlags.Public | BindingFlags.Instance);
        //    //    PropertyInfo hp = array.GetItem(0).GetType().GetProperty("High", BindingFlags.Public | BindingFlags.Instance);
        //    //    PropertyInfo op = array.GetItem(0).GetType().GetProperty("Open", BindingFlags.Public | BindingFlags.Instance);
        //    //    PropertyInfo cp = array.GetItem(0).GetType().GetProperty("Close", BindingFlags.Public | BindingFlags.Instance);

        //    //    Func<object, object> af = MakeAccessor(array.GetItem(0).GetType(), ap.PropertyType, s.ArgumentDataMember);
        //    //    Func<object, object> lf = MakeAccessor(array.GetItem(0).GetType(), lp.PropertyType, "Low");
        //    //    Func<object, object> hf = MakeAccessor(array.GetItem(0).GetType(), lp.PropertyType, "High");
        //    //    Func<object, object> of = MakeAccessor(array.GetItem(0).GetType(), lp.PropertyType, "Open");
        //    //    Func<object, object> cf = MakeAccessor(array.GetItem(0).GetType(), lp.PropertyType, "Close");

        //    //    if(ap.PropertyType == typeof(DateTime)) {
        //    //        for(int i = 0; i < array.Count; i++) {
        //    //            object item = array.GetItem(i);
        //    //            points[i] = new SeriesPoint((DateTime)af(item),
        //    //                new double[] {
        //    //                    (double)lf(item),
        //    //                    (double)hf(item),
        //    //                    (double)of(item),
        //    //                    (double)cf(item)
        //    //                });
        //    //        }
        //    //    }
        //    //    s.Points.AddRange(points);
        //    //}

        //    //s.DataSource = GetDataSource(info);
        //    return s;
        //}

        //private Series CreateLineSeries(string name, string argument, string value) {
        //    Series s = new Series();
        //    s.Name = name;
        //    s.ArgumentDataMember = argument;
        //    s.ArgumentScaleType = ScaleType.Auto;
        //    s.ValueDataMembers.AddRange(value);
        //    s.ValueScaleType = ScaleType.Numerical;
        //    LineSeriesView view = ChartType == WfChartType.StepLine ? new StepLineSeriesView() : new LineSeriesView();
        //    view.LineStyle.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
        //    view.Color = ChartColor;
        //    view.LineStyle.Thickness = (int)(GraphWidth * DpiProvider.Default.DpiScaleFactor);
        //    view.AggregateFunction = SeriesAggregateFunction.Average;
        //    s.View = view;
        //    s.DataSource = DataContext;
        //    return s;
        //}

        //public WfChartType ChartType { get; set; } = WfChartType.Line;
        //[Browsable(false)]
        //public WfColor ChartColorCore { get; set; }

        //[XmlIgnore]
        //public Color ChartColor { get { return ColorFromWfColor(ChartColorCore); } set { ChartColorCore = WfColorFromColor(value); } }
        //public int GraphWidth { get; set; } = 1;

        //internal static Color ColorFromWfColor(WfColor c) {
        //    return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        //}

        //internal static WfColor WfColorFromColor(Color c) {
        //    return new WfColor() { A = c.A, R = c.R, G = c.G, B = c.B };
        //}

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In", Text = "In", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Out", Text = "Out", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected IProgress<object> Progress { get; set; }
        ChartForm form;
        [XmlIgnore]
        public ChartForm Form {
            get {
                if(form == null || form.IsDisposed)
                    form = new ChartForm();
                return form;
            }
            set { form = value; }
        }
        protected override bool OnInitializeCore(WfRunner runner) {
            foreach(var pane in Panes) {
                if(string.IsNullOrEmpty(pane.Name)) {
                    DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "Chart pane's name should be specified.");
                    HasErrors = true;
                    return false;
                }
                if(Panes.Select(p => p.Name).Count() > 1) {
                    DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "Chart pane's name should be unique, but dublicate name detected");
                    HasErrors = true;
                    return false;
                }
            }
            Progress = new Progress<object>(dataSource => {
                Form.Node = this;
                Form.Show();
            });
            return true;
        }

        public bool Rotated { get; set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<WfDiagramPane> Panes { get; set; } = new List<WfDiagramPane>();

        public int PaneDistance { get; set; } = 10;
        public PaneLayoutDirection PaneLayoutDirection { get; set; } = PaneLayoutDirection.Vertical;
        object IChartNode.SeriesSource { get => Inputs["In"].Value; }
    }

    public class WfDiagramPane {
        public string Name { get; set; }
        public double SpaceAllocation { get; set; } = 1.0;
    }

    public interface IChartNode {
        bool Rotated { get; set; }
        List<WfDiagramPane> Panes { get; set; }
        int PaneDistance { get; set; }
        PaneLayoutDirection PaneLayoutDirection { get; set; }
        object SeriesSource { get; }
        List<WfDiagnosticInfo> Diagnostic { get; }
    }
}
