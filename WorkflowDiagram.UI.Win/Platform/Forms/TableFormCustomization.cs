using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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
using WokflowDiagram.Nodes.Visualization;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WokflowDiagram.Nodes.Visualization.Forms {
    public partial class TableFormCustomization : Form, IWfTableCustomizationForm {
        public TableFormCustomization() {
            InitializeComponent();
        }

        bool IWfTableCustomizationForm.ShowFormDialog() {
            var res = ShowDialog();
            return res == DialogResult.OK;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            
            if(!string.IsNullOrEmpty(XmlCustomizationText))
                RestoreLayout(XmlCustomizationText);
            this.propertyGridControl1.SelectedObject = new FilteredGridViewProperties(this.gridView1);
        }

        protected internal void RestoreLayout(string xmlConfigurationText) {
            if(xmlConfigurationText == string.Empty)
                return;

            MemoryStream m = new MemoryStream();
            StreamWriter w = new StreamWriter(m);
            w.Write(xmlConfigurationText);
            w.Flush();
            m.Seek(0, SeekOrigin.Begin);

            this.gridView1.RestoreLayoutFromStream(m);
            this.gridView1.OptionsBehavior.Editable = false;
        }

        private void biClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    
        public string XmlCustomizationText { get; set; }
        public object DataSource {
            get { return this.gridView1.DataSource; }
            set {
                if(value == null) {
                    InitDefaultPreviewData();
                    return;
                }
                this.gridControl1.DataSource = value;
            }
        }

        private void InitDefaultPreviewData() {
            List<WfTableNodeFormData> list = new List<WfTableNodeFormData>();
            for(int i = 0; i < 1000; i++) {
                WfTableNodeFormData data = new WfTableNodeFormData() { Value = i + 1, Text = "Text Preview " + (i + 1), Date = DateTime.Now.AddDays(-1000 + i) };
                list.Add(data);
            }
            this.gridControl1.DataSource = list;
        }

        private void biSaveAndClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            MemoryStream m = new MemoryStream();
            this.gridView1.SaveLayoutToStream(m);
            m.Seek(0, SeekOrigin.Begin);
            StreamReader r = new StreamReader(m);
            XmlCustomizationText = r.ReadToEnd();
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class WfTableNodeFormData {
        public int Value { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }

    public class FilteredGridViewProperties {
        public FilteredGridViewProperties(GridView view) {
            View = view;
        }
        protected GridView View {
            get; set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DXCategory("Appearance")]
        public GridViewAppearances Appearance {
            get { return View.Appearance; }
        }

        [DefaultValue(DrawFocusRectStyle.CellFocus)]
        [DXCategory("Appearance")]
        public DrawFocusRectStyle FocusRectStyle { get { return View.FocusRectStyle; } set { View.FocusRectStyle = value; } }

        FilteredOptionsView optionsView;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DXCategory("Options")]
        public FilteredOptionsView OptionsView {
            get {
                if(optionsView == null)
                    optionsView = new FilteredOptionsView(View.OptionsView);
                return optionsView;
            }
        }

        [DefaultValue(-1)]
        [DXCategory("Appearance")]
        public int RowHeight { get { return View.RowHeight; } set { View.RowHeight = value; } }

        [DefaultValue(0)]
        [DXCategory("Appearance")]
        public virtual int RowSeparatorHeight { get { return View.RowSeparatorHeight; } set { View.RowSeparatorHeight = value; } }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FilteredOptionsBehavior {
        public FilteredOptionsBehavior(GridOptionsBehavior opt) {
            Options = opt;
        }
        protected GridOptionsBehavior Options { get; set; }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FilteredOptionsView {
        public FilteredOptionsView(GridOptionsView opt) {
            Options = opt;
        }
        protected GridOptionsView Options { get; set; }

        [DefaultValue(false)]
        public virtual bool EnableAppearanceEvenRow { get { return Options.EnableAppearanceEvenRow; } set { Options.EnableAppearanceEvenRow = value; } }

        [DefaultValue(false)]
        public virtual bool EnableAppearanceOddRow { get { return Options.EnableAppearanceOddRow; } set { Options.EnableAppearanceOddRow = value; } }

        [DefaultValue(false)]
        public virtual bool RowAutoHeight { get { return Options.RowAutoHeight; } set { Options.RowAutoHeight = value; } }

        [DefaultValue(false)]
        public virtual bool ShowAutoFilterRow { get { return Options.ShowAutoFilterRow; } set { Options.ShowAutoFilterRow = value; } }

        [DefaultValue(true)]
        public virtual bool ShowColumnHeaders { get { return Options.ShowColumnHeaders; } set { Options.ShowColumnHeaders = value; } }

        [DefaultValue(true)]
        public virtual bool ShowGroupedColumns { get { return Options.ShowGroupedColumns; } set { Options.ShowGroupedColumns = value; } }

        [DefaultValue(true)]
        public virtual bool ShowGroupPanel { get { return Options.ShowGroupPanel; } set { Options.ShowGroupPanel = value; } }

        [DefaultValue(DefaultBoolean.Default)]
        public virtual DefaultBoolean ShowHorizontalLines { get { return Options.ShowHorizontalLines; } set { Options.ShowHorizontalLines = value; } }

        [DefaultValue(DefaultBoolean.Default)]
        public virtual DefaultBoolean ShowVerticalLines { get { return Options.ShowVerticalLines; } set { Options.ShowVerticalLines = value; } }

        [DefaultValue(true)]
        public virtual bool ShowIndicator { get { return Options.ShowIndicator; } set { Options.ShowIndicator = value; } }
    }

}
