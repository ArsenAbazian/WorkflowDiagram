using DevExpress.XtraGauges.Win.Base;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraGauges.Win.Gauges.Digital;
using DevExpress.XtraGauges.Win.Gauges.Linear;
using DevExpress.XtraGauges.Win.Gauges.State;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization.Forms;

namespace WokflowDiagram.Nodes.Visualization.Managers {
    public class GaugeVisualizationManager {
        static GaugeVisualizationManager defaultManager;
        public static GaugeVisualizationManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = new GaugeVisualizationManager();
                return defaultManager;
            }
            set { defaultManager = value; }
        }

        public void InitializeGauge(IGaugeNode node, GaugeUserControl control) {
            IEnumerable en = node.GaugesSource as IEnumerable;
            WfGaugeNode gn = node.GaugesSource as WfGaugeNode;
            control.GaugeControl.Gauges.BeginUpdate();
            try {
                control.GaugeControl.Gauges.Clear();
                if(gn != null) {
                    AddGauge(gn, control);
                    return;
                }
                foreach(var item in en) {
                    gn = item as WfGaugeNode;
                    if(gn == null)
                        continue;
                    AddGauge(gn, control);
                }
            }
            finally {
                control.GaugeControl.Gauges.EndUpdate();
            }
        }

        private void AddGauge(WfGaugeNode gn, GaugeUserControl control) {
            BaseGaugeWin gauge = (BaseGaugeWin)gn.CreatePlatformImplGauge();
            gn.GaugePlatformImpl = gauge;
            StateIndicatorGauge sg = gauge as StateIndicatorGauge;
            if(gauge is CircularGauge)
                InitializeCircularGauge((CircularGauge)gauge, gn);
            else if(gauge is LinearGauge)
                InitializeLinearGauge((LinearGauge)gauge, gn);
            if(gauge is DigitalGauge)
                InitializeDigitalGauge((DigitalGauge)gauge, gn);
            if(gauge is StateIndicatorGauge)
                InitializeStateIndicatorGauge((StateIndicatorGauge)gauge, gn);
                

            gn.UpdateValue();
            control.GaugeControl.Gauges.Add(gauge);
        }

        protected virtual void InitializeStateIndicatorGauge(StateIndicatorGauge gauge, WfGaugeNode gn) {
            ImageIndicatorComponent c = gauge.AddImageIndicator();
        }

        protected virtual void InitializeDigitalGauge(DigitalGauge gauge, WfGaugeNode gn) {
            WfDigitalGaugeNode dn = (WfDigitalGaugeNode)gn;
            //TODO
            //gauge.DisplayMode = dn.DisplayMode;
            if(dn.DigitCount > 0)
                gauge.DigitCount = dn.DigitCount;
            if(dn.LetterSpacing > 0.0f)
                gauge.LetterSpacing = dn.LetterSpacing;
            if(dn.ShowBackground) {
                var comp = gauge.AddBackgroundLayer();
                //TODO
                //comp.ShapeType = dn.BackgroundShape;
            }
            if(dn.ShowEffect) {
                var eff = gauge.AddEffectLayer();
                //TODO
                //eff.ShapeType = dn.EffectShape;
            }
        }

        protected virtual void InitializeLinearGauge(LinearGauge gauge, WfGaugeNode gn) {
            gauge.AddDefaultElements();
            InitializeScales(gauge, gn);
            gauge.Labels.Add(CreateCaptionLabel(gn));
        }

        protected virtual void InitializeScales(CircularGauge gauge, WfGaugeNode gn) {
            if(gn.IsCombined) {
                gauge.Scales.Clear();
                foreach(WfGaugeNode sn in gn.Gauges) {
                    ArcScaleComponent comp = gauge.AddScale();
                    comp.Name = sn.Name;
                    if(sn.MinValue != 0 && sn.MaxValue != 0) {
                        comp.MinValue = sn.MinValue;
                        comp.MaxValue = sn.MaxValue;
                    }

                    WfCircularGaugeNode cn = sn as WfCircularGaugeNode;
                    if(cn != null) {
                        if(cn.MajorTickCount > 0)
                            comp.MajorTickCount = cn.MajorTickCount;
                        if(cn.MajorTickmarkShapeOffset != 0)
                            comp.MajorTickmark.ShapeOffset = cn.MajorTickmarkShapeOffset;
                        if(cn.MajorTickmarkTextOffset != 0)
                            comp.MajorTickmark.TextOffset = cn.MajorTickmarkTextOffset;
                        if(!string.IsNullOrEmpty(cn.MajorTickmarkFormatString))
                            comp.MajorTickmark.FormatString = cn.MajorTickmarkFormatString;
                        if(cn.StartAngle != 0 && cn.EndAngle != 0) {
                            comp.StartAngle = cn.StartAngle;
                            comp.EndAngle = cn.EndAngle;
                        }
                    }
                }
            }
            else {
                gauge.Scales[0].MinValue = gn.MinValue;
                gauge.Scales[0].MaxValue = gn.MaxValue;
            }
        }

        protected virtual void InitializeScales(LinearGauge gauge, WfGaugeNode gn) {
            if(gn.IsCombined) {
                gauge.Scales.Clear();
                foreach(WfGaugeNode sn in gn.Gauges) {
                    LinearScaleComponent comp = gauge.AddScale();
                    comp.Name = sn.Name;
                    comp.MinValue = sn.MinValue;
                    comp.MaxValue = sn.MaxValue;
                }
            }
            else {
                gauge.Scales[0].MinValue = gn.MinValue;
                gauge.Scales[0].MaxValue = gn.MaxValue;
            }
        }

        protected virtual void InitializeCircularGauge(CircularGauge gauge, WfGaugeNode gn) {
            gauge.AddDefaultElements();
            InitializeScales(gauge, gn);
            gauge.Labels.Add(CreateCaptionLabel(gn));
        }

        protected virtual LabelComponent CreateCaptionLabel(WfGaugeNode gn) {
            LabelComponent comp = new LabelComponent(gn.Name);
            comp.AllowHTMLString = true;
            comp.ZOrder = -10000;
            comp.AppearanceText.Font = new System.Drawing.Font(comp.AppearanceText.Font.FontFamily, 14.0f);
            comp.Position = new DevExpress.XtraGauges.Core.Base.PointF2D(125, 270);
            comp.Text = gn.Text;
            return comp;
        }
    }
}
