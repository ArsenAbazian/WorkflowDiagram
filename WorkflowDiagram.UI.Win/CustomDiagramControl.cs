using DevExpress.Skins;
using DevExpress.Utils.Design;
using DevExpress.Utils.DPI;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Html;
using DevExpress.Utils.Text;
using DevExpress.XtraDiagram;
using DevExpress.XtraDiagram.Paint;
using DevExpress.XtraEditors.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkflowDiagram.UI.Win {
    public class CustomDiagramControl : DiagramControl {
        public CustomDiagramControl() {
            this.htmlContextCore = new DiagramControlHtmlContext(this);
        }

        protected override DiagramControlPainter CreatePainter() {
            return new MyDiagramControlPainter();
        }

        HtmlTemplateCollection htmlTemplates;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("DevExpress.XtraEditors.Design.HtmlTemplateCollectionEditor, " + AssemblyInfo.SRAssemblyEditorsDesignFull,
            typeof(System.Drawing.Design.UITypeEditor))]
        public HtmlTemplateCollection HtmlTemplates {
            get {
                if(htmlTemplates == null) {
                    htmlTemplates = new HtmlTemplateCollection();
                    htmlTemplates.Changed += OnHtmlTemplatesChanged;
                }
                return htmlTemplates;
            }
        }

        private void OnHtmlTemplatesChanged(object sender, EventArgs e) {
            OnPropertiesChanged();
        }

        DiagramControlHtmlContext htmlContextCore;
        internal DiagramControlHtmlContext HtmlContext => htmlContextCore;

        object htmlImages;
        [DefaultValue(null), TypeConverter(typeof(ImageCollectionImagesConverter))]
        public object HtmlImages {
            get { return htmlImages; }
            set {
                if(htmlImages == value) return;
                HtmlImageCollectionHelper.UnsubscribeEvents(HtmlImages, OnHtmlImagesChanged);
                htmlImages = value;
                HtmlImageCollectionHelper.SubscribeEvents(HtmlImages, OnHtmlImagesChanged);

                OnPropertiesChanged();
            }
        }

        private void OnHtmlImagesChanged(object sender, EventArgs e) {
            OnPropertiesChanged();
        }

        protected internal void ReleaseCache(GraphicsCache cache) {
            if(DirectXProvider.Enabled)
                return;
            cache.Graphics.Dispose();
            cache.Dispose();
        }

        protected internal GraphicsCache CreateGraphicsCache() {
            if(DirectXProvider.Enabled) {
                DirectXProvider.GInfo.Cache.ScaleDPI = ScaleHelper.NoScale;
                return DirectXProvider.GInfo.Cache;
            }
            Graphics g = CreateGraphics();
            GraphicsCache cache = new GraphicsCache(g, ScaleDPI);
            return cache;
        }
    }
    
    public class MyDiagramControlPainter : DiagramControlPainter {
        protected override void DrawBackground(ControlGraphicsInfoArgs info) {
            //base.DrawBackground(info);
        }
    }

    internal class DiagramControlHtmlContext : DxHtmlContext {
        protected DiagramControl DiagramControl { get; }
        public DiagramControlHtmlContext(DiagramControl Diagram) {
            DiagramControl = Diagram;
        }
        protected override object SenderCore => DiagramControl;
        protected override object GetContainerCore() => DevExpress.Utils.Html.Internal.DxHtmlBinderHelper.FindContainer(DiagramControl);
        protected NodeItem GetNode(DxHtmlHitInfo hitInfo) {
            if(hitInfo.Root.Client != null)
                return ((NodeItem)hitInfo.Root.Client);
            return null;
        }
        //protected override DxHtmlElementMouseEventArgs CreateMouseEventArgs(DxHtmlHitInfo hitInfo, MouseEventArgs e) {
        //    return new DiagramHtmlElementEventArgs(hitInfo, e, DiagramControl, GetNode(hitInfo));
        //}
        //protected virtual DiagramHtmlElementHandledEventArgs CreateHandledMouseEventArgs(DxHtmlHitInfo hitInfo, MouseEventArgs e) {
        //    return new DiagramHtmlElementHandledEventArgs(hitInfo, e, DiagramControl, GetNode(hitInfo));
        //}
        protected virtual void ForceRedraw(RectangleF rect) {
            DiagramControl.Invalidate(new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height));
        }
        protected virtual void ForceRedraw() {
            DiagramControl.Invalidate();
        }
        protected override void RaiseMouseOutCore(DxHtmlElementMouseEventArgs e) {
            //DiagramControl.RaiseHtmlElementMouseOut((DiagramHtmlElementEventArgs)e);
        }
        protected override void RaiseMouseDownCore(DxHtmlElementMouseEventArgs e) {
            //DiagramControl.RaiseHtmlElementMouseDown(CreateHandledMouseEventArgs(e.HitInfo, e.MouseArgs));
        }
        protected override void RaiseMouseOverCore(DxHtmlElementMouseEventArgs e) {
            //DiagramControl.RaiseHtmlElementMouseOver((DiagramHtmlElementEventArgs)e);
        }
        protected override void RaiseMouseMoveCore(DxHtmlElementMouseEventArgs e) {
            //DiagramControl.RaiseHtmlElementMouseMove((DiagramHtmlElementEventArgs)e);
        }
        protected override void RaiseMouseUpCore(DxHtmlElementMouseEventArgs e) {
            //DiagramControl.RaiseHtmlElementMouseUp(CreateHandledMouseEventArgs(e.HitInfo, e.MouseArgs));
        }
        protected override void RaiseMouseClickCore(DxHtmlElementMouseEventArgs e) {
            //DiagramControl.RaiseHtmlElementMouseClick((DiagramHtmlElementEventArgs)e);
        }
        protected override void RaiseMouseDoubleClickCore(DxHtmlElementMouseEventArgs e) { }
        protected override void RaiseBlurCore(DxHtmlElementEventArgs e) { }
        protected override void RaiseFocusCore(DxHtmlElementEventArgs e) { }
        protected override void RaiseLoadCore(DxHtmlElementEventArgs e) { }
        protected override void SetCursor(DxHtmlElementBase element) {
            if(DiagramControl == null) return;
            DiagramControl.Cursor = GetCursor(element);
        }

        protected override void OnInvalidatedCallback(DxHtmlRootElement root) {
            base.OnInvalidatedCallback(root);
            NodeItem vi = root.Client as NodeItem;
            if(vi != null) {
                vi.TemplateInfoCalculated = false;
                ForceRedraw(vi.Bounds);
                return;
            }
        }
    }
}
