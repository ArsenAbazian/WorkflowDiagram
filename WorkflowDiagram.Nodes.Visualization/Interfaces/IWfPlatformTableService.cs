using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;

namespace WorkflowDiagram.Nodes.Visualization.Interfaces {
    public interface IWfPlatformTableService {
        IWfTableForm CreateTableForm(WfTableFormNode formNode);
        object CreateTableUserControl(WfTablePanelNode wfTablePanelNode);
        void InitializeTable(WfNode tableNode, object tableUserControl);
        void InitializeSeries(object s, WfNode seriesNode);
        void ShowTableForm(IWfTableForm form, WfTableFormNode wfTableFormNode);
    }
}
