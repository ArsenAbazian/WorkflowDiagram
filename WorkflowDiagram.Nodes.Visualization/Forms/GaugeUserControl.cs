using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Base;
using DevExpress.XtraGauges.Core;
using DevExpress.XtraGauges.Core.Base;
using DevExpress.XtraGauges.Core.Layout;
using DevExpress.XtraGauges.Win;
using DevExpress.XtraGauges.Win.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization.Utils;

namespace WokflowDiagram.Nodes.Visualization.Forms {
    public partial class GaugeUserControl : XtraUserControl, IWfDashboardControl {
        public GaugeUserControl() {
            InitializeComponent();
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            this.gaugeControl1.BackColor = CommonSkins.GetSkin(UserLookAndFeel.Default).GetSystemColor(SystemColors.Control);
        }
        public GaugeControl GaugeControl { get => this.gaugeControl1; }

        private void bsMenu_GetItemData(object sender, EventArgs e) {
            if(this.bsMenu.ItemLinks.Count == 0) { 
                foreach(var gauge in GaugeControl.Gauges) {
                    BarButtonItem item = new BarButtonItem(this.barManager1, gauge.Name);
                    item.Tag = gauge;
                    item.Caption = gauge.Name;
                    item.ItemClick += OnSelectTemplateItemClick;
                    this.bsMenu.ItemLinks.Add(item);

                    item = new BarButtonItem(this.barManager1, gauge.Name);
                    item.Tag = gauge;
                    item.Caption = gauge.Name;
                    item.ItemClick += OnCustomizeItemClick;
                    this.bsDesigner.ItemLinks.Add(item);
                }
            }
        }

        private void OnCustomizeItemClick(object sender, ItemClickEventArgs e) {
            BaseGaugeWin gauge = (BaseGaugeWin)e.Item.Tag;
            MethodInfo mi = gauge.GetType().GetMethod("RunDesigner", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if(mi != null)
                mi.Invoke(gauge, new object[] { true });
        }

        private void OnSelectTemplateItemClick(object sender, ItemClickEventArgs e) {
            StyleChooser.Show((IGauge)e.Item.Tag);
        }

        void IWfDashboardControl.OnApplyWorkspace() {
            
        }

        void IWfDashboardControl.OnInitialized() {
            
        }
    }

    public enum GaugeLayoutMode {
        Default,
        Auto,
        Horizontal,
        Vertical
    }
    public enum GaugeAlignMode {
        Default,
        Near,
        Center,
        Far
    }
    public class WfGaugeControl : GaugeControl {

        GaugeLayoutMode layoutMode = GaugeLayoutMode.Default;
        [DefaultValue(GaugeLayoutMode.Default)]
        public GaugeLayoutMode LayoutMode {
            get { return layoutMode; }
            set {
                if(LayoutMode == value)
                    return;
                layoutMode = value;
                OnPropertiesChanged();
            }
        }

        protected virtual void OnPropertiesChanged() {
            ((ILayoutManagerContainer)this).DoLayout();
            UpdateRect(Rectangle.Empty);
        }

        GaugeAlignMode alignMode = GaugeAlignMode.Default;
        [DefaultValue(GaugeAlignMode.Default)]
        public GaugeAlignMode AlignMode {
            get { return alignMode; }
            set {
                if(AlignMode == value)
                    return;
                alignMode = value;
                OnPropertiesChanged();
            }
        }

        int itemsPerLineCount = -1;
        public int ItemsPerLineCount {
            get { return itemsPerLineCount; }
            set {
                if(ItemsPerLineCount == value)
                    return;
                itemsPerLineCount = value;
                OnPropertiesChanged();
            }
        }

        protected override LayoutManager CreateLayoutManager() {
            return new WfLayoutManager((ILayoutManagerContainer)this, true);
        }
    }

    public class WfLayoutManager : LayoutManager {
        public WfLayoutManager(ILayoutManagerContainer container, bool stretchClients) : base(container, stretchClients) { }

