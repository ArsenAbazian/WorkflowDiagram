using System.Collections.Generic;
using System.Linq;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WokflowDiagram.Nodes.Visualization.Commands {
    public class WfTableFormNodeCommandsProvider : IWfCommandsProvider {
        List<WfCommand> IWfCommandsProvider.Commands => new WfCommand[] { new WfTableFormNodeCustomizeCommand(), new WfTableFormNodeResetCustomizeCommand() }.ToList();
    }

    public class WfTableFormNodeCustomizeCommand : WfCommand {
        public override string Caption => "Customize Table";

        public override bool Execute(WfNode node) {
            WfTableFormNode tnode = node as WfTableFormNode;
            if(tnode == null || tnode.Document.PlatformServices == null)
                return false;
            using(IWfTableCustomizationForm form = tnode.Document.PlatformServices.CreateForm<IWfTableCustomizationForm>()) {
                form.XmlCustomizationText = tnode.XmlConfigurationText;
                form.DataSource = tnode.DataContext;
                if(form.ShowFormDialog()) {
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
