using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WorkflowDiagram.UI.Win.Platform {
    public class WinPlatformTableService : IWfPlatformTableService {
        IWfTableForm IWfPlatformTableService.CreateTableForm(WfTableFormNode formNode) {
            throw new NotImplementedException();
        }

        object IWfPlatformTableService.CreateTableUserControl(WfTablePanelNode wfTablePanelNode) {
            throw new NotImplementedException();
        }

        void IWfPlatformTableService.InitializeSeries(object s, WfNode seriesNode) {
            throw new NotImplementedException();
        }

        void IWfPlatformTableService.InitializeTable(WfNode tableNode, object tableUserControl) {
            throw new NotImplementedException();
        }

        void IWfPlatformTableService.ShowTableForm(IWfTableForm form, WfTableFormNode wfTableFormNode) {
            throw new NotImplementedException();
        }
    }
}
