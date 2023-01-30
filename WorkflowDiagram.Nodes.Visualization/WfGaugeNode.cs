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
            GaugeService = Document.PlatformServices.GetService<IWfPlatformGaugeService>(this);
            IDisposable ds = Gauge as IDisposable;
            if(ds != null)
                ds.Dispose();
            Gauge = null;
            Gauges.Clear();

            Progress = new Progress<object>(dataSource => {
                UpdateValue();
            });

            return true;
        }

        [XmlIgnore]
        public virtual WfGaugeType GaugeType { get { return WfGaugeType.Circular; } }

        protected internal IWfPlatformGaugeService GaugeService { get; set; }
        protected internal List<WfGaugeNode> Gauges { get; } = new List<WfGaugeNode>();
        protected internal object Gauge { get; set; }
        protected internal virtual object CreatePlatformImplGauge() {
            if(Gauge != null && !GaugeService.ShouldRecreateGauge(Gauge))
                return Gauge;
            switch(GaugeType) {
                case WfGaugeType.Circular:
                    Gauge = CreateCircularGauge();
                    break;
                case WfGaugeType.Digital:
                    Gauge = CreateDigitalGauge();
                    break;
                case WfGaugeType.Linear:
                    Gauge = CreateLinearGauge();
                    break;
                    //case WfGaugeType.StateIndicator:
                    //    Gauge = CreateStateIndicatorGauge();
                    //    break;
            }
            return Gauge;
        }

        protected virtual object CreateCircularGauge() {
            return GaugeService.CreateCircularGauge(this);
        }

        protected virtual object CreateLinearGauge() {
            return GaugeService.CreateLinearGauge(this);
        }

        protected virtual object CreateDigitalGauge() {
            return GaugeService.CreateDigitalGauge(this);
        }

        //protected virtual object CreateStateIndicatorGauge() {
        //    return GaugeService.CreateStateIndicatorGauge(this);
        //    //WINFORM
        //    //return new DigitalGauge();
        //}

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

            if(GaugeType == WfGaugeType.Circular)
                UpdateCircularGauge(Gauge);
            else if(GaugeType == WfGaugeType.Linear)
                UpdateLinearGauge(Gauge);
            else if(GaugeType == WfGaugeType.Digital)
                UpdateDigitalGauge(Gauge);
        }

        protected virtual void UpdateDigitalGauge(object gauge) {
            GaugeService.UpdateDigitalGauge(this, gauge);
        }

        protected virtual void UpdateLinearGauge(object gauge) {
            GaugeService.UpdateLinearGauge(this, gauge);
        }

        protected virtual void UpdateCircularGauge(object gauge) {
            GaugeService.UpdateCircularGauge(this, gauge);
        }

        public string DisplayFormat { get; set; } = "g";
        protected internal virtual bool IsCombined { get { return false; } }
    }

    [WfToolboxVisible(true)]
    public class WfCircularGaugeNode : WfGaugeNode {
        public override string VisualTemplateName => "Circular Gauge";
        public override string Type => "Circular Gauge";
        public override WfGaugeType GaugeType => WfGaugeType.Circular;

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            List<WfConnectionPoint> res = base.GetDefaultInputs();
            res.AddRange(new WfConnectionPoint[] {
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
        public override WfGaugeType GaugeType => WfGaugeType.Linear;

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
        public override WfGaugeType GaugeType => WfGaugeType.Digital;

        //public DigitalGaugeDisplayMode DisplayMode { get; set; } = DigitalGaugeDisplayMode.FourteenSegment;
        public int DigitCount { get; set; } = 0;
        public float LetterSpacing { get; set; } = 0.0f;
        public bool ShowBackground { get; set; } = true;
        //public DigitalBackgroundShapeSetType BackgroundShape { get; set; } = DigitalBackgroundShapeSetType.Default;
        public bool ShowEffect { get; set; } = true;
        //public DigitalEffectShapeType EffectShape { get; set; } = DigitalEffectShapeType.Default;
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

    public enum WfGaugeType {
        Circular,
        Linear,
        Digital,
        //StateIndicator
    }
}
