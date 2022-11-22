using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WokflowDiagram.Nodes.Visualization.Forms;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace WokflowDiagram.Nodes.Visualization {
    public class WfDashboardFormNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Dashboard Form";

        public override string Type => "Dashboard Form";

        public override string Category => "Visualization";

        protected override WfConnectionPointCollection CreateInputCollection() {
            var res = base.CreateInputCollection();
            res.AllowedOperations = WfEditOperation.Add | WfEditOperation.Edit | WfEditOperation.Remove;
            return res;
        }

        public override WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type) {
            if(type == WfConnectionPointType.In) {
                var res = base.CreateConnectionPoint(type);
                res.Name = "Panel" + Inputs.Count;
                res.Text = "Panel " + Inputs.Count;
                res.AllowedOperations = WfEditOperation.Edit | WfEditOperation.Remove;
                res.Requirement = WfRequirementType.Optional;
                return res;
            }
            return base.CreateConnectionPoint(type);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Panel1", Text = "Panel 1", Type = WfConnectionPointType.In, AllowedOperations = WfEditOperation.Edit | WfEditOperation.Remove, Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {

            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            Progress = new Progress<object>(dataSource => {
                Form.Node = this;
                Form.Show();
            });
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            List<object> panels = new List<object>();
            foreach(var point in Inputs) {
                if(point.Name == RunConnectionPointName)
                    continue;
                panels.Add(point.Value);
            }
            DataContext = panels;
            Progress.Report(null);
        }

        protected IProgress<object> Progress { get; set; }
        DashboardForm form;
        [XmlIgnore]
        public DashboardForm Form {
            get {
                if(form == null || form.IsDisposed)
                    form = CreateForm();
                return form;
            }
            set { form = value; }
        }

        [Browsable(false)]
        public List<WfWorkspaceInfo> Workspaces { get; } = new List<WfWorkspaceInfo>();
        [Browsable(false)]
        public string SelectedWorkspaceName { get; set; }

        protected virtual DashboardForm CreateForm() { return new DashboardForm(this); }
    }

    public class WfWorkspaceInfo { 
        public string Name { get; set; }
        public string XmlSerializationData { get; set; }
    }
}
