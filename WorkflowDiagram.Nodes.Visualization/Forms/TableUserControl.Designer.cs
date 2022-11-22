
namespace WokflowDiagram.Nodes.Visualization.Forms {
    partial class TableUserControl {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableUserControl));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.biCsvExport = new DevExpress.XtraBars.BarButtonItem();
            this.biDocxExport = new DevExpress.XtraBars.BarButtonItem();
            this.biHtmlExport = new DevExpress.XtraBars.BarButtonItem();
            this.biMhtExport = new DevExpress.XtraBars.BarButtonItem();
            this.biPdfExport = new DevExpress.XtraBars.BarButtonItem();
            this.biXlsExport = new DevExpress.XtraBars.BarButtonItem();
            this.biXlsxExport = new DevExpress.XtraBars.BarButtonItem();
            this.biSaveCustomization = new DevExpress.XtraBars.BarButtonItem();
            this.biShowProperties = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.biSelectedColumnSettings = new DevExpress.XtraBars.BarButtonItem();
            this.biTableSettings = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1828, 1061);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridControl1_MouseDown);
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAppearance = true;
            this.gridView1.OptionsLayout.StoreFormatRules = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView1_PopupMenuShowing);
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PropertiesRibbon.DefaultSimplifiedRibbonGlyphSize = 24;
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "Export To...";
            this.barSubItem1.Id = 3;
            this.barSubItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barSubItem1.ImageOptions.SvgImage")));
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biCsvExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.biDocxExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.biHtmlExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.biMhtExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.biPdfExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.biXlsExport),
            new DevExpress.XtraBars.LinkPersistInfo(this.biXlsxExport)});
            this.barSubItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesText;
            this.barSubItem1.Name = "barSubItem1";
            // 
            // biCsvExport
            // 
            this.biCsvExport.Caption = "CSV";
            this.biCsvExport.Id = 4;
            this.biCsvExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biCsvExport.ImageOptions.SvgImage")));
            this.biCsvExport.Name = "biCsvExport";
            this.biCsvExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biCsvExport_ItemClick);
            // 
            // biDocxExport
            // 
            this.biDocxExport.Caption = "DOCX";
            this.biDocxExport.Id = 5;
            this.biDocxExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biDocxExport.ImageOptions.SvgImage")));
            this.biDocxExport.Name = "biDocxExport";
            this.biDocxExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biDocxExport_ItemClick);
            // 
            // biHtmlExport
            // 
            this.biHtmlExport.Caption = "HTML";
            this.biHtmlExport.Id = 6;
            this.biHtmlExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biHtmlExport.ImageOptions.SvgImage")));
            this.biHtmlExport.Name = "biHtmlExport";
            this.biHtmlExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biHtmlExport_ItemClick);
            // 
            // biMhtExport
            // 
            this.biMhtExport.Caption = "MHT";
            this.biMhtExport.Id = 7;
            this.biMhtExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biMhtExport.ImageOptions.SvgImage")));
            this.biMhtExport.Name = "biMhtExport";
            this.biMhtExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biMhtExport_ItemClick);
            // 
            // biPdfExport
            // 
            this.biPdfExport.Caption = "PDF";
            this.biPdfExport.Id = 8;
            this.biPdfExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biPdfExport.ImageOptions.SvgImage")));
            this.biPdfExport.Name = "biPdfExport";
            this.biPdfExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biPdfExport_ItemClick);
            // 
            // biXlsExport
            // 
            this.biXlsExport.Caption = "XLS";
            this.biXlsExport.Id = 9;
            this.biXlsExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biXlsExport.ImageOptions.SvgImage")));
            this.biXlsExport.Name = "biXlsExport";
            this.biXlsExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biXlsExport_ItemClick);
            // 
            // biXlsxExport
            // 
            this.biXlsxExport.Caption = "XLSX";
            this.biXlsxExport.Id = 10;
            this.biXlsxExport.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biXlsxExport.ImageOptions.SvgImage")));
            this.biXlsxExport.Name = "biXlsxExport";
            this.biXlsxExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biXlsxExport_ItemClick);
            // 
            // biSaveCustomization
            // 
            this.biSaveCustomization.Caption = "Save Customization";
            this.biSaveCustomization.Id = 12;
            this.biSaveCustomization.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSaveCustomization.ImageOptions.SvgImage")));
            this.biSaveCustomization.Name = "biSaveCustomization";
            this.biSaveCustomization.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biSaveCustomization.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSaveCustomization_ItemClick);
            // 
            // biShowProperties
            // 
            this.biShowProperties.Caption = "Customize";
            this.biShowProperties.Id = 13;
            this.biShowProperties.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biShowProperties.ImageOptions.SvgImage")));
            this.biShowProperties.Name = "biShowProperties";
            this.biShowProperties.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biShowProperties.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.biShowProperties_CheckedChanged);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Customize View";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // biSelectedColumnSettings
            // 
            this.biSelectedColumnSettings.Caption = "Column Settings";
            this.biSelectedColumnSettings.Id = 14;
            this.biSelectedColumnSettings.Name = "biSelectedColumnSettings";
            this.biSelectedColumnSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSelectedColumnSettings_ItemClick);
            // 
            // biTableSettings
            // 
            this.biTableSettings.Caption = "Table Settings";
            this.biTableSettings.Id = 15;
            this.biTableSettings.Name = "biTableSettings";
            this.biTableSettings.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biTableSettings_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Reset Customization";
            this.barButtonItem2.Id = 16;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 50);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.propertyGridControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            this.splitContainerControl1.Size = new System.Drawing.Size(1828, 1061);
            this.splitContainerControl1.SplitterPosition = 615;
            this.splitContainerControl1.TabIndex = 3;
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.ActiveViewType = DevExpress.XtraVerticalGrid.PropertyGridView.Office;
            this.propertyGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControl1.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.propertyGridControl1.Size = new System.Drawing.Size(0, 0);
            this.propertyGridControl1.TabIndex = 1;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.Controller = this.barAndDockingController1;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.biCsvExport,
            this.biDocxExport,
            this.biHtmlExport,
            this.biMhtExport,
            this.biPdfExport,
            this.biXlsExport,
            this.biXlsxExport,
            this.barButtonItem1,
            this.biSaveCustomization,
            this.biShowProperties,
            this.biSelectedColumnSettings,
            this.biTableSettings,
            this.barButtonItem2});
            // 
            // bar1
            // 
            this.bar1.BarItemHorzIndent = 4;
            this.bar1.BarItemVertIndent = 4;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.biShowProperties),
            new DevExpress.XtraBars.LinkPersistInfo(this.biSaveCustomization)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1828, 50);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1111);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1828, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 50);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1061);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1828, 50);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1061);
            // 
            // TableUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "TableUserControl";
            this.Size = new System.Drawing.Size(1828, 1111);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem biCsvExport;
        private DevExpress.XtraBars.BarButtonItem biDocxExport;
        private DevExpress.XtraBars.BarButtonItem biHtmlExport;
        private DevExpress.XtraBars.BarButtonItem biMhtExport;
        private DevExpress.XtraBars.BarButtonItem biPdfExport;
        private DevExpress.XtraBars.BarButtonItem biXlsExport;
        private DevExpress.XtraBars.BarButtonItem biXlsxExport;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraBars.BarButtonItem biSaveCustomization;
        private DevExpress.XtraBars.BarCheckItem biShowProperties;
        private DevExpress.XtraBars.BarButtonItem biSelectedColumnSettings;
        private DevExpress.XtraBars.BarButtonItem biTableSettings;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}