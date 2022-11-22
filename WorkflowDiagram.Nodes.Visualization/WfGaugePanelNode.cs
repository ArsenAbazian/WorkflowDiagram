using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization.Forms;
using WokflowDiagram.Nodes.Visualization.Managers;
using WorkflowDiagram;

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
        protected override Control CreateVisualizationControl(object seriesSource) {
            GaugeUserControl control = new GaugeUserControl();
            GaugeVisualizationManager.Default.InitializeGauge(this, control);
            return control;
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
        public GaugeLayoutMode LayoutMode { get; set; } = GaugeLayoutMode.Auto;
        public GaugeAlignMode AlignMode { get; set; } = GaugeAlignMode.Center;
    }

    public interface IGaugeNode {
        object GaugesSource { get; }
    }
}
