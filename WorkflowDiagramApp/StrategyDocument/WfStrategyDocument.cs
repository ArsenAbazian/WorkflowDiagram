using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WorkflowDiagramApp.StrategyDocument {
    [XmlInclude(typeof(WfTickerInputNode))]
    [XmlInclude(typeof(WfStrategyCustomProperty))]
    [XmlInclude(typeof(WfStrategyConstantValue))]
    [XmlInclude(typeof(WfStrategyConditional))]
    [XmlInclude(typeof(WfStrategyAbort))]
    public class WfStrategyDocument : WfDocument {
        public WfStrategyDocument() {
            
        }
    }
}
