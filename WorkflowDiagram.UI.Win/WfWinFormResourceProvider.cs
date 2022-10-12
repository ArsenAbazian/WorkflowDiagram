using DevExpress.LookAndFeel;
using DevExpress.Utils.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.UI.Win {
    public class WfWinFormResourceProvider : IWfDocumentResourcesProvider {
        object IWfDocumentResourcesProvider.GetNodeImage(WfNode node) {
            int w = DevExpress.Utils.ScaleUtils.ScaleValue(32);
            Bitmap bmp = new Bitmap(w, w);
            using(Graphics g = Graphics.FromImage(bmp)) {
                using(GraphicsCache cache = new GraphicsCache(g))
                    GlyphPainter.Default.DrawGlyph(cache,
                        new StubGlyphOptions() { LetterCount = GlyphTextSymbolCount.Two, CaseMode = GlyphTextCaseMode.SentenceCase, ColorMode = GlyphColorMode.All, CornerRadius = 5, RandomizeColors = true },
                        node.Type,
                        new Rectangle(0, 0, w, w),
                        UserLookAndFeel.Default, ObjectState.Normal);
            }
            return bmp;
        }
    }
}
