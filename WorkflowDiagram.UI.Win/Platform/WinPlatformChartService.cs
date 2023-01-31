using DevExpress.Skins;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization;
using WokflowDiagram.Nodes.Visualization.Forms;
using WokflowDiagram.Nodes.Visualization.Managers;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WorkflowDiagram.UI.Win.Platform {
    public class WinPlatformChartService : IWfPlatformChartService {
        IWfChartForm IWfPlatformChartService.CreateChartForm(IChartNode formNode) {
            return new ChartForm();
        }

        object IWfPlatformChartService.CreateChartUserControl(WfChartPanelNode wfChartPanelNode) {
            ChartUserControl control = new ChartUserControl();
            control.Dock = DockStyle.Fill;
            return control;
        }

        object IWfPlatformChartService.CreateSeries(WfChartSeriesNode node, string name, WfChartSeriesViewType viewType) {
            Series s = new Series(name, (ViewType)viewType);

            InitializeBaseSeries(node, s);
            if(node is WfAreaSeriesNode anode)
                InitializeAreaSeries(anode, s);
            else if(node is WfBarSeriesNode bnode)
                InitializeBarSeries(bnode, s);
            else if(node is WfLineSeriesNode lnode)
                InitializeLineSeries(lnode, s);
            else if(node is WfFinancialSeriesNode fnode)
                InitializeFinancialSeries(fnode, s);

            return s;
        }

        protected virtual void InitializeFinancialSeries(WfFinancialSeriesNode fnode, Series s) {
            //WINFORM
            //ReductionColor = DXSkinColors.ForeColors.Critical;
            //Color = DXSkinColors.FillColors.Success;

            s.ArgumentScaleType = ScaleType.DateTime;
            s.CrosshairLabelPattern = "O={OV}\nH={HV}\nL={LV}\nC={CV}";
            s.ValueScaleType = ScaleType.Numerical;

            FinancialSeriesViewBase view = (FinancialSeriesViewBase)s.View;
            view.Color = fnode.Color.ToColor();
            view.AggregateFunction = SeriesAggregateFunction.None; // SeriesAggregateFunction.Financial;
            view.LineThickness = (int)(fnode.LineThickness * DpiProvider.Default.DpiScaleFactor);
            view.LevelLineLength = fnode.LineLevelLength;

            view.ReductionOptions.Color = fnode.ReductionColor.ToColor();
            view.ReductionOptions.Level = StockLevel.Open;
            view.ReductionOptions.ColorMode = ReductionColorMode.OpenToCloseValue;

            if(view is CandleStickSeriesView)
                ((CandleStickSeriesView)view).ReductionOptions.FillMode = CandleStickFillMode.AlwaysFilled;
            s.ValueDataMembers.Clear();
            s.ValueDataMembers.AddRange(fnode.LowDataMember, fnode.HighDataMember, fnode.OpenDataMember, fnode.CloseDataMember);
        }

        protected virtual void InitializeLineSeries(WfLineSeriesNode lnode, Series s) {
            s.ArgumentScaleType = ScaleType.Auto;
            LineSeriesView view = (LineSeriesView)s.View;
            view.Color = lnode.LineColor.ToColor();
            view.AggregateFunction = SeriesAggregateFunction.Average;
            view.LineStyle.Thickness = lnode.LineThickness;
            view.LineStyle.DashStyle = (DashStyle)lnode.LineStyle;
            view.LineMarkerOptions.BorderColor = lnode.MarkerOptions.BorderColor.ToColor();
            view.LineMarkerOptions.BorderVisible = lnode.MarkerOptions.ShowBorder;
            view.LineMarkerOptions.Color = lnode.MarkerOptions.Color.ToColor();
            view.LineMarkerOptions.FillStyle.FillMode = FillMode.Solid;
            view.LineMarkerOptions.Size = lnode.MarkerOptions.Size;
            view.LineMarkerOptions.Kind = (MarkerKind)lnode.MarkerOptions.Kind;
        }

        protected virtual void InitializeBarSeries(WfBarSeriesNode bnode, Series s) {
            s.ArgumentScaleType = ScaleType.Auto;
            BarSeriesView view = (BarSeriesView)s.View;
            view.Color = bnode.Color.ToColor();
            view.AggregateFunction = SeriesAggregateFunction.Average;
            view.Border.Thickness = bnode.LineThickness;
            view.Border.Color = bnode.LineColor.ToColor();
            view.BarWidth = bnode.BarWidth;
            view.ColorEach = bnode.ColorEach;
            view.FillStyle.FillMode = (FillMode)bnode.FillMode;
            view.Transparency = (byte)bnode.Transparency;
        }

        protected virtual void InitializeBaseSeries(WfChartSeriesNode node, Series s) {
            s.Name = node.SeriesName;
            s.Tag = this;
            s.ArgumentDataMember = node.ArgumentDataMember;
            s.ValueDataMembers.AddRange(node.ValueDataMember);
            s.DataSource = node.DataSource;
        }

        protected virtual void InitializeAreaSeries(WfAreaSeriesNode anode, Series s) {
            s.ArgumentScaleType = ScaleType.Auto;
            AreaSeriesView view = (AreaSeriesView)s.View;
            view.Color = anode.Color.ToColor();
            view.AggregateFunction = SeriesAggregateFunction.Average;
            view.Border.Thickness = anode.LineThickness;
            view.Border.Color = anode.LineColor.ToColor();
            view.MarkerOptions.BorderColor = anode.MarkerOptions.BorderColor.ToColor();
            view.MarkerOptions.BorderVisible = anode.MarkerOptions.ShowBorder;
            view.MarkerOptions.Color = anode.MarkerOptions.Color.ToColor();
            view.MarkerOptions.FillStyle.FillMode = (DevExpress.XtraCharts.FillMode)WfFillMode.Solid;
            view.MarkerOptions.Size = anode.MarkerOptions.Size;
            view.MarkerOptions.Kind = (MarkerKind)anode.MarkerOptions.Kind;
            view.ColorEach = anode.ColorEach;

            view.FillStyle.FillMode = (FillMode)anode.FillMode;
            view.Transparency = (byte)anode.Transparency;
        }

        void IWfPlatformChartService.InitializeChart(WfNode chartNode, object chartUserControl) {
            ChartVisualizationManager.Default.InitializeChart((IChartNode)chartNode, ((ChartUserControl)chartUserControl).ChartControl);
        }
    }
}
