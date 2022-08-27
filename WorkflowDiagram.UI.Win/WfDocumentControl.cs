using DevExpress.Diagram.Core;
using DevExpress.Diagram.Core.Native;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.DirectXPaint;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Drawing.Animation;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraDiagram;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.WinExplorer.ViewInfo;
using DevExpress.XtraVerticalGrid.Events;
using DevExpress.XtraVerticalGrid.Rows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkflowDiagram.Editors;

namespace WorkflowDiagram.UI.Win {
    public partial class WfDocumentControl : XtraUserControl, ISupportXtraAnimation {
        public WfDocumentControl() {
            InitializeComponent();
            InitializeConnectorViewTypes();

            this.connectionsEditor1.ShowPointProperties += OnShowPointProperties;
            this.connectionsEditor2.ShowPointProperties += OnShowPointProperties;

            this.diagramControl1.OptionsBehavior.ActiveTool = diagramControl1.OptionsBehavior.PointerTool;
            this.diagramControl1.PaintEx += OnDiagramPaintX;
            UserLookAndFeel.Default.StyleChanged += OnLookAndFeelChanged;
        }

        private void OnLookAndFeelChanged(object sender, EventArgs e) {
            UpdateControlsBackground();
            this.diagramDataBindingController1.Refresh();
        }

        private void OnShowPointProperties(object sender, WfPointValueEventArgs e) {
            this.cpgValue.SelectedObject = e.Point.Value;
            this.dpValue.Show();
        }

        private void OnDiagramPaintX(object sender, XtraPaintEventArgs e) {
            if(DocumentAfterLoad != null)
                Document = DocumentAfterLoad;
        }

        public WfDocumentControl(WfDocument document) : this(document, null) { }

        public WfDocumentControl(WfDocument document, IWfDocumentOwner owner) : this() {
            DocumentAfterLoad = document;
            DocumentOwner = owner;
            //Document = document;
        }

