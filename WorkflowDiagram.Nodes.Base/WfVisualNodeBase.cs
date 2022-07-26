using DevExpress.LookAndFeel;
using DevExpress.Utils.Drawing;
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

namespace WorkflowDiagram.Nodes.Base {
    public abstract class WfVisualNodeBase : WfNode {
        [XmlIgnore]
        [Browsable(false)]
        public virtual WfColor Color { get { return WfColor.FromArgb(255, 255, 255, 255); } }

        protected override object CreateImage() {
            Bitmap bmp = new Bitmap(32, 32);
            using(Graphics g = Graphics.FromImage(bmp)) {
                using(GraphicsCache cache = new GraphicsCache(g))
                    GlyphPainter.Default.DrawGlyph(cache, 
                        new StubGlyphOptions() { LetterCount = GlyphTextSymbolCount.Two, CaseMode = GlyphTextCaseMode.SentenceCase, ColorMode = GlyphColorMode.All, CornerRadius = 5, RandomizeColors = true }, 
                        Type,
                        new Rectangle(0, 0, 32, 32),
                        UserLookAndFeel.Default, ObjectState.Normal);
            }
            return bmp;
        }
    }
}
