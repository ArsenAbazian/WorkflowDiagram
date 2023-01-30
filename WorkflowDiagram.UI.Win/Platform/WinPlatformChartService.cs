using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WorkflowDiagram.UI.Win.Platform {
    public class WinPlatformChartService : IWfPlatformChartService {
        IWfChartForm IWfPlatformChartService.CreateChartForm(WfChartFormNode formNode) {
            throw new NotImplementedException();
        }

        object IWfPlatformChartService.CreateChartUserControl(WfChartPanelNode wfChartPanelNode) {
            throw new NotImplementedException();
        }

        object IWfPlatformChartService.CreateSeries(string name, WfChartSeriesViewType viewType) {
            throw new NotImplementedException();
        }

        void IWfPlatformChartService.InitializeChart(WfNode chartNode, object chartUserControl) {
            throw new NotImplementedException();
        }

        void IWfPlatformChartService.InitializeSeries(object s, WfNode seriesNode) {
            throw new NotImplementedException();
        }

        void IWfPlatformChartService.ShowChartForm(IWfChartForm form, WfChartFormNode wfChartFormNode) {
            throw new NotImplementedException();
        }
    }
}
