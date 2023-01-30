using DevExpress.Utils.Filtering.Internal;
using DevExpress.XtraGauges.Win.Base;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraGauges.Win.Gauges.Digital;
using DevExpress.XtraGauges.Win.Gauges.Linear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;
using WokflowDiagram.Nodes.Visualization.Forms;
using WokflowDiagram.Nodes.Visualization.Managers;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WorkflowDiagram.UI.Win.Platform {
    public class WinPlatformGaugeService : IWfPlatformGaugeService {
        object IWfPlatformGaugeService.CreateCircularGauge(WfGaugeNode wfGaugeNode) {
            return new CircularGauge() { Name = wfGaugeNode.Name };
        }

        object IWfPlatformGaugeService.CreateDigitalGauge(WfGaugeNode wfGaugeNode) {
            return new DigitalGauge() { Name = wfGaugeNode.Name };
        }

        object IWfPlatformGaugeService.CreateGaugeUserControl(WfGaugePanelNode wfGaugePanelNode) {
            var control = new GaugeUserControl();
            GaugeVisualizationManager.Default.InitializeGauge(wfGaugePanelNode, control);
            return control;
        }

        object IWfPlatformGaugeService.CreateLinearGauge(WfGaugeNode wfGaugeNode) {
            return new LinearGauge() { Name = wfGaugeNode.Name };
        }

        bool IWfPlatformGaugeService.ShouldRecreateGauge(object gauge) {
            BaseGaugeWin baseGaugeWin = gauge as BaseGaugeWin;
            return baseGaugeWin == null || baseGaugeWin.IsDisposing; 
        }

        void IWfPlatformGaugeService.UpdateCircularGauge(WfGaugeNode wfGaugeNode, object gauge) {
            CircularGauge cg = (CircularGauge)gauge;
            for(int i = 0; i < wfGaugeNode.Gauges.Count; i++) {
                cg.Scales[i].Value = (float)Convert.ToDouble(wfGaugeNode.Gauges[i].Value);
                cg.Scales[i].MinValue = (float)Convert.ToDouble(wfGaugeNode.Gauges[i].MinValue);
                cg.Scales[i].MaxValue = (float)Convert.ToDouble(wfGaugeNode.Gauges[i].MaxValue);
            }
        }

        void IWfPlatformGaugeService.UpdateDigitalGauge(WfGaugeNode wfGaugeNode, object gauge) {
            DigitalGauge dg = (DigitalGauge)gauge;
            dg.Text = string.Format("{0:" + wfGaugeNode.DisplayFormat + "}", wfGaugeNode.Value);
        }

        void IWfPlatformGaugeService.UpdateLinearGauge(WfGaugeNode wfGaugeNode, object gauge) {
            LinearGauge lg = (LinearGauge)gauge;
            for(int i = 0; i < wfGaugeNode.Gauges.Count; i++) {
                lg.Scales[i].Value = (float)Convert.ToDouble(wfGaugeNode.Gauges[i].Value);
                lg.Scales[i].MinValue = (float)Convert.ToDouble(wfGaugeNode.Gauges[i].MinValue);
                lg.Scales[i].MaxValue = (float)Convert.ToDouble(wfGaugeNode.Gauges[i].MaxValue);
            }
        }
    }
}
