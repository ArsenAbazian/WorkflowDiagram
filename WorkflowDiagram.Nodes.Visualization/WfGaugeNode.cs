using DevExpress.XtraGauges.Base;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGauges.Win.Base;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraGauges.Win.Gauges.Digital;
using DevExpress.XtraGauges.Win.Gauges.Linear;
using DevExpress.XtraGauges.Win.Gauges.State;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(false)]
    public class WfGaugeNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Gauge";
        public override string Type => "Gauge";
        public override string Category => "Visualization";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Value", Text = "Value", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Gauge", Text = "Gauge", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }
        
        protected IProgress<object> Progress { get; set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            if(Gauge != null)
                Gauge.Dispose();
            Gauge = null;
            Gauges.Clear();

            Progress = new Progress<object>(dataSource => {
                UpdateValue();
            });

            return true;
        }

        [XmlIgnore]
        public virtual GaugeType GaugeType { get { return GaugeType.Circular; } }

        protected internal List<WfGaugeNode> Gauges { get; } = new List<WfGaugeNode>();
        protected internal BaseGaugeWin Gauge { get; set; }
        protected internal virtual BaseGaugeWin CreateGauge() {
            if(Gauge != null && !Gauge.IsDisposing)
                return Gauge;
            switch(GaugeType) {
                case GaugeType.Circular:
                    Gauge = CreateCircularGauge();
                    break;
                case GaugeType.Digital:
                    Gauge = CreateDigitalGauge();
                    break;
                case GaugeType.Linear:
                    Gauge = CreateLinearGauge();
                    break;
                case GaugeType.StateIndicator:
                    Gauge = new StateIndicatorGauge();
                    break;
            }
            Gauge.Name = Name;
            return Gauge;
        }

        protected virtual CircularGauge CreateCircularGauge() {
            return new CircularGauge();
        }

        protected virtual LinearGauge CreateLinearGauge() {
            return new LinearGauge();
        }

        protected virtual DigitalGauge CreateDigitalGauge() {
            return new DigitalGauge();
        }

        protected override void OnVisitCore(WfRunner runner) {
            if(Gauges.Count == 0)
                Gauges.Add(this);
            Value = Inputs["Value"].Value;
            var min = Inputs["Minimum"];
            var max = Inputs["Maximum"];
            if(min != null && max != null) {
                if(min.Value != null)
                    MinValue = (float)Convert.ToDouble(min.Value);
                if(max.Value != null)
                    MaxValue = (float)Convert.ToDouble(max.Value);
            }
            DataContext = this;
            Outputs["Gauge"].Visit(runner, this);
            Progress.Report(null);
        }

        public virtual float MinValue { get; set; } = 0.0f;
        public virtual float MaxValue { get; set; } = 1.0f;
        [XmlIgnore]
        [Browsable(false)]
        public object Value { get; set; }

        protected internal virtual void UpdateValue() {
            if(Gauge == null)
                return;
            
            if(Gauge is CircularGauge)
                UpdateCircularGauge((CircularGauge)Gauge);
            else if(Gauge is LinearGauge)
                UpdateLinearGauge((LinearGauge)Gauge);
            else if(Gauge is DigitalGauge)
                UpdateDigitalGauge((DigitalGauge)Gauge);
        }

        protected virtual void UpdateDigitalGauge(DigitalGauge gauge) {
            gauge.Text = string.Format("{0:" + DisplayFormat + "}", Value);
        }

        protected virtual void UpdateLinearGauge(LinearGauge gauge) {
            for(int i = 0; i < Gauges.Count; i++) {
                gauge.Scales[i].Value = (float)Convert.ToDouble(Gauges[i].Value);
                gauge.Scales[i].MinValue = (float)Convert.ToDouble(Gauges[i].MinValue);
                gauge.Scales[i].MaxValue = (float)Convert.ToDouble(Gauges[i].MaxValue);
            }
        }

        protected virtual void UpdateCircularGauge(CircularGauge gauge) {
            for(int i = 0; i < Gauges.Count; i++) {
                gauge.Scales[i].Value = (float)Convert.ToDouble(Gauges[i].Value);
                gauge.Scales[i].MinValue = (float)Convert.ToDouble(Gauges[i].MinValue);
                gauge.Scales[i].MaxValue = (float)Convert.ToDouble(Gauges[i].MaxValue);
            }
        }

        public string DisplayFormat { get; set; } = "g";
        protected internal virtual bool IsCombined { get { return false; } }
    }

    [WfToolboxVisible(true)]
    public class WfCircularGaugeNode : WfGaugeNode {
        public override string VisualTemplateName => "Circular Gauge";
        public override string Type => "Circular Gauge";
        public override GaugeType GaugeType => GaugeType.Circular;

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            List<WfConnectionPoint> res = base.GetDefaultInputs();
            res.AddRange( new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Minimum", Text = "Minimum", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Maximum", Text = "Maximum", Requirement = WfRequirementType.Optional }
            }.ToList());
            return res;
        }

        public int MajorTickCount { get; set; } = 0;
        public string MajorTickmarkFormatString { get; set; } = "";
        public int MajorTickmarkShapeOffset { get; set; } = 0;
        public int MajorTickmarkTextOffset { get; set; } = 0;
        public int MinorTickCount { get; set; } = 0;
        public float StartAngle { get; set; } = 0.0f;
        public float EndAngle { get; set; } = 0.0f;
    }

    [WfToolboxVisible(true)]
    public class WfLinearGaugeNode : WfGaugeNode {
        public override string VisualTemplateName => "Linear Gauge";
        public override string Type => "Linear Gauge";
        public override GaugeType GaugeType => GaugeType.Linear;

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            List<WfConnectionPoint> res = base.GetDefaultInputs();
            res.AddRange(new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Minimum", Text = "Minimum", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Maximum", Text = "Maximum", Requirement = WfRequirementType.Optional }
            }.ToList());
            return res;
        }
    }

    [WfToolboxVisible(true)]
    public class WfDigitalGaugeNode : WfGaugeNode {
        public override string VisualTemplateName => "Digital Gauge";
        public override string Type => "Digital Gauge";
        public override GaugeType GaugeType => GaugeType.Digital;

        public DigitalGaugeDisplayMode DisplayMode { get; set; } = DigitalGaugeDisplayMode.FourteenSegment;
        public int DigitCount { get; set; } = 0;
        public float LetterSpacing { get; set; } = 0.0f;
        public bool ShowBackground { get; set; } = true;
        public DigitalBackgroundShapeSetType BackgroundShape { get; set; } = DigitalBackgroundShapeSetType.Default;
        public bool ShowEffect { get; set; } = true;
        public DigitalEffectShapeType EffectShape { get; set; } = DigitalEffectShapeType.Default;
        public WfDigitalTextInput TextInput { get; set; } = WfDigitalTextInput.Value;

        [Browsable(false), XmlIgnore, EditorBrowsable(EditorBrowsableState.Never)]
        public override float MinValue { get; set; } = 0.0f;
        [Browsable(false), XmlIgnore, EditorBrowsable(EditorBrowsableState.Never)]
        public override float MaxValue { get; set; } = 1.0f;
    }

    public enum WfDigitalTextInput {
        Value,
        Text
    }
}