        static Rectangle CalcContent(ILayoutManagerContainer container) {
            Rectangle bounds = container.Bounds;
            IThickness thickness = container.LayoutPadding;
            return new Rectangle(
                bounds.Left + thickness.Left,
                bounds.Top + thickness.Top,
                bounds.Width - thickness.Width,
                bounds.Height - thickness.Height);
        }

        protected WfGaugeControl WfContainer { get { return (WfGaugeControl)Container; } }

        protected override void CalcClients() {
            if(WfContainer.LayoutMode == GaugeLayoutMode.Default) {
                base.CalcClients();
                return;
            }
            Rectangle restRect = CalcContent(Container);
            int itemsCount = Container.Clients.Count;
            int itemsPerLine = WfContainer.ItemsPerLineCount == -1 ? itemsCount : Math.Min(WfContainer.ItemsPerLineCount, itemsCount);
            int linesCount = itemsCount / itemsPerLine + (itemsCount % itemsPerLine > 0 ? 1 : 0);
            GaugeLayoutMode lm = WfContainer.LayoutMode;
            if(lm == GaugeLayoutMode.Auto)
                lm = restRect.Width > restRect.Height ? GaugeLayoutMode.Horizontal : GaugeLayoutMode.Vertical;

            int lineHeight = lm == GaugeLayoutMode.Horizontal ? restRect.Height / linesCount : restRect.Width / linesCount;
            int itemSize = lm == GaugeLayoutMode.Horizontal ? restRect.Width / itemsPerLine : restRect.Height / itemsPerLine;

            int size = Math.Min(itemSize, lineHeight);
            int indent = 0;
            itemSize = size;
            if(size < lineHeight) {
                if(lm == GaugeLayoutMode.Horizontal)
                    indent = (restRect.Height - linesCount * size) / 2;
                else
                    indent = (restRect.Width - linesCount * size) / 2;
                lineHeight = size;
            }

            List<List<ILayoutManagerClient>> table = GetTable();


            int startY = Align(restRect.Y, restRect.Height, table.Count * lineHeight, GaugeAlignMode.Center);
            int startX = Align(restRect.X, restRect.Width, table.Count * lineHeight, GaugeAlignMode.Center);
            for(int li = 0; li < table.Count; li++) {
                int lineSize = table[li].Count * itemSize;
                if(lm == GaugeLayoutMode.Horizontal) {
                    int x = Align(restRect.X, restRect.Width, lineSize, WfContainer.AlignMode);
                    int y = startY + li* lineHeight;
                    for(int it = 0; it < table[li].Count; it++, x += itemSize)
                        table[li][it].Bounds = new Rectangle(x, y, itemSize, itemSize);
                }
                else {
                    int y = Align(restRect.Y, restRect.Height, lineSize, WfContainer.AlignMode);
                    int x = startX + li * lineHeight;
                    for(int it = 0; it < table[li].Count; it++, y += itemSize)
                        table[li][it].Bounds = new Rectangle(x, y, itemSize, itemSize);
                }
            }

        }

        private int Align(int containerStart, int containerWidth, int contentSize, GaugeAlignMode alignMode) {
            if(alignMode == GaugeAlignMode.Default || alignMode == GaugeAlignMode.Center)
                return containerStart + (containerWidth - contentSize) / 2;
            if(alignMode == GaugeAlignMode.Near)
                return containerStart;
            return containerStart + containerWidth - contentSize;
        }

        private List<List<ILayoutManagerClient>> GetTable() {
            int itemsCount = Container.Clients.Count;
            int itemsPerLine = WfContainer.ItemsPerLineCount == -1 ? itemsCount : Math.Min(WfContainer.ItemsPerLineCount, itemsCount);
            int linesCount = itemsCount / itemsPerLine + (itemsCount % itemsPerLine > 0 ? 1 : 0);
            List<List<ILayoutManagerClient>> table = new List<List<ILayoutManagerClient>>();
            for(int li = 0; li < linesCount; li++) {
                List<ILayoutManagerClient> line = new List<ILayoutManagerClient>();
                for(int i = li * itemsPerLine; i < li * itemsPerLine + itemsPerLine; i++) {
                    line.Add(Container.Clients[i]);
                }
                table.Add(line);
            }
            return table;
        }
    }
}
