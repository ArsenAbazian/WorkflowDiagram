using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraCharts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization.Managers {
    public class ChartVisualizationManager {
        static ChartVisualizationManager manager;
        public static ChartVisualizationManager Default {
            get {
                if(manager == null)
                    manager = new ChartVisualizationManager();
                return manager;
            }
            set { manager = value; }
        }

        public ChartVisualizationManager() {
            UserLookAndFeel.Default.StyleChanged += OnStyleChanged;
        }

        protected void UpdateChartColors(ChartControl c) {
            c.BackColor = CommonSkins.GetSkin(c.LookAndFeel.ActiveLookAndFeel).GetSystemColor(SystemColors.Control);
            XYDiagram d = c.Diagram as XYDiagram;
            if(d == null)
                return;
            UserLookAndFeel lf = c.LookAndFeel.ActiveLookAndFeel;
            UpdatePaneColors(d.DefaultPane, lf);
            foreach(XYDiagramPaneBase pane in d.Panes)
                UpdatePaneColors(pane, lf);
            UpdateAxisColors(d.AxisX, lf);
            UpdateAxisColors(d.AxisY, lf);
            foreach(Axis a in d.GetAllAxesX())
                UpdateAxisColors(a, lf);
            foreach(Axis a in d.GetAllAxesY())
                UpdateAxisColors(a, lf);
        }

        protected virtual void UpdateAxisColors(Axis a, UserLookAndFeel lf) {
            a.Color = CommonSkins.GetSkin(lf).GetSystemColor(SystemColors.GrayText);
            a.Label.TextColor = a.Color;
        }

        protected virtual void UpdatePaneColors(XYDiagramPaneBase pane, UserLookAndFeel lf) {
            pane.BackColor = CommonSkins.GetSkin(lf).GetSystemColor(SystemColors.Control);
            pane.BorderVisible = false;
        }

        private void OnStyleChanged(object sender, EventArgs e) {
            foreach(var r in Charts) {
                ChartControl c = null;
                if(!r.TryGetTarget(out c))
                    continue;
                if(c == null || c.IsDisposed)
                    continue;
                UpdateChartColors(c);
            }
        }

        protected void OnLookAndFeelChanged(object sender, LookAndFeelChangedEventArgs e) { 
            
        }

        protected List<WeakReference<ChartControl>> Charts { get; } = new List<WeakReference<ChartControl>>();
        public void InitializeChart(IChartNode node, ChartControl chartControl) {
            if(node == null)
                return;

            Charts.Add(new WeakReference<ChartControl>(chartControl));

            if(chartControl.Series.Count > 0)
                chartControl.Series.Clear();

            object seriesSource = node.SeriesSource;
            if(seriesSource is WfChartSeriesNode) {
                WfChartSeriesNode owner = (WfChartSeriesNode)seriesSource;
                Series s = owner.CreateSeries();
                chartControl.Series.Add(s);

                if(owner is WfFinancialSeriesNode) {
                    XYDiagram d = ((XYDiagram)chartControl.Diagram);
                    d.AxisX.DateTimeScaleOptions.MeasureUnit = ((WfFinancialSeriesNode)owner).ArgumentMeauseUnit;
                    d.AxisX.DateTimeScaleOptions.MeasureUnitMultiplier = ((WfFinancialSeriesNode)owner).MeasureUnitMultiplier;
                    d.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                    d.EnableAxisXZooming = d.EnableAxisYScrolling = true;
                    d.EnableAxisXScrolling = d.EnableAxisYScrolling = true;
                }
                s.Name = owner.SeriesName;
            }
            else {
                IEnumerable en = null;
                if(seriesSource is Dictionary<string, object>)
                    en = ((Dictionary<string, object>)seriesSource).Values;
                else
                    en = seriesSource as IEnumerable;
                if(en == null)
                    return;
                chartControl.BeginInit();
                XYDiagram d = chartControl.Diagram as XYDiagram;
                if(chartControl.Diagram == null) {
                    d = new XYDiagram();
                    d.EnableAxisXZooming = d.EnableAxisYZooming = true;
                    d.EnableAxisYScrolling = d.EnableAxisXScrolling = true;
                    d.Rotated = node.Rotated;
                    d.PaneLayout.Direction = node.PaneLayoutDirection;
                    d.PaneLayout.AutoLayoutMode = PaneAutoLayoutMode.Linear;

                    foreach(WfDiagramPane pane in node.Panes) {
                        var xy = new XYDiagramPane(pane.Name);
                        d.Panes.Add(xy);
                    }

                    chartControl.Diagram = d;
                }

                foreach(var item in en) {
                    WfChartSeriesNode owner = item as WfChartSeriesNode;
                    if(owner == null)
                        continue;
                    Series s = owner.CreateSeries();
                    if(!string.IsNullOrEmpty(owner.PaneName) && owner.PaneName != "Default") {
                        XYDiagramPaneBase pane = d.FindPaneByName(owner.PaneName);
                        if(pane == null) {
                            d.Panes.Add(new XYDiagramPane(owner.PaneName));
                        }
                        ((XYDiagramSeriesViewBase)s.View).Pane = d.FindPaneByName(owner.PaneName);
                    }
                    else {
                        ((XYDiagramSeriesViewBase)s.View).Pane = d.DefaultPane;
                    }

                    try {
                        chartControl.Series.Add(s);
                    }
                    catch(Exception e) {
                        node.Diagnostic.Add(new WfDiagnosticInfo() { Type = WfDiagnosticSeverity.Error, Text = "Exception while add series. " + e.ToString() });
                    }

                    if(owner is WfFinancialSeriesNode) {
                        d.AxisX.DateTimeScaleOptions.MeasureUnit = ((WfFinancialSeriesNode)owner).ArgumentMeauseUnit;
                        d.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                        var view = ((XYDiagramSeriesViewBase)s.View);

                        if(view.AxisY == null) {
                            var axis = new SecondaryAxisY(s.Name);
                            d.SecondaryAxesY.Add(axis);
                            view.AxisY = (SecondaryAxisY)d.FindAxisYByName(s.Name);
                            view.AxisY.WholeRange.AlwaysShowZeroLevel = false;
                        }
                    }
                }

            }

            UpdateChartColors(chartControl);
            chartControl.EndInit();
        }
    }
}
