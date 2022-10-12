using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WokflowDiagram.Nodes.Visualization.Forms;
using WokflowDiagram.Nodes.Visualization.Managers;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization {
    [WfToolboxVisible(true)]
    public class WfTablePanelNode : WfDashboardPanelNode, ITableNode {
        public override string VisualTemplateName => "Table Panel";

        public override string Type => "Table Panel";

        protected override Control CreateVisualizationControl(object seriesSource) {
            TableUserControl control = new TableUserControl();
            TableVisualizationManager.Default.InitializeTable(this, control);
            return control;
        }

        protected override void OnVisitCore(WfRunner runner) {
            DataSource = Inputs["In"].Value;
            base.OnVisitCore(runner);
        }

        [Browsable(false)]
        public string XmlConfigurationText { get; set; }
        [Browsable(false)]
        [XmlIgnore]
        public object DataSource { get; set; }

        public override string ToString() {
            return "Table Panel";
        }
    }
}