        public IWfDocumentOwner DocumentOwner { get; private set; }
        private void InitializeConnectorViewTypes() {
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem("Curved", ConnectorType.Curved));
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem("OrgChart", ConnectorType.OrgChart));
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem("RightAngle", ConnectorType.RightAngle));
            this.repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem("Straight", ConnectorType.Straight));
            this.beConnectorViewType.EditValue = ConnectorType.Curved;
        }

        private void beConnectorViewType_EditValueChanged(object sender, EventArgs e) {
            foreach(IDiagramConnector conn in this.diagramControl1.Connectors()) {
                conn.Type = (ConnectorType)this.beConnectorViewType.EditValue;
            }
            this.diagramControl1.UpdateConnectorsRoute(this.diagramControl1.Connectors());
        }

        protected WfDocument DocumentAfterLoad { get; private set; }
        WfDocument document;
        public WfDocument Document {
            get { return document; }
            set {
                if(Document == value)
                    return;
                document = value;
                DocumentAfterLoad = null;
                OnDocumentChanged();
            }
        }

        public void RefreshData() {
            this.gvDiagnostics.RefreshData();
        }

        protected virtual void OnDocumentChanged() {
            Document.InitializeVisualData();
            this.beFontSize.EditValue = Document.FontSizeDelta;
            this.pgcProperties.SelectedObject = Document;
            this.gridControl1.DataSource = Document.GetAvailableToolbarItems();
            //this.wfNodeBindingSource.DataSource = Document.GetAvailableToolbarItems();
            this.diagramDataBindingController1.DataSource = Document.Nodes;
            this.diagramDataBindingController1.ConnectorsSource = Document.Connectors;
            this.diagramControl1.OptionsBehavior.ActiveTool = diagramControl1.OptionsBehavior.PointerTool;
            this.diagramControl1.FitToDrawing();
            this.gcDiagnostics.DataSource = Document.Diagnostics;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(string.IsNullOrEmpty(Document.FileName)) {
                if(xtraSaveFileDialog1.ShowDialog() == DialogResult.OK) {
                    Document.Save(xtraSaveFileDialog1.FileName);
                }
            }
            else {
                try {
                    Document.Save();
                }
                catch(Exception ee) {
                    XtraMessageBox.Show("Cannot save file. Error: " + ee.ToString());
                }
            }
            this.alertControl1.AutoFormDelay = 2000;
            this.alertControl1.AppearanceText.FontSizeDelta = 2;
            this.alertControl1.AppearanceCaption.FontSizeDelta = 2;
            this.alertControl1.AllowHotTrack = false;
            this.alertControl1.Show(new AlertInfo("Save", "Document Successfullty Saved...") { SvgImage = this.svgImageCollection2["success"] }, FindForm());
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        public void FitToDrawing() {
            this.diagramControl1.FitToDrawing();
        }

        private void bbiFitToContent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.diagramControl1.FitToDrawing();
        }

        private void bbiZoomItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.diagramControl1.FitToItems(this.diagramControl1.SelectedItems);
        }

        private void bciAnimateFlow_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            AnimationEnabled = this.bciAnimateFlow.Checked;
        }

        private object AnimationId = new object();
        bool animationEnabled = false;
        public bool AnimationEnabled {
            get { return animationEnabled; }
            set {
                if(AnimationEnabled == value)
                    return;
                animationEnabled = value;
                OnAnimationEnabledChanged();
            }
        }
        protected CustomAnimationInfo AnimationInfo { get; set; }
        protected float AnimationProgress { get; set; }

        protected int FpsCount { get; set; } = 0;
        private void OnAnimationEnabledChanged() {
            this.bciAnimateFlow.Checked = AnimationEnabled;
            if(!AnimationEnabled) {
                if(AnimationInfo != null)
                    XtraAnimator.Current.Animations.Remove(AnimationInfo);
                AnimationInfo = null;
                return;
            }
            else {
                CustomAnimationInfo info = new CustomAnimationInfo(this, AnimationId, (int)(TimeSpan.TicksPerMillisecond * 15), int.MaxValue, (ee) => {
                    float routeSeconds = 2.0f;
                    float elapsedSeconds = AnimationInfo.ElapsedTicks / (float)TimeSpan.TicksPerSecond;
                    FpsCount++;
                    AnimationProgress = elapsedSeconds / routeSeconds;
                    ((IDirectXClient)this.diagramControl1).DirectXProvider.Render();
                });
                info.RunThread = ((IDirectXClient)this.diagramControl1).DirectXProvider.Enabled;
                AnimationInfo = info;
                XtraAnimator.Current.AddAnimation(info);
            }
        }


        Control ISupportXtraAnimation.OwnerControl => this.diagramControl1;

        bool ISupportXtraAnimation.CanAnimate => true;

        public void propertyGridControl1_CustomRecordCellEdit(object sender, GetCustomRowCellEditEventArgs e) {
            if(this.pgcProperties.SelectedObject == null)
                return;
            object rec = this.pgcProperties.GetRecordObject(e.RecordIndex);
            PropertyInfo pi = rec.GetType().GetProperty(e.Row.Properties.FieldName, BindingFlags.Instance | BindingFlags.Public);
            if(pi == null)
                return;
            if(pi.PropertyType == typeof(Color)) {
                e.RepositoryItem = new RepositoryItemColorPickEdit();
                return;
            }
            var attr = pi.GetCustomAttribute<PropertyEditorAttribute>();
            if(attr == null)
                return;
            var constr = attr.EditorType.GetConstructor(new Type[] { });
            IPropertyEditor ieditor = null;
            if(constr != null) {
                if(attr.EditorType.IsSubclassOf(typeof(RepositoryItem))) {
                    RepositoryItem item = (RepositoryItem)constr.Invoke(new object[] { });
                    e.RepositoryItem = item;
                    ieditor = item as IPropertyEditor;
                }
                if(ieditor != null)
                    ieditor.Initialize(rec, e.Row.Properties.FieldName, pi.GetValue(rec));
                return;
            }
        }

        private void propertyGridControl1_Resize(object sender, EventArgs e) {

        }

        private void propertyGridControl1_SizeChanged(object sender, EventArgs e) {
            this.pgcProperties.BestFit();
        }

        private void propertyGridControl1_SelectedChanged(object sender, SelectedChangedEventArgs e) {
            this.pgcProperties.BestFit();
        }

        private void propertyGridControl1_CellValueChanged(object sender, CellValueChangedEventArgs e) {

        }

        private void propertyGridControl1_ShowingEditor(object sender, CancelEventArgs e) {

        }

        private void barEditItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e) {
            Document.FontSizeDelta = Convert.ToInt32(this.beFontSize.EditValue);
            this.diagramDataBindingController1.Refresh();
        }

        static Color ColorFromWfColor(WfColor c) {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        private void diagramControl1_DragDrop(object sender, DragEventArgs e) {
            List<WfNode> templateNodes = GetDropNodes(e);
            Point cp = this.diagramControl1.PointToClient(new Point(e.X, e.Y));
            foreach(WfNode node in templateNodes) {
                PointF pt = this.diagramControl1.PointToDocument(new PointFloat(cp.X, cp.Y));
                node.X = pt.X;
                node.Y = pt.Y;
                Document.Nodes.Add(node);
            }
        }

        protected Font SavedConnectorTemplateFont { get; set; }
        public RibbonControl Ribbon { get { return this.ribbonControl1; } }

        private void diagramDataBindingController1_GenerateConnector(object sender, DevExpress.XtraDiagram.DiagramGenerateConnectorEventArgs e) {
            WfConnector c = e.DataObject as WfConnector;
            e.Connector = e.CreateConnectorFromTemplate("ConnectorTemplate");
            e.Connector.Type = (ConnectorType)this.beConnectorViewType.EditValue;
            if(SavedConnectorTemplateFont == null)
                SavedConnectorTemplateFont = e.Connector.Appearance.Font;
            e.Connector.Appearance.FontSizeDelta = Document.FontSizeDelta;
            e.Connector.Appearance.ContentBackground = CommonSkins.GetSkin(this.diagramControl1.LookAndFeel).GetSystemColor(SystemColors.Window);
            UpdateConnectorStyle(c, e.Connector);
            e.Connector.BeginItemPointIndex = c.FromIndex;
            e.Connector.EndItemPointIndex = c.ToIndex;
            if(this.bciShowConnectorText.Checked)
                e.Connector.Content = c.From.Connectors.IndexOf(c).ToString();
        }

        private void diagramDataBindingController1_DiagramConnectorAdding(object sender, DevExpress.XtraDiagram.DiagramDiagramConnectorAddingEventArgs e) {
            DiagramItem fromItem = (DiagramItem)e.Connector.BeginItem;
            DiagramItem toItem = (DiagramItem)e.Connector.EndItem;

            WfNode from = (WfNode)fromItem.DataContext;
            WfNode to = (WfNode)toItem.DataContext;

            WfConnector c = new WfConnector();
            WfConnectionPoint fromPoint = e.Connector.BeginItemPointIndex >= 0 ? from.Points[e.Connector.BeginItemPointIndex] : null;
            WfConnectionPoint toPoint = e.Connector.EndItemPointIndex >= 0 ? to.Points[e.Connector.EndItemPointIndex] : null;
            if(fromPoint == null && from.Outputs.Count > 0)
                fromPoint = from.Outputs[0];
            if(toPoint == null && to.Inputs.Count > 0)
                toPoint = to.Inputs[0];
            
            if(fromPoint == null || fromPoint.Type != WfConnectionPointType.Out) {
                e.Cancel = true;
                return;
            }
            if(toPoint == null || toPoint.Type != WfConnectionPointType.In) {
                e.Cancel = true;
                return;
            }

            if(fromPoint == toPoint) {
                e.Cancel = true;
                return;
            }

            Document.AddConnector(c);
            c.From = fromPoint;
            c.To = toPoint;
            
            e.DataItem = c;
            e.Connector.DataContext = c;
        }

        WinExplorerItemViewInfo downItem;
        Point downPoint = Point.Empty;
        private void wevToolbar_MouseDown(object sender, MouseEventArgs e) {
            var hitInfo = this.wevToolbar.CalcHitInfo(e.Location);
            this.downItem = hitInfo.InItem ? hitInfo.ItemInfo : null;
            this.downPoint = e.Location;
        }

        private void wevToolbar_MouseMove(object sender, MouseEventArgs e) {
            if(this.downItem == null)
                return;
            Point delta = new Point(e.Location.X - this.downPoint.X, e.Location.Y - this.downPoint.Y);
            if(delta.X > SystemInformation.DragSize.Width ||
                delta.Y > SystemInformation.DragSize.Height) {
                this.gridControl1.DoDragDrop(((WfNode)downItem.Row.RowKey).Clone(), DragDropEffects.Move);
                this.downItem = null;
                this.wevToolbar.RefreshData();
            }
        }

        private void wevToolbar_MouseUp(object sender, MouseEventArgs e) {
            this.downItem = null;
        }

        private void diagramControl1_DragOver(object sender, DragEventArgs e) {
            e.Effect = GetDropNodes(e).Count > 0? DragDropEffects.Move: DragDropEffects.None;
        }
         
        private void diagramControl1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = GetDropNodes(e).Count > 0 ? DragDropEffects.Move : DragDropEffects.None;
        }

        protected List<WfNode> GetDropNodes(DragEventArgs e) {
            var formats = e.Data.GetFormats();
            if(formats == null || formats.Length == 0)
                return null;
            List<WfNode> res = new List<WfNode>();
            foreach(var format in formats) {
                WfNode node = e.Data.GetData(format) as WfNode;
                if(node != null)
                    res.Add(node);
            }
            return res;
        }

        private void diagramDataBindingController1_GenerateItem(object sender, DiagramGenerateItemEventArgs e) {
            WfNode node = e.DataObject as WfNode;
            WfConnector connector = e.DataObject as WfConnector;

            if(node != null) {
                e.Item = CreateNodeItem(e, node);
            }
            else if(connector != null) {
                e.Item = CreateConnector(e, connector);
            }
        }

        protected virtual DiagramItem CreateConnector(DiagramGenerateItemEventArgs e, WfConnector connector) {
            var res = e.CreateItemFromTemplate("ConnectorTemplate");
            res.DataContext = connector;
            return res;
        }

        protected virtual DiagramItem CreateNodeItem(DiagramGenerateItemEventArgs e, WfNode node) {
            return new NodeItem(node, this.diagramControl1);
        }

        private void bbiZoom100_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.diagramControl1.OptionsView.ZoomFactor = 1;
        }

        Color BlendColor(Color bg, Color c) {
            float a = 0.1f;
            return Color.FromArgb((int)(a * c.R + (1 - a) * bg.R), (int)(a * c.G + (1 - a) * bg.G), (int)(a * c.B + (1 - a) * bg.B));
        }
        void UpdateControlsBackground() {
            this.wevToolbar.Appearance.EmptySpace.BackColor = GetBackgroundColor();
        }
        Color GetBackgroundColor() {
            Color bg = LookAndFeelHelper.GetSystemColor(UserLookAndFeel.Default, SystemColors.Window);
            Color fg = LookAndFeelHelper.GetSystemColor(UserLookAndFeel.Default, SystemColors.WindowText);
            return BlendColor(bg, fg);
        }
        protected override void OnHandleCreated(EventArgs e) {
            UpdateControlsBackground();
            base.OnHandleCreated(e);
        }

        private void diagramControl1_SelectionChanged(object sender, DiagramSelectionChangedEventArgs e) {
            var items = this.diagramControl1.SelectedItems;
            var item = items.Count == 0 ? null : items[0];
            if(!IsValidObjects(items))
                return;
            UpdateProperties(item, items);
        }

        private bool IsValidObjects(ReadOnlyCollection<DiagramItem> items) {
            return items.FirstOrDefault(i => i.DataContext == null) == null;
        }

        private object[] GetSelectedItems(ReadOnlyCollection<DiagramItem> items) {
            return items.Select(i => i.DataContext).ToArray();
        }

        private bool IsSameObjectTypes(object[] selItems) {
            Type tp = selItems[0].GetType();
            foreach(object item in selItems) {
                if(item.GetType() != tp)
                    return false;
            }
            return true;
        }

        private void UpdateProperties(DiagramItem item, ReadOnlyCollection<DiagramItem> items) {
            if(item != null) {
                object[] selItems = GetSelectedItems(items);
                if(IsSameObjectTypes(selItems))
                    this.pgcProperties.SelectedObjects = selItems;
                else
                    this.pgcProperties.SelectedObject = item.DataContext;
                if(selItems[0] is WfNode) {
                    this.connectionsEditor1.Connections = ((WfNode)selItems[0]).Inputs;
                    this.connectionsEditor2.Connections = ((WfNode)selItems[0]).Outputs;
                    this.gcDiagnostics.DataSource = ((WfNode)selItems[0]).Diagnostic;
                }
                else {
                    this.connectionsEditor1.Connections = null;
                    this.connectionsEditor2.Connections = null;
                    this.gcDiagnostics.DataSource = null;
                }
            }
            else {
                this.pgcProperties.SelectedObject = null;
                this.connectionsEditor1.Connections = null;
                this.connectionsEditor2.Connections = null;
                this.gcDiagnostics.DataSource = null;
            }
            //UpdateBarItems();
        }

        private void diagramDataBindingController1_CustomLayoutItems(object sender, DiagramCustomLayoutItemsEventArgs e) {
            e.Handled = true;
        }

        private void bciSelectionMode_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.diagramControl1.OptionsBehavior.ActiveTool = diagramControl1.OptionsBehavior.PointerTool;
        }

        private void bciConnectionMode_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.diagramControl1.OptionsBehavior.ActiveTool = diagramControl1.OptionsBehavior.ConnectorTool;
        }

        private void diagramControl1_ItemsResizing(object sender, DiagramItemsResizingEventArgs e) {
            foreach(var item in e.Items) {
                item.NewSize = new SizeF(item.NewSize.Width, item.CurrentSize.Height);
            }
        }

        private void bciShowConnectorText_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.diagramDataBindingController1.Refresh();
        }

        private void diagramControl1_QueryConnectionPoints(object sender, DiagramQueryConnectionPointsEventArgs e) {
            e.ItemConnectionBorderState = ConnectionElementState.Disabled;
        }

        private void diagramControl1_CustomDrawBackground(object sender, CustomDrawBackgroundEventArgs e) {
            e.GraphicsCache.FillRectangle(GetBackgroundColor(), e.ViewportBounds);
        }

        private void diagramDataBindingController1_UpdateConnector(object sender, DiagramUpdateConnectorEventArgs e) {
            WfConnector conn = e.Connector.DataContext as WfConnector;
            UpdateConnectorStyle(conn, e.Connector);
        }

        protected void UpdateConnectorStyle(WfConnector conn, DiagramConnector dc) {
            if(conn == null)
                return;
            if(conn.LastVisited) {
                dc.Appearance.BorderColor =
                dc.Appearance.ForeColor =
                dc.Appearance.BackColor = DXSkinColors.FillColors.Success;
            }
            else {
                dc.Appearance.BorderColor =
                dc.Appearance.ForeColor =
                dc.Appearance.BackColor = DXSkinColors.FillColors.Warning;
            }
        }

        bool deletingInProcess;
        private void diagramControl1_ItemsDeleting(object sender, DiagramItemsDeletingEventArgs e) {
            if(this.deletingInProcess)
                return;
            this.deletingInProcess = true;
            this.diagramControl1.BeginUpdate();
            try {
                foreach(DiagramItem item in e.Items) {
                    WfNode node = item.DataContext as WfNode;
                    if(node != null)
                        Document.RemoveNode(node);
                }
                foreach(DiagramItem item in e.Items) {
                    WfConnector c = item.DataContext as WfConnector;
                    if(c != null)
                        c.Detach();
                }
            }
            finally {
                this.deletingInProcess = false;
                this.diagramControl1.EndUpdate();
            }
        }

        private void diagramControl1_CustomDrawItem(object sender, CustomDrawItemEventArgs e) {
            WfConnector conn = e.Item.DataContext as WfConnector;
            if(conn != null) {
                if(!AnimationEnabled)
                    return;
                if(!conn.From.IsVisitedByRunner)
                    return;
                DiagramConnector c = (DiagramConnector)e.Item;
                e.DefaultDraw(CustomDrawItemMode.Background);
                int count = (int)(AnimationProgress / 0.2f);
                float start = AnimationProgress - 0.2f * count;
                float end = Math.Min(AnimationProgress, 1.0f);
                for(float k = start; k <= end; k += 0.2f) {
                    float len = k > 1.0f ? k - 1.0f : k;
                    System.Windows.Point pt = ((IDiagramConnector)c).CalcLinePoint(k);
                    e.GraphicsCache.FillEllipse((float)pt.X - 3.0f, (float)pt.Y - 3.0f, 6.0f, 6.0f, c.Appearance.BorderColor);
                }

                e.DefaultDraw(CustomDrawItemMode.Content);
                return;
            }
        }

        private void biOwnerProperties_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.pgcProperties.SelectedObject = Document.Owner;
        }

        private void gvDiagnostics_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            
        }
    }
}
