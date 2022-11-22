using DevExpress.Utils.Drawing;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.UI.Win {
    public class CustomPropertyGrid : PropertyGridControl {
        protected override BaseViewInfo CreateViewInfo(bool isPrinting) {
            return new CustomPropertyGridViewInfo(this, isPrinting);
        }
    }
    public class CustomPropertyGridViewInfo : PGridOfficeViewInfo {
        public CustomPropertyGridViewInfo(VGridControlBase grid, bool isPrinting) : base(grid, isPrinting) {
        }


        protected override int CalcEditorWidth(int width) {
            int textWidth = 0;
            GraphicsInfo g = new GraphicsInfo();
            g.AddGraphics(null);
            foreach(var row in RowsViewInfo) {
                CategoryRowViewInfo vi = row as CategoryRowViewInfo;
                if(vi != null) {
                    foreach(var crow in vi.Row.ChildRows) {
                        textWidth = Math.Max(textWidth, (int)PaintAppearance.FocusedRow.CalcTextSize(g.Cache, crow.Properties.Caption, -1).Width);
                    }
                }
            }
            int indent = 32;
            int rowHeaderPixelWidth = textWidth + indent; //width / 200 * Grid.RowHeaderWidth;
            return ViewRects.BandWidth - rowHeaderPixelWidth - GetRightIndent(null);
        }
    }
}
