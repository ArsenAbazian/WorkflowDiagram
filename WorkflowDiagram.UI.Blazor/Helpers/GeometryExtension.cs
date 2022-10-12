using System.Drawing;

namespace WorkflowDiagram.UI.Blazor.Helpers {
    public static class GeometryExtension {
        public static Point GetCenter(this RectangleF bounds) {
            int x = (int)(bounds.X + bounds.Width / 2);
            int y = (int)(bounds.Y + bounds.Height / 2);
            return new Point(x, y);
        }

        public static Point ToPoint(this PointF point) {
            return new Point((int)(point.X + 0.5f), (int)(point.Y + 0.5f));
        }

        public static RectangleF Scale(this RectangleF r, double scale) {
            return new RectangleF(
                (float)(r.X * scale),
                (float)(r.Y * scale),
                (float)(r.Width * scale),
                (float)(r.Height * scale));
        }
    }
}
