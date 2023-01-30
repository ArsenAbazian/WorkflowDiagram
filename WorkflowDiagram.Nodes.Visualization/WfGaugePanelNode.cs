using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(true)]
    public class WfGaugePanelNode : WfDashboardPanelNode, IGaugeNode {
        public override string VisualTemplateName => "Gauges Panel";

        public override string Type => "Gauges Panel";

        public bool Rotated { get; set; } = false;
        
        object IGaugeNode.GaugesSource { 
            get {
                List<object> gauges = new List<object>();
                for(int i = 1; i < Inputs.Count; i++) {
                    gauges.Add(Inputs[i].Value);
                }
                return gauges;
            } 
        }

        protected internal IWfPlatformGaugeService GaugeService { get; set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            GaugeService = Document.PlatformServices.GetService<IWfPlatformGaugeService>(this);
            return base.OnInitializeCore(runner);
        }

        protected override object CreateVisualizationControl(object seriesSource) {
            return GaugeService.CreateGaugeUserControl(this);
        }

        public override string ToString() {
            return "Gauges Panel";
        }

        protected override WfConnectionPointCollection CreateInputCollection() {
            var res = base.CreateInputCollection();
            res.AllowedOperations |= WfEditOperation.Add | WfEditOperation.Edit | WfEditOperation.Remove;
            return res;
        }

        public override WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type) {
            var res = base.CreateConnectionPoint(type);
            if(type == WfConnectionPointType.In) {
                res.Requirement = WfRequirementType.Optional;
                res.Name = "In" + Inputs.Count;
                res.Text = res.Name;
                res.AllowedOperations = WfEditOperation.Edit | WfEditOperation.Remove;
            }
            return res;
        }

        public int GaugesPerLine { get; set; } = -1;
        public WfGaugeLayoutMode LayoutMode { get; set; } = WfGaugeLayoutMode.Auto;
        public WfGaugeAlignMode AlignMode { get; set; } = WfGaugeAlignMode.Center;
    }

    public interface IGaugeNode {
        object GaugesSource { get; }
    }

    public enum WfGaugeLayoutMode {
        Default,
        Auto,
        Horizontal,
        Vertical
    }
    public enum WfGaugeAlignMode {
        Default,
        Near,
        Center,
        Far
    }
}
