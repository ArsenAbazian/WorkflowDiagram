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
    [XmlInclude(typeof(WfStrategyExpression))]
    [XmlInclude(typeof(WfExpressionInputPoint))]
    [XmlInclude(typeof(WfStrategySwitch))]
    [XmlInclude(typeof(WfSwitchOutputConnectionPoint))]
    [XmlInclude(typeof(WfStrategyStorageValue))]
    [XmlInclude(typeof(WfStorageSetConnectionPoint))]
    public class WfStrategyDocument : WfDocument {
        public WfStrategyDocument() {
            
        }

        protected Dictionary<string, object> Storage { get; } = new Dictionary<string, object>();
        internal object GetStorageValue(string valueName) {
            object res = null;
            if(string.IsNullOrEmpty(valueName))
                return null;
            if(Storage.TryGetValue(valueName, out res))
                return res;
            return null;
        }

        internal void SetStorageValue(string valueName, object value) {
            Storage[valueName] = value;
        }
    }
}
