using DevExpress.XtraEditors;
using DevExpress.XtraVerticalGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization.Forms;
using WokflowDiagram.Nodes.Visualization;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization.Commands {
    public class WfTableFormNodeCommandsProvider : IWfCommandsProvider {
        List<WfCommand> IWfCommandsProvider.Commands => new WfCommand[] { new WfTableFormNodeCustomizeCommand(), new WfTableFormNodeResetCustomizeCommand() }.ToList();
    }

    public class WfTableFormNodeCustomizeCommand : WfCommand {
        public override string Caption => "Customize Table";

        public override bool Execute(WfNode node) {
            WfTableFormNode tnode = node as WfTableFormNode;
            if(tnode == null)
                return false;
            using(TableFormCustomization form = new TableFormCustomization()) {
                form.XmlCustomizationText = tnode.XmlConfigurationText;
                form.DataSource = tnode.DataContext;
                if(form.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    tnode.XmlConfigurationText = form.XmlCustomizationText;
                }
            }
            return true;
        }
    }

    public class WfTableFormNodeResetCustomizeCommand : WfCommand {
        public override string Caption => "Reset Customization";

        public override bool Execute(WfNode node) {
            WfTableFormNode tnode = node as WfTableFormNode;
            if(tnode == null)
                return false;
            tnode.XmlConfigurationText = string.Empty;
            return true;
        }
    }
}
