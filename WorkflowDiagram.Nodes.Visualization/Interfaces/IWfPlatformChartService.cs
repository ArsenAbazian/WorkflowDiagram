using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;

namespace WorkflowDiagram.Nodes.Visualization.Interfaces {
    public interface IWfPlatformChartService {
        IWfChartForm CreateChartForm(WfChartFormNode formNode);
        object CreateChartUserControl(WfChartPanelNode wfChartPanelNode);
        object CreateSeries(string name, WfChartSeriesViewType viewType);
        void InitializeChart(WfNode chartNode, object chartUserControl);
        void InitializeSeries(object s, WfNode seriesNode);
        void ShowChartForm(IWfChartForm form, WfChartFormNode wfChartFormNode);
    }
}
