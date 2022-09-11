
using DevExpress.XtraVerticalGrid.Events;
using System;
using System.ComponentModel;

namespace WorkflowDiagram.UI.Win {
    partial class WfDocumentControl {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WfDocumentControl));
            DevExpress.Utils.Animation.PushTransition pushTransition2 = new DevExpress.Utils.Animation.PushTransition();
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
            this.bbiZoom100 = new DevExpress.XtraBars.BarButtonItem();
            this.bciShowConnectorText = new DevExpress.XtraBars.BarCheckItem();
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
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.panelContainer4 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpProperties = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.pgcProperties = new WorkflowDiagram.UI.Win.CustomPropertyGrid();
            this.dpValue = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer5 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.cpgValue = new WorkflowDiagram.UI.Win.CustomPropertyGrid();
            this.panelContainer2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpOutputs = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.connectionsEditor2 = new WorkflowDiagram.UI.Win.ConnectionsEditor();
            this.dpInputs = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer4 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.connectionsEditor1 = new WorkflowDiagram.UI.Win.ConnectionsEditor();
            this.dpToolbox = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.wfNodeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.wevToolbar = new DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.svgImageCollection1 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.panelContainer3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dpDiagram = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer3 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.diagramControl1 = new WorkflowDiagram.UI.Win.CustomDiagramControl();
            this.Default = new DevExpress.Utils.Html.HtmlTemplate();
            this.InRow = new DevExpress.Utils.Html.HtmlTemplate();
            this.OutRow = new DevExpress.Utils.Html.HtmlTemplate();
            this.dpDiagnostics = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gcDiagnostics = new DevExpress.XtraGrid.GridControl();
            this.wfDiagnosticInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvDiagnostics = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colType1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.diagramDataBindingController1 = new DevExpress.XtraDiagram.DiagramDataBindingController(this.components);
            this.diagramConnector1 = new DevExpress.XtraDiagram.DiagramConnector();
            this.diagramContainer1 = new DevExpress.XtraDiagram.DiagramContainer();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barDockingMenuItem1 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.barDockingMenuItem2 = new DevExpress.XtraBars.BarDockingMenuItem();
            this.barWorkspaceMenuItem1 = new DevExpress.XtraBars.BarWorkspaceMenuItem();
            this.workspaceManager1 = new DevExpress.Utils.WorkspaceManager(this.components);
            this.biOwnerProperties = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.diagramBarController1 = new DevExpress.XtraDiagram.Bars.DiagramBarController(this.components);
            this.PrintMenuPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ExportAsPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ImageToolsBringToFrontContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ImageToolsSendToBackContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ToolsContainerPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.BringToFrontPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.SendToBackPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.alertControl1 = new DevExpress.XtraBars.Alerter.AlertControl(this.components);
            this.svgImageCollection2 = new DevExpress.Utils.SvgImageCollection(this.components);
            this.customPropertyGrid1 = new WorkflowDiagram.UI.Win.CustomPropertyGrid();
            this.pmContextMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            this.panelContainer4.SuspendLayout();
            this.dpProperties.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pgcProperties)).BeginInit();
            this.dpValue.SuspendLayout();
            this.controlContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpgValue)).BeginInit();
            this.panelContainer2.SuspendLayout();
            this.dpOutputs.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.dpInputs.SuspendLayout();
            this.controlContainer4.SuspendLayout();
            this.dpToolbox.SuspendLayout();
            this.controlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfNodeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wevToolbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).BeginInit();
            this.panelContainer3.SuspendLayout();
            this.dpDiagram.SuspendLayout();
            this.controlContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl1)).BeginInit();
            this.dpDiagnostics.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDiagnostics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfDiagnosticInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDiagnostics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramDataBindingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramDataBindingController1.TemplateDiagram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintMenuPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExportAsPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsBringToFrontContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsSendToBackContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsContainerPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BringToFrontPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SendToBackPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customPropertyGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmContextMenu)).BeginInit();
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
            this.barAndDockingController1.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(24, 24);
            this.barAndDockingController1.PropertiesRibbon.DefaultSimplifiedRibbonGlyphSize = 24;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer1,
            this.dpToolbox,
            this.panelContainer3});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiZoom100),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciShowConnectorText),
            new DevExpress.XtraBars.LinkPersistInfo(this.bciAnimateFlow),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.StandaloneBarDockControl = this.standaloneBarDockControl1;
            this.bar2.Text = "Custom 2";
            // 
            // bbiZoom100
            // 
            this.bbiZoom100.Caption = "100%";
            this.bbiZoom100.Id = 0;
            this.bbiZoom100.Name = "bbiZoom100";
            this.bbiZoom100.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiZoom100_ItemClick);
            // 
            // bciShowConnectorText
            // 
            this.bciShowConnectorText.Caption = "Show Connectors Order Indices";
            this.bciShowConnectorText.Id = 4;
            this.bciShowConnectorText.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bciShowConnectorText.ImageOptions.SvgImage")));
            this.bciShowConnectorText.Name = "bciShowConnectorText";
            this.bciShowConnectorText.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bciShowConnectorText_CheckedChanged);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 432);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Margin = new System.Windows.Forms.Padding(6);
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1521, 62);
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
            this.bciConnectionMode.Caption = "Connect Nodes";
            this.bciConnectionMode.GroupIndex = 33;
            this.bciConnectionMode.Id = 3;
            this.bciConnectionMode.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bciConnectionMode.ImageOptions.SvgImage")));
            this.bciConnectionMode.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C));
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
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl2.Manager = this.barManager1;
            this.standaloneBarDockControl2.Margin = new System.Windows.Forms.Padding(6);
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(1521, 62);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlTop.Size = new System.Drawing.Size(2454, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 975);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlBottom.Size = new System.Drawing.Size(2454, 0);
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
            this.barDockControlRight.Location = new System.Drawing.Point(2454, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Margin = new System.Windows.Forms.Padding(6);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 975);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.panelContainer4);
            this.panelContainer1.Controls.Add(this.panelContainer2);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.ID = new System.Guid("ff37c224-8fb9-4ce9-b433-77c72f7a5415");
            this.panelContainer1.Location = new System.Drawing.Point(1958, 76);
            this.panelContainer1.Margin = new System.Windows.Forms.Padding(6);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(496, 200);
            this.panelContainer1.Size = new System.Drawing.Size(496, 899);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // panelContainer4
            // 
            this.panelContainer4.ActiveChild = this.dpProperties;
            this.panelContainer4.Controls.Add(this.dpProperties);
            this.panelContainer4.Controls.Add(this.dpValue);
            this.panelContainer4.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.panelContainer4.ID = new System.Guid("fa335202-d893-4c28-8dbd-8c527ff95c91");
            this.panelContainer4.Location = new System.Drawing.Point(0, 0);
            this.panelContainer4.Name = "panelContainer4";
            this.panelContainer4.OriginalSize = new System.Drawing.Size(350, 450);
            this.panelContainer4.Size = new System.Drawing.Size(496, 450);
            this.panelContainer4.Tabbed = true;
            this.panelContainer4.Text = "panelContainer4";
            // 
            // dpProperties
            // 
            this.dpProperties.Controls.Add(this.controlContainer1);
            this.dpProperties.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpProperties.ID = new System.Guid("2e7a5007-edf4-47b5-a997-3b43f78d87bc");
            this.dpProperties.Location = new System.Drawing.Point(11, 52);
            this.dpProperties.Margin = new System.Windows.Forms.Padding(4);
            this.dpProperties.Name = "dpProperties";
            this.dpProperties.OriginalSize = new System.Drawing.Size(350, 450);
            this.dpProperties.Size = new System.Drawing.Size(479, 342);
            this.dpProperties.Text = "Properties";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.pgcProperties);
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(479, 342);
            this.controlContainer1.TabIndex = 0;
            // 
            // pgcProperties
            // 
            this.pgcProperties.ActiveViewType = DevExpress.XtraVerticalGrid.PropertyGridView.Office;
            this.pgcProperties.BandsInterval = 4;
            this.pgcProperties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pgcProperties.Cursor = System.Windows.Forms.Cursors.Default;
            this.pgcProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgcProperties.Location = new System.Drawing.Point(0, 0);
            this.pgcProperties.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pgcProperties.Name = "pgcProperties";
            this.pgcProperties.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.pgcProperties.OptionsView.FixedLineWidth = 4;
            this.pgcProperties.OptionsView.LevelIndent = 8;
            this.pgcProperties.OptionsView.MinRowAutoHeight = 40;
            this.pgcProperties.OptionsView.ShowRootLevelIndent = false;
            this.pgcProperties.Size = new System.Drawing.Size(479, 342);
            this.pgcProperties.TabIndex = 0;
            this.pgcProperties.CustomRecordCellEdit += new DevExpress.XtraVerticalGrid.Events.GetCustomRowCellEditEventHandler(this.propertyGridControl1_CustomRecordCellEdit);
            this.pgcProperties.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.propertyGridControl1_ShowingEditor);
            this.pgcProperties.CellValueChanged += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.propertyGridControl1_CellValueChanged);
            this.pgcProperties.SelectedChanged += new DevExpress.XtraVerticalGrid.Events.SelectedChangedHandler(this.propertyGridControl1_SelectedChanged);
            this.pgcProperties.SizeChanged += new System.EventHandler(this.propertyGridControl1_SizeChanged);
            this.pgcProperties.Resize += new System.EventHandler(this.propertyGridControl1_Resize);
            // 
            // dpValue
            // 
            this.dpValue.Controls.Add(this.controlContainer5);
            this.dpValue.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpValue.FloatSize = new System.Drawing.Size(726, 668);
            this.dpValue.ID = new System.Guid("0d72315f-14fc-4691-8b7e-55d3b869866f");
            this.dpValue.Location = new System.Drawing.Point(11, 52);
            this.dpValue.Name = "dpValue";
            this.dpValue.OriginalSize = new System.Drawing.Size(514, 437);
            this.dpValue.Size = new System.Drawing.Size(479, 342);
            this.dpValue.Text = "Connection Point Value";
            // 
            // controlContainer5
            // 
            this.controlContainer5.Controls.Add(this.cpgValue);
            this.controlContainer5.Location = new System.Drawing.Point(0, 0);
            this.controlContainer5.Name = "controlContainer5";
            this.controlContainer5.Size = new System.Drawing.Size(479, 342);
            this.controlContainer5.TabIndex = 0;
            // 
            // cpgValue
            // 
            this.cpgValue.ActiveViewType = DevExpress.XtraVerticalGrid.PropertyGridView.Office;
            this.cpgValue.BandsInterval = 4;
            this.cpgValue.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.cpgValue.Cursor = System.Windows.Forms.Cursors.Default;
            this.cpgValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpgValue.Location = new System.Drawing.Point(0, 0);
            this.cpgValue.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.cpgValue.Name = "cpgValue";
            this.cpgValue.OptionsBehavior.Editable = false;
            this.cpgValue.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.cpgValue.OptionsView.FixedLineWidth = 4;
            this.cpgValue.OptionsView.LevelIndent = 8;
            this.cpgValue.OptionsView.MinRowAutoHeight = 40;
            this.cpgValue.OptionsView.ShowRootLevelIndent = false;
            this.cpgValue.Size = new System.Drawing.Size(479, 342);
            this.cpgValue.TabIndex = 1;
            // 
            // panelContainer2
            // 
            this.panelContainer2.ActiveChild = this.dpOutputs;
            this.panelContainer2.Controls.Add(this.dpInputs);
            this.panelContainer2.Controls.Add(this.dpOutputs);
            this.panelContainer2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.panelContainer2.ID = new System.Guid("64d54bfe-1bcb-4f50-b097-02e220e7c271");
            this.panelContainer2.Location = new System.Drawing.Point(0, 450);
            this.panelContainer2.Margin = new System.Windows.Forms.Padding(6);
            this.panelContainer2.Name = "panelContainer2";
            this.panelContainer2.OriginalSize = new System.Drawing.Size(350, 449);
            this.panelContainer2.Size = new System.Drawing.Size(496, 449);
            this.panelContainer2.Tabbed = true;
            this.panelContainer2.Text = "panelContainer2";
            // 
            // dpOutputs
            // 
            this.dpOutputs.Controls.Add(this.dockPanel2_Container);
            this.dpOutputs.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpOutputs.ID = new System.Guid("eb5b1cfe-6fc8-4248-aac2-b7b1d5f2170f");
            this.dpOutputs.Location = new System.Drawing.Point(11, 52);
            this.dpOutputs.Margin = new System.Windows.Forms.Padding(6);
            this.dpOutputs.Name = "dpOutputs";
            this.dpOutputs.OriginalSize = new System.Drawing.Size(333, 346);
            this.dpOutputs.Size = new System.Drawing.Size(479, 346);
            this.dpOutputs.Text = "Outputs";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.connectionsEditor2);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Margin = new System.Windows.Forms.Padding(6);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(479, 346);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // connectionsEditor2
            // 
            this.connectionsEditor2.Connections = null;
            this.connectionsEditor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionsEditor2.Location = new System.Drawing.Point(0, 0);
            this.connectionsEditor2.Margin = new System.Windows.Forms.Padding(2);
            this.connectionsEditor2.Name = "connectionsEditor2";
            this.connectionsEditor2.Size = new System.Drawing.Size(479, 346);
            this.connectionsEditor2.TabIndex = 0;
            // 
            // dpInputs
            // 
            this.dpInputs.Controls.Add(this.controlContainer4);
            this.dpInputs.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpInputs.ID = new System.Guid("1a180504-aab3-44b2-b538-85b03b34b9ea");
            this.dpInputs.Location = new System.Drawing.Point(11, 52);
            this.dpInputs.Margin = new System.Windows.Forms.Padding(6);
            this.dpInputs.Name = "dpInputs";
            this.dpInputs.OriginalSize = new System.Drawing.Size(333, 346);
            this.dpInputs.Size = new System.Drawing.Size(479, 346);
            this.dpInputs.Text = "Inputs";
            // 
            // controlContainer4
            // 
            this.controlContainer4.Controls.Add(this.connectionsEditor1);
            this.controlContainer4.Location = new System.Drawing.Point(0, 0);
            this.controlContainer4.Margin = new System.Windows.Forms.Padding(6);
            this.controlContainer4.Name = "controlContainer4";
            this.controlContainer4.Size = new System.Drawing.Size(479, 346);
            this.controlContainer4.TabIndex = 0;
            // 
            // connectionsEditor1
            // 
            this.connectionsEditor1.Connections = null;
            this.connectionsEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionsEditor1.Location = new System.Drawing.Point(0, 0);
            this.connectionsEditor1.Margin = new System.Windows.Forms.Padding(2);
            this.connectionsEditor1.Name = "connectionsEditor1";
            this.connectionsEditor1.Size = new System.Drawing.Size(479, 346);
            this.connectionsEditor1.TabIndex = 0;
            // 
            // dpToolbox
            // 
            this.dpToolbox.Controls.Add(this.controlContainer2);
            this.dpToolbox.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dpToolbox.ID = new System.Guid("97e93803-aed1-4d92-9239-d7651ce21362");
            this.dpToolbox.Location = new System.Drawing.Point(0, 76);
            this.dpToolbox.Margin = new System.Windows.Forms.Padding(4);
            this.dpToolbox.Name = "dpToolbox";
            this.dpToolbox.OriginalSize = new System.Drawing.Size(425, 200);
            this.dpToolbox.Size = new System.Drawing.Size(425, 899);
            this.dpToolbox.Text = "Toolbox";
            // 
            // controlContainer2
            // 
            this.controlContainer2.Controls.Add(this.gridControl1);
            this.controlContainer2.Location = new System.Drawing.Point(6, 52);
            this.controlContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(408, 841);
            this.controlContainer2.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.wfNodeBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.wevToolbar;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(6);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(408, 841);
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
            this.wevToolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.wevToolbar.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colText,
            this.colType,
            this.colCategory,
            this.colDescription,
            this.colImage});
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
            this.wevToolbar.OptionsViewStyles.Content.ItemWidth = 408;
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
            // colImage
            // 
            this.colImage.Caption = "Image";
            this.colImage.FieldName = "Image";
            this.colImage.MinWidth = 40;
            this.colImage.Name = "colImage";
            this.colImage.Visible = true;
            this.colImage.VisibleIndex = 5;
            this.colImage.Width = 150;
            // 
            // svgImageCollection1
            // 
            this.svgImageCollection1.Add("input", "image://svgimages/spreadsheet/pivottablegroupselection.svg");
            // 
            // panelContainer3
            // 
            this.panelContainer3.ChildPanelOrientation = DevExpress.XtraBars.Docking.LayoutOrientation.Vertical;
            this.panelContainer3.Controls.Add(this.dpDiagram);
            this.panelContainer3.Controls.Add(this.dpDiagnostics);
            this.panelContainer3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.panelContainer3.FloatSize = new System.Drawing.Size(361, 301);
            this.panelContainer3.ID = new System.Guid("745e5740-3b82-4e2d-9596-cdfbc63e1fdc");
            this.panelContainer3.Location = new System.Drawing.Point(425, 76);
            this.panelContainer3.Margin = new System.Windows.Forms.Padding(6);
            this.panelContainer3.Name = "panelContainer3";
            this.panelContainer3.OriginalSize = new System.Drawing.Size(200, 899);
            this.panelContainer3.Size = new System.Drawing.Size(1533, 899);
            this.panelContainer3.Text = "panelContainer3";
            // 
            // dpDiagram
            // 
            this.dpDiagram.Controls.Add(this.controlContainer3);
            this.dpDiagram.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpDiagram.FloatSize = new System.Drawing.Size(361, 301);
            this.dpDiagram.ID = new System.Guid("40f5dcfc-165c-4322-9459-a3ef717b621e");
            this.dpDiagram.Location = new System.Drawing.Point(0, 0);
            this.dpDiagram.Margin = new System.Windows.Forms.Padding(4);
            this.dpDiagram.Name = "dpDiagram";
            this.dpDiagram.OriginalSize = new System.Drawing.Size(1749, 462);
            this.dpDiagram.Size = new System.Drawing.Size(1533, 557);
            this.dpDiagram.Text = "Diagram";
            // 
            // controlContainer3
            // 
            this.controlContainer3.Controls.Add(this.diagramControl1);
            this.controlContainer3.Controls.Add(this.standaloneBarDockControl2);
            this.controlContainer3.Controls.Add(this.standaloneBarDockControl1);
            this.controlContainer3.Location = new System.Drawing.Point(6, 52);
            this.controlContainer3.Margin = new System.Windows.Forms.Padding(4);
            this.controlContainer3.Name = "controlContainer3";
            this.controlContainer3.Size = new System.Drawing.Size(1521, 494);
            this.controlContainer3.TabIndex = 0;
            // 
            // diagramControl1
            // 
            this.diagramControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.diagramControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramControl1.HtmlTemplates.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] {
            this.Default,
            this.InRow,
            this.OutRow});
            this.diagramControl1.Location = new System.Drawing.Point(0, 62);
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
            this.diagramControl1.Size = new System.Drawing.Size(1521, 370);
            this.diagramControl1.TabIndex = 0;
            this.diagramControl1.Text = "diagramControl1";
            this.diagramControl1.SelectionChanged += new System.EventHandler<DevExpress.XtraDiagram.DiagramSelectionChangedEventArgs>(this.diagramControl1_SelectionChanged);
            this.diagramControl1.ItemsDeleting += new System.EventHandler<DevExpress.XtraDiagram.DiagramItemsDeletingEventArgs>(this.diagramControl1_ItemsDeleting);
            this.diagramControl1.ItemsResizing += new System.EventHandler<DevExpress.XtraDiagram.DiagramItemsResizingEventArgs>(this.diagramControl1_ItemsResizing);
            this.diagramControl1.QueryConnectionPoints += new System.EventHandler<DevExpress.XtraDiagram.DiagramQueryConnectionPointsEventArgs>(this.diagramControl1_QueryConnectionPoints);
            this.diagramControl1.CustomDrawItem += new System.EventHandler<DevExpress.XtraDiagram.CustomDrawItemEventArgs>(this.diagramControl1_CustomDrawItem);
            this.diagramControl1.CustomDrawBackground += new System.EventHandler<DevExpress.XtraDiagram.CustomDrawBackgroundEventArgs>(this.diagramControl1_CustomDrawBackground);
            this.diagramControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.diagramControl1_DragDrop);
            this.diagramControl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.diagramControl1_DragEnter);
            this.diagramControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.diagramControl1_DragOver);
            this.diagramControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.diagramControl1_MouseDown);
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
            this.InRow.Template = "<div class=\"in-item\">\r\n\t<div class=\"in-point\" id=\"{0}\"></div>\r\n\t<div class=\"name\"" +
    ">{1}</div>\r\n\t<div class=\"value\">${{{0}}}</div>\r\n</div>";
            // 
            // OutRow
            // 
            this.OutRow.Name = "OutRow";
            this.OutRow.Styles = resources.GetString("OutRow.Styles");
            this.OutRow.Template = "<div class=\"out-item\">\r\n\t<div class=\"value\">${{{0}}}</div>\r\n\t<div class=\"name\">{1" +
    "}</div>\r\n\t<div class=\"out-point\" id=\"{0}\"></div>\r\n</div>";
            // 
            // dpDiagnostics
            // 
            this.dpDiagnostics.Controls.Add(this.dockPanel1_Container);
            this.dpDiagnostics.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpDiagnostics.FloatVertical = true;
            this.dpDiagnostics.ID = new System.Guid("cd6b2811-9b4c-4fd1-9722-d82a4ee5f460");
            this.dpDiagnostics.Location = new System.Drawing.Point(0, 557);
            this.dpDiagnostics.Margin = new System.Windows.Forms.Padding(4);
            this.dpDiagnostics.Name = "dpDiagnostics";
            this.dpDiagnostics.OriginalSize = new System.Drawing.Size(1749, 437);
            this.dpDiagnostics.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dpDiagnostics.SavedIndex = 1;
            this.dpDiagnostics.SavedParent = this.panelContainer3;
            this.dpDiagnostics.SavedSizeFactor = 1.29727D;
            this.dpDiagnostics.Size = new System.Drawing.Size(1533, 342);
            this.dpDiagnostics.Text = "Diagnostics";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.gcDiagnostics);
            this.dockPanel1_Container.Location = new System.Drawing.Point(6, 52);
            this.dockPanel1_Container.Margin = new System.Windows.Forms.Padding(4);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(1521, 284);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // gcDiagnostics
            // 
            this.gcDiagnostics.DataSource = this.wfDiagnosticInfoBindingSource;
            this.gcDiagnostics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDiagnostics.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcDiagnostics.Location = new System.Drawing.Point(0, 0);
            this.gcDiagnostics.MainView = this.gvDiagnostics;
            this.gcDiagnostics.Margin = new System.Windows.Forms.Padding(4);
            this.gcDiagnostics.MenuManager = this.barManager1;
            this.gcDiagnostics.Name = "gcDiagnostics";
            this.gcDiagnostics.Size = new System.Drawing.Size(1521, 284);
            this.gcDiagnostics.TabIndex = 0;
            this.gcDiagnostics.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDiagnostics});
            // 
            // wfDiagnosticInfoBindingSource
            // 
            this.wfDiagnosticInfoBindingSource.DataSource = typeof(WorkflowDiagram.WfDiagnosticInfo);
            // 
            // gvDiagnostics
            // 
            this.gvDiagnostics.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gvDiagnostics.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colType1,
            this.colText1});
            this.gvDiagnostics.GridControl = this.gcDiagnostics;
            this.gvDiagnostics.LevelIndent = 0;
            this.gvDiagnostics.Name = "gvDiagnostics";
            this.gvDiagnostics.OptionsBehavior.Editable = false;
            this.gvDiagnostics.OptionsView.ShowGroupPanel = false;
            this.gvDiagnostics.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvDiagnostics.OptionsView.ShowIndicator = false;
            this.gvDiagnostics.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gvDiagnostics.PreviewIndent = 0;
            this.gvDiagnostics.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvDiagnostics_FocusedRowChanged);
            // 
            // colType1
            // 
            this.colType1.FieldName = "Type";
            this.colType1.MinWidth = 40;
            this.colType1.Name = "colType1";
            this.colType1.Visible = true;
            this.colType1.VisibleIndex = 0;
            this.colType1.Width = 198;
            // 
            // colText1
            // 
            this.colText1.FieldName = "Text";
            this.colText1.MinWidth = 40;
            this.colText1.Name = "colText1";
            this.colText1.Visible = true;
            this.colText1.VisibleIndex = 1;
            this.colText1.Width = 150;
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "*.xml";
            this.xtraSaveFileDialog1.Filter = "Workflow Document File (*.xml)|*.xml|All files (*.*)|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
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
            this.barDockingMenuItem1,
            this.barDockingMenuItem2,
            this.barWorkspaceMenuItem1,
            this.biOwnerProperties});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(6);
            this.ribbonControl1.MaxItemId = 5;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.OptionsMenuMinWidth = 660;
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1});
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;
            this.ribbonControl1.Size = new System.Drawing.Size(2454, 76);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // barDockingMenuItem1
            // 
            this.barDockingMenuItem1.Caption = "Toolbar Windows";
            this.barDockingMenuItem1.Id = 1;
            this.barDockingMenuItem1.Name = "barDockingMenuItem1";
            // 
            // barDockingMenuItem2
            // 
            this.barDockingMenuItem2.Caption = "Windows";
            this.barDockingMenuItem2.Id = 2;
            this.barDockingMenuItem2.Name = "barDockingMenuItem2";
            // 
            // barWorkspaceMenuItem1
            // 
            this.barWorkspaceMenuItem1.Caption = "Workspaces";
            this.barWorkspaceMenuItem1.Id = 3;
            this.barWorkspaceMenuItem1.Name = "barWorkspaceMenuItem1";
            this.barWorkspaceMenuItem1.WorkspaceManager = this.workspaceManager1;
            // 
            // workspaceManager1
            // 
            this.workspaceManager1.TargetControl = this;
            this.workspaceManager1.TransitionType = pushTransition2;
            // 
            // biOwnerProperties
            // 
            this.biOwnerProperties.Caption = "Object Properties";
            this.biOwnerProperties.Id = 4;
            this.biOwnerProperties.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biOwnerProperties.ImageOptions.SvgImage")));
            this.biOwnerProperties.Name = "biOwnerProperties";
            this.biOwnerProperties.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biOwnerProperties_ItemClick);
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
            this.ribbonPageGroup1.ItemLinks.Add(this.biOwnerProperties);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Custom 2";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.barDockingMenuItem2);
            this.ribbonPageGroup3.ItemLinks.Add(this.barWorkspaceMenuItem1);
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
            // alertControl1
            // 
            this.alertControl1.HtmlImages = this.svgImageCollection2;
            // 
            // 
            // 
            this.alertControl1.HtmlTemplate.Styles = resources.GetString("alertControl1.HtmlTemplate.Styles");
            this.alertControl1.HtmlTemplate.Template = resources.GetString("alertControl1.HtmlTemplate.Template");
            this.alertControl1.Images = this.svgImageCollection2;
            // 
            // svgImageCollection2
            // 
            this.svgImageCollection2.Add("success", "image://svgimages/diagramicons/check.svg");
            this.svgImageCollection2.Add("close", "image://svgimages/diagramicons/del.svg");
            this.svgImageCollection2.Add("warning", "image://svgimages/business objects/bo_attention.svg");
            this.svgImageCollection2.Add("info", "image://svgimages/outlook inspired/about.svg");
            this.svgImageCollection2.Add("error", "image://svgimages/outlook inspired/highimportance.svg");
            // 
            // customPropertyGrid1
            // 
            this.customPropertyGrid1.ActiveViewType = DevExpress.XtraVerticalGrid.PropertyGridView.Office;
            this.customPropertyGrid1.BandsInterval = 4;
            this.customPropertyGrid1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.customPropertyGrid1.Cursor = System.Windows.Forms.Cursors.Default;
            this.customPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.customPropertyGrid1.Margin = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.customPropertyGrid1.Name = "customPropertyGrid1";
            this.customPropertyGrid1.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.customPropertyGrid1.OptionsView.FixedLineWidth = 4;
            this.customPropertyGrid1.OptionsView.LevelIndent = 8;
            this.customPropertyGrid1.OptionsView.MinRowAutoHeight = 40;
            this.customPropertyGrid1.OptionsView.ShowRootLevelIndent = false;
            this.customPropertyGrid1.Size = new System.Drawing.Size(333, 387);
            this.customPropertyGrid1.TabIndex = 0;
            // 
            // pmContextMenu
            // 
            this.pmContextMenu.Name = "pmContextMenu";
            this.pmContextMenu.Ribbon = this.ribbonControl1;
            // 
            // WfDocumentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContainer3);
            this.Controls.Add(this.dpToolbox);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WfDocumentControl";
            this.Size = new System.Drawing.Size(2454, 975);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.panelContainer4.ResumeLayout(false);
            this.dpProperties.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pgcProperties)).EndInit();
            this.dpValue.ResumeLayout(false);
            this.controlContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cpgValue)).EndInit();
            this.panelContainer2.ResumeLayout(false);
            this.dpOutputs.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.dpInputs.ResumeLayout(false);
            this.controlContainer4.ResumeLayout(false);
            this.dpToolbox.ResumeLayout(false);
            this.controlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfNodeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wevToolbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection1)).EndInit();
            this.panelContainer3.ResumeLayout(false);
            this.dpDiagram.ResumeLayout(false);
            this.controlContainer3.ResumeLayout(false);
            this.controlContainer3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl1)).EndInit();
            this.dpDiagnostics.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDiagnostics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfDiagnosticInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDiagnostics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramDataBindingController1.TemplateDiagram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramDataBindingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintMenuPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExportAsPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsBringToFrontContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageToolsSendToBackContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToolsContainerPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BringToFrontPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SendToBackPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgImageCollection2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customPropertyGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pmContextMenu)).EndInit();
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
        private DevExpress.XtraBars.BarButtonItem bbiFitToContent;
        private DevExpress.XtraBars.BarButtonItem bbiZoomItem;
        private DevExpress.XtraBars.BarCheckItem bciAnimateFlow;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private CustomPropertyGrid pgcProperties;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarEditItem beFontSize;
        private DevExpress.XtraDiagram.DiagramDataBindingController diagramDataBindingController1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.WinExplorer.WinExplorerView wevToolbar;
        private System.Windows.Forms.BindingSource wfNodeBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
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
        private ConnectionsEditor connectionsEditor1;
        private ConnectionsEditor connectionsEditor2;
        private DevExpress.XtraGrid.GridControl gcDiagnostics;
        private System.Windows.Forms.BindingSource wfDiagnosticInfoBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDiagnostics;
        private DevExpress.XtraGrid.Columns.GridColumn colType1;
        private DevExpress.XtraGrid.Columns.GridColumn colText1;
        private DevExpress.XtraBars.Docking.DockPanel dpDiagnostics;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dpProperties;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraBars.Docking.DockPanel dpToolbox;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private DevExpress.XtraBars.Docking.DockPanel dpDiagram;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer3;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer2;
        private DevExpress.XtraBars.Docking.DockPanel dpInputs;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer4;
        private DevExpress.XtraBars.Docking.DockPanel dpOutputs;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer3;
        private DevExpress.XtraBars.BarDockingMenuItem barDockingMenuItem2;
        private DevExpress.XtraBars.BarWorkspaceMenuItem barWorkspaceMenuItem1;
        private DevExpress.Utils.WorkspaceManager workspaceManager1;
        private DevExpress.XtraGrid.Columns.GridColumn colImage;
        private DevExpress.XtraBars.Alerter.AlertControl alertControl1;
        private DevExpress.Utils.SvgImageCollection svgImageCollection2;
        private DevExpress.XtraBars.BarButtonItem biOwnerProperties;
        private DevExpress.XtraBars.Docking.DockPanel dpValue;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer5;
        private CustomPropertyGrid cpgValue;
        private CustomPropertyGrid customPropertyGrid1;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer4;
        private DevExpress.XtraBars.PopupMenu pmContextMenu;
    }
}
