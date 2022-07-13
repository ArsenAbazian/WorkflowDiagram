using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class WfActionNode : WfNode {
        public override void OnVisit() {
            Debug.WriteLine("On Visit ActionNode: " + Name);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] { new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In1", ColorString="0,255,0" } }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] { new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Out1", ColorString="0,0,255" } }.ToList();
        }
    }
}
