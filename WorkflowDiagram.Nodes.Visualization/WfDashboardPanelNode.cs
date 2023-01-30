using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(false)]
    public class WfDashboardPanelNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Dashboard Panel";

        public override string Type => "Dashboard Panel";

        public override string Category => "Visualization";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "In", Text = "In", Type = WfConnectionPointType.In, Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Panel", Text = "Panel", Type = WfConnectionPointType.Out, Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected virtual object CreateVisualizationControl(object dataSource) { return null; }

        public object CreateVisualizationControl() {
            VisualizationControl = CreateVisualizationControl(Inputs["In"].Value);
            //WINFORMS
            //VisualizationControl.Dock = DockStyle.Fill;
            return VisualizationControl;
        }

        [XmlIgnore]
        [Browsable(false)]
        public object VisualizationControl { get; set; }
        protected override void OnVisitCore(WfRunner runner) {
            DataContext = this;
            Outputs[0].Visit(runner, this);
        }
        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }
        public string Caption { get; set; } = "Dashboard Panel";
        public int PanelOrderIndex { get; set; } = 0;
        public string PanelName { get; set; }
    }
}
