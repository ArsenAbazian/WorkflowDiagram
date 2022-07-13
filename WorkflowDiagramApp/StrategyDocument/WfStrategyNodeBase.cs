using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;

namespace WorkflowDiagramApp.StrategyDocument {
    public abstract class WfStrategyNodeBase : WfNode {
        [XmlIgnore]
        [Browsable(false)]
        public virtual string ImageName { get { return "defaultItem.png"; } }
        [XmlIgnore]
        [Browsable(false)]
        public virtual WfColor Color { get { return WfColor.FromArgb(255, 255, 255, 255); } }
        [XmlIgnore]
        [Browsable(false)]
        public Image Image { get; set; }

        protected string ConstrainStringValue(string value) {
            if(value == null)
                value = string.Empty;
            value = value.Trim();
            return value;
        }
    }
}
