using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WorkflowDiagram.Nodes.Base {
    public abstract class WfVisualNodeBase : WfNode {
        [XmlIgnore]
        [Browsable(false)]
        public virtual WfColor NodeColor { get { return WfColor.FromArgb(255, 255, 255, 255); } }
    }
}
