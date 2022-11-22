using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WokflowDiagram.Nodes.Visualization {
    public static class ColorHelper {
        public static Color ToColor(this WfColor c) {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static WfColor ToWfColor(this Color c) {
            return new WfColor() { A = c.A, R = c.R, G = c.G, B = c.B };
        }
    }
}
