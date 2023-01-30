using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;

namespace WorkflowDiagram.Nodes.Visualization.Interfaces {
    public interface IWfPlatformGaugeService {
        object CreateCircularGauge(WfGaugeNode wfGaugeNode);
        object CreateDigitalGauge(WfGaugeNode wfGaugeNode);
        object CreateLinearGauge(WfGaugeNode wfGaugeNode);
        //object CreateStateIndicatorGauge(WfGaugeNode wfGaugeNode);
        bool ShouldRecreateGauge(object gauge);
        void UpdateCircularGauge(WfGaugeNode wfGaugeNode, object gauge);
        void UpdateDigitalGauge(WfGaugeNode wfGaugeNode, object gauge);
        void UpdateLinearGauge(WfGaugeNode wfGaugeNode, object gauge);
        object CreateGaugeUserControl(WfGaugePanelNode wfGaugePanelNode);
    }
}
