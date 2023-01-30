using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WokflowDiagram.Nodes.Visualization {
    [WfCommandsProvider("WokflowDiagram.Nodes.Visualisation.Commands.WfTableFormNodeCommandsProvider")]
    public class WfTableFormNode : WfVisualNodeBase, ITableNode {
        public override string VisualTemplateName => "TableForm";

        public override string Type => "TableForm";
        public override string Category => "Visualization";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "DataSource", Text = "DataSource", Requirement = WfRequirementType.Mandatory },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new List<WfConnectionPoint>();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            Progress = new Progress<object>(dataSource => {
                Form.Node = this;
                Form.DataSource = dataSource;
                Form.Show();
            });
            return true;
        }

        protected IWfPlatformServices Services { get; set; }

        IWfTableForm form;
        [XmlIgnore]
        public IWfTableForm Form {
            get {
                if(form == null || Document.PlatformServices.ShouldRecreateForm(form))
                    form = Document.PlatformServices.CreateForm<IWfTableForm>();
                return form;
            }
            set {
                form = value;
            }
        }

        [Browsable(false)]
        public string XmlConfigurationText { get; set; }
        [Browsable(false)]
        [XmlIgnore]
        public object DataSource { get; set; }

        protected IProgress<object> Progress { get; set; }
        protected override void OnVisitCore(WfRunner runner) {
            object dataSource = Inputs["DataSource"].Value;
            DataSource = dataSource;
            Progress.Report(dataSource);
        }
    }

    public interface ITableNode {
        string XmlConfigurationText { get; set; }
        object DataSource { get; set; }
    }
}
