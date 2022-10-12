using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization.Utils;

namespace WokflowDiagram.Nodes.Visualization.Forms {
    public partial class TableUserControl : XtraUserControl, IWfDashboardControl {
        public TableUserControl() {
            CurrencyDataController.DisableThreadingProblemsDetection = true;
            InitializeComponent();
            this.xtraSaveFileDialog1.Filter = 
                "Csv files|*.csv|Docx files|*.docx|Html files|*.html|Mht files|*.mht|Pdf files|*.pdf|Xls files|*.xls|Xlsx files|*.xlsx|All files|*.*";
        }

        public object DataSource { get { return this.gridControl1.DataSource; } set { this.gridControl1.DataSource = value; } }

        protected internal virtual void OnFormShown(Form form) {
            foreach(GridColumn column in this.gridView1.Columns) {
                column.AllowSummaryMenu = true;
            }
            if(Node != null)
                RestoreLayout(Node.XmlConfigurationText);
            this.propertyGridControl1.SelectedObject = new FilteredGridViewProperties(this.gridView1);
        }

        void SaveCsv() {
            this.xtraSaveFileDialog1.Filter = "Csv files *.csv|*.csv|All files *.*|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
            if(this.xtraSaveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.gridControl1.ExportToCsv(this.xtraSaveFileDialog1.FileName);
        }

        void SaveDocX() {
            this.xtraSaveFileDialog1.Filter = "Docx files *.docx|*.docx|All files *.*|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
            if(this.xtraSaveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.gridControl1.ExportToDocx(this.xtraSaveFileDialog1.FileName);
        }

        void SaveHtml() {
            this.xtraSaveFileDialog1.Filter = "Html files *.html|*.html|All files *.*|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
            if(this.xtraSaveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.gridControl1.ExportToHtml(this.xtraSaveFileDialog1.FileName);
        }

        protected internal void RestoreLayout(string xmlConfigurationText) {
            if(string.IsNullOrEmpty(xmlConfigurationText))
                return;
            
            MemoryStream m = new MemoryStream(xmlConfigurationText.Length);
            StreamWriter w = new StreamWriter(m);
            w.Write(xmlConfigurationText);
            w.Flush();
            m.Seek(0, SeekOrigin.Begin);

            this.gridView1.RestoreLayoutFromStream(m);
            this.gridView1.OptionsBehavior.Editable = false;
        }

        void SaveMht() {
            this.xtraSaveFileDialog1.Filter = "Mht files *.mht|*.mht|All files *.*|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
            if(this.xtraSaveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.gridControl1.ExportToMht(this.xtraSaveFileDialog1.FileName);
        }

        void SavePdf() {
            this.xtraSaveFileDialog1.Filter = "Pdf files *.pdf|*.pdf|All files *.*|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
            if(this.xtraSaveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.gridControl1.ExportToPdf(this.xtraSaveFileDialog1.FileName);
        }

        void SaveXls() {
            this.xtraSaveFileDialog1.Filter = "Xls files *.xls|*.xls|All files *.*|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
            if(this.xtraSaveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.gridControl1.ExportToXls(this.xtraSaveFileDialog1.FileName);
        }

        void SaveXlsx() {
            this.xtraSaveFileDialog1.Filter = "Xlsx files *.xlsx|*.xlsx|All files *.*|*.*";
            this.xtraSaveFileDialog1.FilterIndex = 0;
            if(this.xtraSaveFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            this.gridControl1.ExportToXlsx(this.xtraSaveFileDialog1.FileName);
        }

        private void aiExportCsv_Click(object sender, EventArgs e) {
            SaveCsv();
        }

        private void aiExportDocx_Click(object sender, EventArgs e) {
            SaveDocX();
        }

        private void aiExportHtml_Click(object sender, EventArgs e) {
            SaveHtml();
        }

        private void aiExportMht_Click(object sender, EventArgs e) {
            SaveMht();
        }

        private void aiExportPdf_Click(object sender, EventArgs e) {
            SavePdf();
        }

        private void aiExportXls_Click(object sender, EventArgs e) {
            SaveXls();
        }

        private void aiExportXlsx_Click(object sender, EventArgs e) {
            SaveXlsx();
        }

        private void biCsvExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveCsv();
        }

        private void biDocxExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveDocX();
        }

        private void biHtmlExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveHtml();
        }

        private void biMhtExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveMht();
        }

        private void biPdfExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SavePdf();
        }

        private void biXlsExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveXls();
        }

        private void biXlsxExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveXlsx();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both;
        }

        private void biSaveCustomization_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            //this.splitContainerControl1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel1;
            if(Node == null) {
                XtraMessageBox.Show("Node not specified. Skip layout save");
                return;
            }

            MemoryStream m = new MemoryStream();
            this.gridView1.SaveLayoutToStream(m);
            m.Seek(0, SeekOrigin.Begin);
            StreamReader r = new StreamReader(m);
            Node.XmlConfigurationText = r.ReadToEnd();
            XtraMessageBox.Show("Layout Saved To The Node");
        }

        public ITableNode Node { get; set; }

        private void biShowProperties_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.splitContainerControl1.PanelVisibility = this.biShowProperties.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel1;
            if(this.biShowProperties.Checked)
                this.propertyGridControl1.SelectedObject = new FilteredGridViewProperties(this.gridView1);
        }

        private void biSelectedColumnSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {

        }

        private void biTableSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.propertyGridControl1.SelectedObject = new FilteredGridViewProperties(this.gridView1);
        }

        private void gridControl1_MouseDown(object sender, MouseEventArgs e) {
            
        }

        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e) {
            if(e.HitInfo.InColumn) {
                e.Menu.Items.Add(new DevExpress.Utils.Menu.DXMenuItem("Customize Column", (d, ee) => {
                    this.biShowProperties.Checked = true;
                    this.propertyGridControl1.SelectedObject = new FilteredColumnProperties(e.HitInfo.Column);
                }));
            }
        }

        void IWfDashboardControl.OnApplyWorkspace() {
            
        }
        
        void IWfDashboardControl.OnInitialized() {
            
        }
    }

    public class FilteredColumnProperties {
        public FilteredColumnProperties(GridColumn column) {
            Column = column;
        }
        protected GridColumn Column { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DXCategory("Appearance")]
        public AppearanceObject AppearanceHeader {
            get { return Column.AppearanceHeader; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DXCategory("Appearance")]
        public AppearanceObject AppearanceCell {
            get { return Column.AppearanceCell; }
        }

        public string Caption { get => Column.Caption; set => Column.Caption = value; }
        public FixedStyle Fixed { get => Column.Fixed; set => Column.Fixed = value; }
        public FormatType FormatType { get => Column.DisplayFormat.FormatType; set => Column.DisplayFormat.FormatType = value; }
        public string FormatString { get => Column.DisplayFormat.FormatString; set => Column.DisplayFormat.FormatString = value; }
    }
}
