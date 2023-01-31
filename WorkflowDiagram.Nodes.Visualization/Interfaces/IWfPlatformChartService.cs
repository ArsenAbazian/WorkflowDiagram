using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;

namespace WorkflowDiagram.Nodes.Visualization.Interfaces {
    public interface IWfPlatformChartService {
        IWfChartForm CreateChartForm(IChartNode formNode);
        object CreateChartUserControl(WfChartPanelNode wfChartPanelNode);
        object CreateSeries(WfChartSeriesNode node, string name, WfChartSeriesViewType viewType);
        void InitializeChart(WfNode chartNode, object chartUserControl);
    }
}
