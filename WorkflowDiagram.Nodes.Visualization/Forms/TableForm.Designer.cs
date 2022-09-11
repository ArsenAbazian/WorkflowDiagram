
namespace WokflowDiagram.Nodes.Visualization.Forms {
    partial class TableForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableForm));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.backstageViewControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewControl();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.backstageViewClientControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.aiExportCsv = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aiExportDocx = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aiExportHtml = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aiExportMht = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aiExportPdf = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aiExportXls = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.aiExportXlsx = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.btiExport = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.biCsvExport = new DevExpress.XtraBars.BarButtonItem();
            this.biDocxExport = new DevExpress.XtraBars.BarButtonItem();
            this.biHtmlExport = new DevExpress.XtraBars.BarButtonItem();
            this.biMhtExport = new DevExpress.XtraBars.BarButtonItem();
            this.biPdfExport = new DevExpress.XtraBars.BarButtonItem();
            this.biXlsExport = new DevExpress.XtraBars.BarButtonItem();
            this.biXlsxExport = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.biSaveCustomization = new DevExpress.XtraBars.BarButtonItem();
            this.biShowProperties = new DevExpress.XtraBars.BarCheckItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.xtraSaveFileDialog1 = new DevExpress.XtraEditors.XtraSaveFileDialog(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backstageViewControl1)).BeginInit();
            this.backstageViewControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            this.backstageViewClientControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1828, 921);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsLayout.StoreAllOptions = true;
            this.gridView1.OptionsLayout.StoreAppearance = true;
            this.gridView1.OptionsLayout.StoreFormatRules = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonDropDownControl = this.backstageViewControl1;
            this.ribbonControl1.CommandLayout = DevExpress.XtraBars.Ribbon.CommandLayout.Simplified;
            this.ribbonControl1.Controller = this.barAndDockingController1;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
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
            this.biShowProperties});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 14;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
            this.ribbonControl1.Size = new System.Drawing.Size(1828, 190);
            // 
            // backstageViewControl1
            // 
            this.backstageViewControl1.Controller = this.barAndDockingController1;
            this.backstageViewControl1.Controls.Add(this.backstageViewClientControl1);
            this.backstageViewControl1.Items.Add(this.btiExport);
            this.backstageViewControl1.Location = new System.Drawing.Point(29, 907);
            this.backstageViewControl1.Name = "backstageViewControl1";
            this.backstageViewControl1.OwnerControl = this.ribbonControl1;
            this.backstageViewControl1.SelectedTab = this.btiExport;
            this.backstageViewControl1.SelectedTabIndex = 0;
            this.backstageViewControl1.Size = new System.Drawing.Size(342, 257);
            this.backstageViewControl1.TabIndex = 1;
            this.backstageViewControl1.VisibleInDesignTime = true;
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PropertiesRibbon.DefaultSimplifiedRibbonGlyphSize = 24;
            // 
            // backstageViewClientControl1
            // 
            this.backstageViewClientControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backstageViewClientControl1.Appearance.Options.UseFont = true;
            this.backstageViewClientControl1.Controls.Add(this.accordionControl1);
            this.backstageViewClientControl1.Location = new System.Drawing.Point(306, 62);
            this.backstageViewClientControl1.Name = "backstageViewClientControl1";
            this.backstageViewClientControl1.Size = new System.Drawing.Size(36, 195);
            this.backstageViewClientControl1.TabIndex = 1;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.aiExportCsv,
            this.aiExportDocx,
            this.aiExportHtml,
            this.aiExportMht,
            this.aiExportPdf,
            this.aiExportXls,
            this.aiExportXlsx});
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            this.accordionControl1.Size = new System.Drawing.Size(452, 195);
            this.accordionControl1.TabIndex = 0;
            // 
            // aiExportCsv
            // 
            this.aiExportCsv.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiExportCsv.Appearance.Default.Options.UseFont = true;
            this.aiExportCsv.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("aiExportCsv.ImageOptions.SvgImage")));
            this.aiExportCsv.Name = "aiExportCsv";
            this.aiExportCsv.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aiExportCsv.Text = "CSV";
            this.aiExportCsv.Click += new System.EventHandler(this.aiExportCsv_Click);
            // 
            // aiExportDocx
            // 
            this.aiExportDocx.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiExportDocx.Appearance.Default.Options.UseFont = true;
            this.aiExportDocx.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("aiExportDocx.ImageOptions.SvgImage")));
            this.aiExportDocx.Name = "aiExportDocx";
            this.aiExportDocx.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aiExportDocx.Text = "DOCX";
            this.aiExportDocx.Click += new System.EventHandler(this.aiExportDocx_Click);
            // 
            // aiExportHtml
            // 
            this.aiExportHtml.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiExportHtml.Appearance.Default.Options.UseFont = true;
            this.aiExportHtml.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("aiExportHtml.ImageOptions.SvgImage")));
            this.aiExportHtml.Name = "aiExportHtml";
            this.aiExportHtml.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aiExportHtml.Text = "HTML";
            this.aiExportHtml.Click += new System.EventHandler(this.aiExportHtml_Click);
            // 
            // aiExportMht
            // 
            this.aiExportMht.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiExportMht.Appearance.Default.Options.UseFont = true;
            this.aiExportMht.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("aiExportMht.ImageOptions.SvgImage")));
            this.aiExportMht.Name = "aiExportMht";
            this.aiExportMht.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aiExportMht.Text = "MHT";
            this.aiExportMht.Click += new System.EventHandler(this.aiExportMht_Click);
            // 
            // aiExportPdf
            // 
            this.aiExportPdf.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiExportPdf.Appearance.Default.Options.UseFont = true;
            this.aiExportPdf.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("aiExportPdf.ImageOptions.SvgImage")));
            this.aiExportPdf.Name = "aiExportPdf";
            this.aiExportPdf.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aiExportPdf.Text = "PDF";
            this.aiExportPdf.Click += new System.EventHandler(this.aiExportPdf_Click);
            // 
            // aiExportXls
            // 
            this.aiExportXls.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiExportXls.Appearance.Default.Options.UseFont = true;
            this.aiExportXls.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("aiExportXls.ImageOptions.SvgImage")));
            this.aiExportXls.Name = "aiExportXls";
            this.aiExportXls.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aiExportXls.Text = "XLS";
            this.aiExportXls.Click += new System.EventHandler(this.aiExportXls_Click);
            // 
            // aiExportXlsx
            // 
            this.aiExportXlsx.Appearance.Default.Font = new System.Drawing.Font("Segoe UI", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aiExportXlsx.Appearance.Default.Options.UseFont = true;
            this.aiExportXlsx.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("aiExportXlsx.ImageOptions.SvgImage")));
            this.aiExportXlsx.Name = "aiExportXlsx";
            this.aiExportXlsx.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.aiExportXlsx.Text = "XLSX";
            this.aiExportXlsx.Click += new System.EventHandler(this.aiExportXlsx_Click);
            // 
            // btiExport
            // 
            this.btiExport.Caption = "Export";
            this.btiExport.ContentControl = this.backstageViewClientControl1;
            this.btiExport.ImageOptions.ItemNormal.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btiExport.ImageOptions.ItemNormal.SvgImage")));
            this.btiExport.Name = "btiExport";
            this.btiExport.Selected = true;
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
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Customize View";
            this.barButtonItem1.Id = 11;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // biSaveCustomization
            // 
            this.biSaveCustomization.Caption = "Save Table Properties";
            this.biSaveCustomization.Id = 12;
            this.biSaveCustomization.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biSaveCustomization.ImageOptions.SvgImage")));
            this.biSaveCustomization.Name = "biSaveCustomization";
            this.biSaveCustomization.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biSaveCustomization_ItemClick);
            // 
            // biShowProperties
            // 
            this.biShowProperties.Caption = "Customize Table Properties";
            this.biShowProperties.Id = 13;
            this.biShowProperties.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biShowProperties.ImageOptions.SvgImage")));
            this.biShowProperties.Name = "biShowProperties";
            this.biShowProperties.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.biShowProperties_CheckedChanged);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1,
            this.ribbonPageGroup2});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Common";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barSubItem1);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Common";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.biSaveCustomization);
            this.ribbonPageGroup2.ItemLinks.Add(this.biShowProperties);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // xtraSaveFileDialog1
            // 
            this.xtraSaveFileDialog1.FileName = "xtraSaveFileDialog1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 190);
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
            this.splitContainerControl1.Size = new System.Drawing.Size(1828, 921);
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
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1828, 1111);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.backstageViewControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "TableForm";
            this.Ribbon = this.ribbonControl1;
            this.Text = "TableForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backstageViewControl1)).EndInit();
            this.backstageViewControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            this.backstageViewClientControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraEditors.XtraSaveFileDialog xtraSaveFileDialog1;
        private DevExpress.XtraBars.Ribbon.BackstageViewControl backstageViewControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem btiExport;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aiExportCsv;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aiExportDocx;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aiExportHtml;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aiExportMht;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aiExportXls;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aiExportXlsx;
        private DevExpress.XtraBars.Navigation.AccordionControlElement aiExportPdf;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem biCsvExport;
        private DevExpress.XtraBars.BarButtonItem biDocxExport;
        private DevExpress.XtraBars.BarButtonItem biHtmlExport;
        private DevExpress.XtraBars.BarButtonItem biMhtExport;
        private DevExpress.XtraBars.BarButtonItem biPdfExport;
        private DevExpress.XtraBars.BarButtonItem biXlsExport;
        private DevExpress.XtraBars.BarButtonItem biXlsxExport;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraBars.BarButtonItem biSaveCustomization;
        private DevExpress.XtraBars.BarCheckItem biShowProperties;
    }
}