using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram {
    [Browsable(false)]
    public class WfCustomNode : WfNode {
        public override string VisualTemplateName => "Custom";
        public override string Type => "Custom";

        public WfCustomNode() { }

        public WfCustomNode(Func<WfRunner, WfNode, bool> onVisit) {
            VisitAction = onVisit;
        }

        public WfCustomNode(Func<WfRunner, WfNode, bool> onInitialize, Func<WfRunner, WfNode, bool> onVisit) {
            InitializeAction= onInitialize;
            VisitAction = onVisit;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "In", Text = "In", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Out", Text = "Out", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        [XmlIgnore]
        [Browsable(false)]
        public Func<WfRunner, WfNode, bool> InitializeAction { get; set; }
        [XmlIgnore]
        [Browsable(false)]
        public Func<WfRunner, WfNode, bool> VisitAction { get; set; }

        protected override bool OnInitializeCore(WfRunner runner) {
            if(InitializeAction != null)
                return InitializeAction(runner, this);
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            bool res = VisitAction != null ? VisitAction(runner, this) : true;
            if(res)
                Outputs["Out"].Visit(runner, Inputs["In"].Value);
            else
                Outputs["Out"].SkipVisit(runner, Inputs["In"].Value);
        }
    }
}
