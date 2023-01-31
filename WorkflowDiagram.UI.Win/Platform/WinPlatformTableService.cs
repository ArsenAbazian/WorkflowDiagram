using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization;
using WokflowDiagram.Nodes.Visualization.Forms;
using WokflowDiagram.Nodes.Visualization.Managers;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WorkflowDiagram.UI.Win.Platform {
    public class WinPlatformTableService : IWfPlatformTableService {
        IWfTableForm IWfPlatformTableService.CreateTableForm(WfTableFormNode formNode) {
            return new TableForm();
        }

        object IWfPlatformTableService.CreateTableUserControl(ITableNode wfTablePanelNode) {
            return new TableUserControl();
        }

        void IWfPlatformTableService.InitializeTable(ITableNode tableNode, object tableUserControl) {
            ((TableUserControl)tableUserControl).Node = tableNode;
            TableVisualizationManager.Default.InitializeTable(tableNode, (TableUserControl)tableUserControl);
        }
    }
}
