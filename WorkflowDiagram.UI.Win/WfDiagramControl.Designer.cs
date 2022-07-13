
using DevExpress.XtraVerticalGrid.Events;
using System;
using System.ComponentModel;

namespace WorkflowDiagram.UI.Win {
    partial class WfDiagramControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WfDiagramControl));
            this.bbiOpen = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.bbiFitToContent = new DevExpress.XtraBars.BarButtonItem();
            this.bbiZoomItem = new DevExpress.XtraBars.BarButtonItem();
            this.bciAnimateFlow = new DevExpress.XtraBars.BarCheckItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.beFontSize = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bciShowConnectorText = new DevExpress.XtraBars.BarCheckItem();
            this.bbiZoom100 = new DevExpress.XtraBars.BarButtonItem();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.bciSelectionMode = new DevExpress.XtraBars.BarCheckItem();
            this.bciConnectionMode = new DevExpress.XtraBars.BarCheckItem();
            this.beConnectorViewType = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.standaloneBarDockControl2 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.wfNodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.wevToolbar = new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.sidePanel2 = new DevExpress.XtraEditors.SidePanel();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.propertyGridControl1 = new WorkflowDiagram.UI.Win.CustomPropertyGrid();
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.sankeyDiagramControl1 = new DevExpress.XtraCharts.Sankey.SankeyDiagramControl();
            this.diagramDataBindingController1 = new DevExpress.XtraDiagram.DiagramDataBindingController(this.components);
            this.diagramControl1 = new WorkflowDiagram.UI.Win.CustomDiagramControl();
            this.Default = new DevExpress.Utils.Html.HtmlTemplate();
            this.InRow = new DevExpress.Utils.Html.HtmlTemplate();
            this.OutRow = new DevExpress.Utils.Html.HtmlTemplate();
            this.diagramConnector1 = new DevExpress.XtraDiagram.DiagramConnector();
            this.diagramContainer1 = new DevExpress.XtraDiagram.DiagramContainer();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barDockingMenuItem1 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.diagramBarController1 = new DevExpress.XtraDiagram.Bars.DiagramBarController(this.components);
            this.PrintMenuPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ExportAsPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ImageToolsBringToFrontContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ImageToolsSendToBackContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ToolsContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.BringToFrontPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.SendToBackPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.sidePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfNodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wevToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            this.sidePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramDataBindingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramDataBindingController1.TemplateDiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintMenuPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExportAsPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsBringToFrontContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsSendToBackContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BringToFrontPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SendToBackPopupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // bbiOpen
            // 
            this.bbiOpen.Caption = "&Open";
            this.bbiOpen.Id = 0;
            this.bbiOpen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiOpen.ImageOptions.SvgImage")));
            this.bbiOpen.Name = "bbiOpen";
            this.bbiOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // bbiNew
            // 
            this.bbiNew.Caption = "&New";
            this.bbiNew.Id = 1;
            this.bbiNew.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiNew.ImageOptions.SvgImage")));
            this.bbiNew.Name = "bbiNew";
            // 
            // bbiSave
            // 
            this.bbiSave.Caption = "&Save";
            this.bbiSave.Id = 2;
            this.bbiSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiSave.ImageOptions.SvgImage")));
            this.bbiSave.Name = "bbiSave";
            this.bbiSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // bbiSaveAs
            // 
            this.bbiSaveAs.Caption = "Save &As";
            this.bbiSaveAs.Id = 3;
            this.bbiSaveAs.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiSaveAs.ImageOptions.SvgImage")));
            this.bbiSaveAs.Name = "bbiSaveAs";
            // 
            // bbiFitToContent
            // 
            this.bbiFitToContent.Caption = "ZoomAll";
            this.bbiFitToContent.Id = 4;
            this.bbiFitToContent.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiFitToContent.ImageOptions.SvgImage")));
            this.bbiFitToContent.Name = "bbiFitToContent";
            this.bbiFitToContent.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiFitToContent_ItemClick);
            // 
            // bbiZoomItem
            // 
            this.bbiZoomItem.Caption = "Zoom To Item";
            this.bbiZoomItem.Id = 5;
            this.bbiZoomItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bbiZoomItem.ImageOptions.SvgImage")));
            this.bbiZoomItem.Name = "bbiZoomItem";
            this.bbiZoomItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiZoomItem_ItemClick);
            // 
            // bciAnimateFlow
            // 
            this.bciAnimateFlow.Caption = "Animate Flow";
            this.bciAnimateFlow.Id = 7;
            this.bciAnimateFlow.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bciAnimateFlow.ImageOptions.SvgImage")));
            this.bciAnimateFlow.Name = "bciAnimateFlow";
            this.bciAnimateFlow.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bciAnimateFlow_CheckedChanged);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barSubItem1.Caption = "View Options";
            this.barSubItem1.Id = 8;
            this.barSubItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barSubItem1.ImageOptions.SvgImage")));
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.beFontSize)});
            this.barSubItem1.Name = "barSubItem1";
            this.barSubItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // beFontSize
            // 
            this.beFontSize.Caption = "Font Size";
            this.beFontSize.Edit = this.repositoryItemSpinEdit1;
            this.beFontSize.EditWidth = 100;
            this.beFontSize.Id = 9;
            this.beFontSize.Name = "beFontSize";
            this.beFontSize.EditValueChanged += new System.EventHandler(this.barEditItem1_EditValueChanged);
            this.beFontSize.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEditItem1_ItemClick);
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.DisplayFormat.FormatString = "n0";
            this.repositoryItemSpinEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemSpinEdit1.Mask.EditMask = "n0";
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PropertiesRibbon.DefaultSimplifiedRibbonGlyphSize = 24;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar4});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl2);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiFitToContent,
            this.bbiZoomItem,
            this.bciAnimateFlow,
            this.beFontSize,
            this.barSubItem1,
            this.bbiZoom100,
            this.bciSelectionMode,
            this.bciConnectionMode,
            this.bciShowConnectorText,
            this.beConnectorViewType});
            this.barManager1.MaxItemId = 7;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemImageComboBox1});
            // 
            // bar2
            // 
            this.bar2.BarName = "Custom 2";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar2.FloatLocation = new System.Drawing.Point(311, 573);
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiFitToContent),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiZoomItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciShowConnectorText),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciAnimateFlow),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiZoom100),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar2.Text = "Custom 2";
            // 
            // bciShowConnectorText
            // 
            this.bciShowConnectorText.Caption = "Show Connectors Order Indices";
            this.bciShowConnectorText.Id = 4;
            this.bciShowConnectorText.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bciShowConnectorText.ImageOptions.SvgImage")));
            this.bciShowConnectorText.Name = "bciShowConnectorText";
            this.bciShowConnectorText.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bciShowConnectorText_CheckedChanged);
            // 
            // bbiZoom100
            // 
            this.bbiZoom100.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.bbiZoom100.Caption = "100%";
            this.bbiZoom100.Id = 0;
            this.bbiZoom100.Name = "bbiZoom100";
            this.bbiZoom100.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiZoom100_ItemClick);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(3, 852);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Margin = new System.Windows.Forms.Padding(6);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1540, 44);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // bar4
            // 
            this.bar4.BarName = "Custom 3";
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Standalone;
            this.bar4.FloatLocation = new System.Drawing.Point(320, 202);
            this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bciSelectionMode),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciConnectionMode),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.beConnectorViewType, "", false, true, true, 167)});
            this.bar4.OptionsBar.DrawBorder = false;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.StandaloneBarDockControl = this.standaloneBarDockControl2;
            this.bar4.Text = "Custom 3";
            // 
            // bciSelectionMode
            // 
            this.bciSelectionMode.BindableChecked = true;
            this.bciSelectionMode.Caption = "Selection Mode";
            this.bciSelectionMode.Checked = true;
            this.bciSelectionMode.GroupIndex = 33;
            this.bciSelectionMode.Id = 2;
            this.bciSelectionMode.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bciSelectionMode.ImageOptions.SvgImage")));
            this.bciSelectionMode.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Escape);
            this.bciSelectionMode.Name = "bciSelectionMode";
            this.bciSelectionMode.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bciSelectionMode_CheckedChanged);
            // 
            // bciConnectionMode
            // 
            this.bciConnectionMode.Caption = "barCheckItem2";
            this.bciConnectionMode.GroupIndex = 33;
            this.bciConnectionMode.Id = 3;
            this.bciConnectionMode.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bciConnectionMode.ImageOptions.SvgImage")));
            this.bciConnectionMode.Name = "bciConnectionMode";
            this.bciConnectionMode.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bciConnectionMode_CheckedChanged);
            // 
            // beConnectorViewType
            // 
            this.beConnectorViewType.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.beConnectorViewType.Caption = "Connector Type";
            this.beConnectorViewType.Edit = this.repositoryItemImageComboBox1;
            this.beConnectorViewType.Id = 6;
            this.beConnectorViewType.Name = "beConnectorViewType";
            this.beConnectorViewType.EditValueChanged += new System.EventHandler(this.beConnectorViewType_EditValueChanged);
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // standaloneBarDockControl2
            // 
            this.standaloneBarDockControl2.AutoSize = true;
            this.standaloneBarDockControl2.CausesValidation = false;
            this.standaloneBarDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(3, 45);
            this.standaloneBarDockControl2.Manager = this.barManager1;
            this.standaloneBarDockControl2.Margin = new System.Windows.Forms.Padding(6);
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(1540, 46);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlTop.Size = new System.Drawing.Size(2450, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 975);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlBottom.Size = new System.Drawing.Size(2450, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 975);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(2450, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 975);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.groupControl1);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel1.Location = new System.Drawing.Point(0, 76);
            this.sidePanel1.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(470, 899);
            this.sidePanel1.TabIndex = 6;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Margin = new System.Windows.Forms.Padding(6);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(468, 899);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Toolbox";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.wfNodeBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Location = new System.Drawing.Point(3, 45);
            this.gridControl1.MainView = this.wevToolbar;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(462, 851);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.wevToolbar});
            // 
            // wfNodeBindingSource
            // 
            this.wfNodeBindingSource.DataSource = typeof(WorkflowDiagram.WfNode);
            // 
            // wevToolbar
            // 
            this.wevToolbar.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colText,
            this.colType,
            this.colCategory,
            this.colDescription});
            this.wevToolbar.ColumnSet.GroupColumn = this.colCategory;
            this.wevToolbar.GridControl = this.gridControl1;
            this.wevToolbar.GroupCount = 1;
            this.wevToolbar.HtmlImages = this.svgImageCollection1;
            this.wevToolbar.Name = "wevToolbar";
            this.wevToolbar.OptionsView.Style = DevExpress.XtraGrid.Views.WinExplorer.WinExplorerViewStyle.Content;
            // 
            // 
            // 
            this.wevToolbar.OptionsViewStyles.Content.HtmlTemplate.Styles = resources.GetString("wevToolbar.OptionsViewStyles.Content.HtmlTemplate.Styles");
            this.wevToolbar.OptionsViewStyles.Content.HtmlTemplate.Template = "<div class=\"button\">\r\n\t<img src=\"${Image}\" class=\"image\">\r\n\t<div class=\"text\">${D" +
    "isplayText}</div>\r\n</div>";
            this.wevToolbar.OptionsViewStyles.Content.ItemWidth = 462;
            this.wevToolbar.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCategory, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.wevToolbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.wevToolbar_MouseDown);
            this.wevToolbar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.wevToolbar_MouseUp);
            this.wevToolbar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.wevToolbar_MouseMove);
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 40;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 150;
            // 
            // colText
            // 
            this.colText.FieldName = "Text";
            this.colText.MinWidth = 40;
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 1;
            this.colText.Width = 150;
            // 
            // colType
            // 
            this.colType.FieldName = "DisplayText";
            this.colType.MinWidth = 40;
            this.colType.Name = "colType";
            this.colType.OptionsColumn.ReadOnly = true;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 2;
            this.colType.Width = 150;
            // 
            // colCategory
            // 
            this.colCategory.FieldName = "Category";
            this.colCategory.MinWidth = 40;
            this.colCategory.Name = "colCategory";
            this.colCategory.OptionsColumn.ReadOnly = true;
            this.colCategory.Visible = true;
            this.colCategory.VisibleIndex = 3;
            this.colCategory.Width = 150;
            // 
            // colDescription
            // 
            this.colDescription.FieldName = "Description";
            this.colDescription.MinWidth = 40;
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 4;
            this.colDescription.Width = 150;
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("input", "image://svgimages/spreadsheet/pivottablegroupselection.svg");
            // 
            // sidePanel2
            // 
            this.sidePanel2.Controls.Add(this.groupControl3);
            this.sidePanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.sidePanel2.Location = new System.Drawing.Point(2016, 76);
            this.sidePanel2.Margin = new System.Windows.Forms.Padding(6);
            this.sidePanel2.Name = "sidePanel2";
            this.sidePanel2.Size = new System.Drawing.Size(434, 899);
            this.sidePanel2.TabIndex = 12;
            this.sidePanel2.Text = "sidePanel2";
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.propertyGridControl1);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(2, 0);
            this.groupControl3.Margin = new System.Windows.Forms.Padding(6);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(432, 899);
            this.groupControl3.TabIndex = 1;
            this.groupControl3.Text = "Properties";
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.ActiveViewType = DevExpress.XtraVerticalGrid.PropertyGridView.Office;
            this.propertyGridControl1.BandsInterval = 4;
            this.propertyGridControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.propertyGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControl1.Location = new System.Drawing.Point(3, 45);
            this.propertyGridControl1.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.propertyGridControl1.OptionsView.FixedLineWidth = 4;
            this.propertyGridControl1.OptionsView.LevelIndent = 8;
            this.propertyGridControl1.OptionsView.MinRowAutoHeight = 40;
            this.propertyGridControl1.OptionsView.ShowRootLevelIndent = false;
            this.propertyGridControl1.Size = new System.Drawing.Size(426, 851);
            this.propertyGridControl1.TabIndex = 0;
            this.propertyGridControl1.CustomRecordCellEdit += new DevExpress.XtraVerticalGrid.Events.GetCustomRowCellEditEventHandler(this.propertyGridControl1_CustomRecordCellEdit);
            this.propertyGridControl1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.propertyGridControl1_ShowingEditor);
            this.propertyGridControl1.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.propertyGridControl1_CellValueChanged);
            this.propertyGridControl1.SelectedChanged += new DevExpress.XtraVerticalGrid.Events.SelectedChangedHandler(this.propertyGridControl1_SelectedChanged);
            this.propertyGridControl1.SizeChanged += new System.EventHandler(this.propertyGridControl1_SizeChanged);
            this.propertyGridControl1.Resize += new System.EventHandler(this.propertyGridControl1_Resize);
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "*.xml";
            this.xtraSaveFileDialog1.Filter = "Workflow Document File (*.xml)|*.xml|All files (*.*)|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
            // 
            // sankeyDiagramControl1
            // 
            this.sankeyDiagramControl1.Location = new System.Drawing.Point(0, 0);
            this.sankeyDiagramControl1.Name = "sankeyDiagramControl1";
            this.sankeyDiagramControl1.Size = new System.Drawing.Size(300, 300);
            this.sankeyDiagramControl1.TabIndex = 0;
            this.sankeyDiagramControl1.Text = "sankeyDiagramControl1";
            // 
            // diagramDataBindingController1
            // 
            this.diagramDataBindingController1.ConnectorFromMember = "FromNodeId";
            this.diagramDataBindingController1.ConnectorKeyMember = "Id";
            this.diagramDataBindingController1.ConnectorsSource = null;
            this.diagramDataBindingController1.ConnectorToMember = "ToNodeId";
            this.diagramDataBindingController1.Diagram = this.diagramControl1;
            this.diagramDataBindingController1.ItemsPath = "";
            this.diagramDataBindingController1.KeyMember = "Id";
            // 
            // 
            // 
            this.diagramDataBindingController1.TemplateDiagram.Items.AddRange(new DevExpress.XtraDiagram.DiagramItem[] {
            this.diagramConnector1,
            this.diagramContainer1});
            this.diagramDataBindingController1.TemplateDiagram.Location = new System.Drawing.Point(0, 0);
            this.diagramDataBindingController1.TemplateDiagram.Name = "";
            this.diagramDataBindingController1.TemplateDiagram.OptionsBehavior.SelectedStencils = new DevExpress.Diagram.Core.StencilCollection(new string[] {
            "TemplateDesigner",
            "BasicShapes"});
            this.diagramDataBindingController1.TemplateDiagram.OptionsView.CanvasSizeMode = DevExpress.Diagram.Core.CanvasSizeMode.Fill;
            this.diagramDataBindingController1.TemplateDiagram.OptionsView.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.diagramDataBindingController1.TemplateDiagram.OptionsView.ShowPageBreaks = false;
            this.diagramDataBindingController1.TemplateDiagram.TabIndex = 0;
            this.diagramDataBindingController1.DiagramConnectorAdding += new System.EventHandler<DevExpress.XtraDiagram.DiagramDiagramConnectorAddingEventArgs>(this.diagramDataBindingController1_DiagramConnectorAdding);
            this.diagramDataBindingController1.GenerateItem += new System.EventHandler<DevExpress.XtraDiagram.DiagramGenerateItemEventArgs>(this.diagramDataBindingController1_GenerateItem);
            this.diagramDataBindingController1.GenerateConnector += new System.EventHandler<DevExpress.XtraDiagram.DiagramGenerateConnectorEventArgs>(this.diagramDataBindingController1_GenerateConnector);
            this.diagramDataBindingController1.UpdateConnector += new System.EventHandler<DevExpress.XtraDiagram.DiagramUpdateConnectorEventArgs>(this.diagramDataBindingController1_UpdateConnector);
            this.diagramDataBindingController1.CustomLayoutItems += new System.EventHandler<DevExpress.XtraDiagram.DiagramCustomLayoutItemsEventArgs>(this.diagramDataBindingController1_CustomLayoutItems);
            // 
            // diagramControl1
            // 
            this.diagramControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.diagramControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramControl1.HtmlTemplates.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] {
            this.Default,
            this.InRow,
            this.OutRow});
            this.diagramControl1.Location = new System.Drawing.Point(3, 91);
            this.diagramControl1.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.diagramControl1.Name = "diagramControl1";
            this.diagramControl1.OptionsBehavior.ScrollMode = DevExpress.Diagram.Core.DiagramScrollMode.Content;
            this.diagramControl1.OptionsBehavior.SelectedStencils = new DevExpress.Diagram.Core.StencilCollection(new string[] {
            "BasicShapes",
            "BasicFlowchartShapes"});
            this.diagramControl1.OptionsBehavior.SelectPartiallyCoveredItems = true;
            this.diagramControl1.OptionsConnector.LineJumpPlacement = DevExpress.Diagram.Core.LineJumpPlacement.None;
            this.diagramControl1.OptionsView.CanvasSizeMode = DevExpress.Diagram.Core.CanvasSizeMode.Fill;
            this.diagramControl1.OptionsView.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.diagramControl1.OptionsView.ShowGrid = false;
            this.diagramControl1.OptionsView.ShowPageBreaks = false;
            this.diagramControl1.OptionsView.ShowRulers = false;
            this.diagramControl1.OptionsView.ToolboxVisibility = DevExpress.Diagram.Core.ToolboxVisibility.Closed;
            this.diagramControl1.Size = new System.Drawing.Size(1540, 761);
            this.diagramControl1.TabIndex = 0;
            this.diagramControl1.Text = "diagramControl1";
            this.diagramControl1.SelectionChanged += new System.EventHandler<DevExpress.XtraDiagram.DiagramSelectionChangedEventArgs>(this.diagramControl1_SelectionChanged);
            this.diagramControl1.ItemsDeleting += new System.EventHandler<DevExpress.XtraDiagram.DiagramItemsDeletingEventArgs>(this.diagramControl1_ItemsDeleting);
            this.diagramControl1.ItemsResizing += new System.EventHandler<DevExpress.XtraDiagram.DiagramItemsResizingEventArgs>(this.diagramControl1_ItemsResizing);
            this.diagramControl1.QueryConnectionPoints += new System.EventHandler<DevExpress.XtraDiagram.DiagramQueryConnectionPointsEventArgs>(this.diagramControl1_QueryConnectionPoints);
            this.diagramControl1.CustomDrawBackground += new System.EventHandler<DevExpress.XtraDiagram.CustomDrawBackgroundEventArgs>(this.diagramControl1_CustomDrawBackground);
            this.diagramControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.diagramControl1_DragDrop);
            this.diagramControl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.diagramControl1_DragEnter);
            this.diagramControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.diagramControl1_DragOver);
            // 
            // Default
            // 
            this.Default.Name = "Default";
            this.Default.Styles = resources.GetString("Default.Styles");
            this.Default.Template = resources.GetString("Default.Template");
            // 
            // InRow
            // 
            this.InRow.Name = "InRow";
            this.InRow.Styles = resources.GetString("InRow.Styles");
            this.InRow.Template = "<div class=\"in-item\"><div class=\"in-point\" id=\"{0}\"></div>{0}</div>";
            // 
            // OutRow
            // 
            this.OutRow.Name = "OutRow";
            this.OutRow.Styles = resources.GetString("OutRow.Styles");
            this.OutRow.Template = "<div class=\"out-item\">{0}<div class=\"out-point\" id=\"{0}\"></div></div>";
            // 
            // diagramConnector1
            // 
            this.diagramConnector1.Appearance.ContentBackground = System.Drawing.Color.White;
            this.diagramConnector1.BeginPoint = new DevExpress.Utils.PointFloat(-20F, 190F);
            this.diagramConnector1.CanChangeRoute = false;
            this.diagramConnector1.CanDragBeginPoint = false;
            this.diagramConnector1.CanDragEndPoint = false;
            this.diagramConnector1.EndPoint = new DevExpress.Utils.PointFloat(70F, 280F);
            this.diagramConnector1.Points = new DevExpress.XtraDiagram.PointCollection(new DevExpress.Utils.PointFloat[] {
            new DevExpress.Utils.PointFloat(70F, 190F)});
            this.diagramConnector1.TemplateName = "ConnectorTemplate";
            this.diagramConnector1.ThemeStyleId = DevExpress.Diagram.Core.DiagramShapeStyleId.Balanced5;
            this.diagramConnector1.Type = DevExpress.Diagram.Core.ConnectorType.Curved;
            // 
            // diagramContainer1
            // 
            this.diagramContainer1.Anchors = ((DevExpress.Diagram.Core.Sides)((DevExpress.Diagram.Core.Sides.Left | DevExpress.Diagram.Core.Sides.Top)));
            this.diagramContainer1.CanAddItems = false;
            this.diagramContainer1.CanCopyWithoutParent = true;
            this.diagramContainer1.ConnectionPoints = new DevExpress.XtraDiagram.PointCollection(new DevExpress.Utils.PointFloat[] {
            new DevExpress.Utils.PointFloat(0.5F, 0F),
            new DevExpress.Utils.PointFloat(1F, 0.5F),
            new DevExpress.Utils.PointFloat(0.5F, 1F),
            new DevExpress.Utils.PointFloat(0F, 0.5F)});
            this.diagramContainer1.DragMode = DevExpress.Diagram.Core.ContainerDragMode.ByAnyPoint;
            this.diagramContainer1.ItemsCanAttachConnectorBeginPoint = false;
            this.diagramContainer1.ItemsCanAttachConnectorEndPoint = false;
            this.diagramContainer1.ItemsCanChangeParent = false;
            this.diagramContainer1.ItemsCanCopyWithoutParent = false;
            this.diagramContainer1.ItemsCanDeleteWithoutParent = false;
            this.diagramContainer1.ItemsCanEdit = false;
            this.diagramContainer1.ItemsCanMove = false;
            this.diagramContainer1.ItemsCanResize = false;
            this.diagramContainer1.ItemsCanRotate = false;
            this.diagramContainer1.ItemsCanSelect = false;
            this.diagramContainer1.ItemsCanSnapToOtherItems = false;
            this.diagramContainer1.ItemsCanSnapToThisItem = false;
            this.diagramContainer1.MoveWithSubordinates = true;
            this.diagramContainer1.Position = new DevExpress.Utils.PointFloat(250F, 130F);
            this.diagramContainer1.Size = new System.Drawing.SizeF(200F, 200F);
            this.diagramContainer1.StrokeId = DevExpress.Diagram.Core.DiagramThemeColorId.White;
            this.diagramContainer1.TemplateName = "NodeTemplate";
            this.diagramContainer1.ThemeStyleId = DevExpress.Diagram.Core.DiagramShapeStyleId.Variant2;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.CommandLayout = DevExpress.XtraBars.Ribbon.CommandLayout.Simplified;
            this.ribbonControl1.Controller = this.barAndDockingController1;
            this.ribbonControl1.EmptyAreaImageOptions.ImagePadding = new System.Windows.Forms.Padding(60, 58, 60, 58);
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.bbiOpen,
            this.bbiNew,
            this.bbiSave,
            this.bbiSaveAs,
            this.barDockingMenuItem1});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonControl1.MaxItemId = 2;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.OptionsMenuMinWidth = 660;
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(2450, 76);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barDockingMenuItem1
            // 
            this.barDockingMenuItem1.Caption = "Toolbar Windows";
            this.barDockingMenuItem1.Id = 1;
            this.barDockingMenuItem1.Name = "barDockingMenuItem1";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup3});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ConvertedFromBarManager";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiSave);
            this.ribbonPageGroup1.ItemLinks.Add(this.bbiSaveAs);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Custom 2";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.barDockingMenuItem1);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "ribbonPageGroup3";
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.diagramControl1);
            this.groupControl2.Controls.Add(this.standaloneBarDockControl2);
            this.groupControl2.Controls.Add(this.standaloneBarDockControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(470, 76);
            this.groupControl2.Margin = new System.Windows.Forms.Padding(6);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1546, 899);
            this.groupControl2.TabIndex = 11;
            this.groupControl2.Text = "Diagram";
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesTextDescription;
            this.applicationMenu1.MinWidth = 350;
            this.applicationMenu1.Name = "applicationMenu1";
            // 
            // diagramBarController1
            // 
            this.diagramBarController1.Control = this.diagramControl1;
            // 
            // PrintMenuPopupMenu
            // 
            this.PrintMenuPopupMenu.Name = "PrintMenuPopupMenu";
            // 
            // ExportAsPopupMenu
            // 
            this.ExportAsPopupMenu.Name = "ExportAsPopupMenu";
            // 
            // ImageToolsBringToFrontContainerPopupMenu
            // 
            this.ImageToolsBringToFrontContainerPopupMenu.Name = "ImageToolsBringToFrontContainerPopupMenu";
            // 
            // ImageToolsSendToBackContainerPopupMenu
            // 
            this.ImageToolsSendToBackContainerPopupMenu.Name = "ImageToolsSendToBackContainerPopupMenu";
            // 
            // ToolsContainerPopupMenu
            // 
            this.ToolsContainerPopupMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.ToolsContainerPopupMenu.Name = "ToolsContainerPopupMenu";
            // 
            // BringToFrontPopupMenu
            // 
            this.BringToFrontPopupMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.BringToFrontPopupMenu.Name = "BringToFrontPopupMenu";
            // 
            // SendToBackPopupMenu
            // 
            this.SendToBackPopupMenu.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.SendToBackPopupMenu.Name = "SendToBackPopupMenu";
            // 
            // WfDiagramControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.sidePanel2);
            this.Controls.Add(this.sidePanel1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WfDiagramControl";
            this.Size = new System.Drawing.Size(2450, 975);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.sidePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfNodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wevToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            this.sidePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramDataBindingController1.TemplateDiagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramDataBindingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintMenuPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExportAsPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsBringToFrontContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsSendToBackContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BringToFrontPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SendToBackPopupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.BarButtonItem bbiOpen;
        private DevExpress.XtraBars.BarButtonItem bbiNew;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiSaveAs;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SidePanel sidePanel2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraBars.BarButtonItem bbiFitToContent;
        private DevExpress.XtraBars.BarButtonItem bbiZoomItem;
        private DevExpress.XtraBars.BarCheckItem bciAnimateFlow;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private CustomPropertyGrid propertyGridControl1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarEditItem beFontSize;
        private DevExpress.XtraCharts.Sankey.SankeyDiagramControl sankeyDiagramControl1;
        private DevExpress.XtraDiagram.DiagramDataBindingController diagramDataBindingController1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView wevToolbar;
        private System.Windows.Forms.BindingSource wfNodeBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private CustomDiagramControl diagramControl1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraDiagram.DiagramConnector diagramConnector1;
        private DevExpress.XtraDiagram.DiagramContainer diagramContainer1;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colCategory;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.Utils.Html.HtmlTemplate Default;
        private DevExpress.XtraBars.BarButtonItem bbiZoom100;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraDiagram.Bars.DiagramBarController diagramBarController1;
        private DevExpress.XtraBars.PopupMenu PrintMenuPopupMenu;
        private DevExpress.XtraBars.PopupMenu ExportAsPopupMenu;
        private DevExpress.XtraBars.PopupMenu ImageToolsBringToFrontContainerPopupMenu;
        private DevExpress.XtraBars.PopupMenu ImageToolsSendToBackContainerPopupMenu;
        private DevExpress.XtraBars.PopupMenu ToolsContainerPopupMenu;
        private DevExpress.XtraBars.PopupMenu BringToFrontPopupMenu;
        private DevExpress.XtraBars.PopupMenu SendToBackPopupMenu;
        private DevExpress.Utils.Html.HtmlTemplate InRow;
        private DevExpress.Utils.Html.HtmlTemplate OutRow;
        private DevExpress.Utils.SvgImageCollection svgImageCollection1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl2;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.BarCheckItem bciSelectionMode;
        private DevExpress.XtraBars.BarCheckItem bciConnectionMode;
        private DevExpress.XtraBars.BarCheckItem bciShowConnectorText;
        private DevExpress.XtraBars.BarEditItem beConnectorViewType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}
