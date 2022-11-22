using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class WfNodeEventArgs : EventArgs {
        public WfNodeEventArgs(WfNode node) {
            Node = node;
        }

        public WfNode Node { get; private set; }
    }

    public delegate void WfNodeEventHandler(object sender, WfNodeEventArgs e);
}
