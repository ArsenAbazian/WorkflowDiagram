using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    [Serializable]
    public struct WfColor {
        public string Name { get; set; }
        public int A { get; set; }
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public bool IsEmpty { get { return (A | R | G | B) == 0; } }

        public static WfColor FromArgb(int a, int r, int g, int b) {
            WfColor c = new WfColor();
            c.A = a; c.R = r; c.G = g; c.B = b;
            return c;
        }

        public static WfColor FromArgb(string name, int a, int r, int g, int b) {
            WfColor c = new WfColor();
            c.Name = name;
            c.A = a; c.R = r; c.G = g; c.B = b;
            return c;
        }

        public override bool Equals(object obj) {
            if(!(obj is WfColor))
                return false;
            WfColor c = (WfColor)obj;
            return A == c.A && R == c.R && G == c.G && B == c.B;
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public static bool operator ==(WfColor c1, WfColor c2) => c1.Equals(c2);
        public static bool operator !=(WfColor c1, WfColor c2) => !c1.Equals(c2);

        public override string ToString() {
            return string.Format("{{{0}, {1}, {2}, {3}}}", A, R, G, B);
        }
    }
}
