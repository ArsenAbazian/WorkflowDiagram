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

namespace WokflowDiagram.Nodes.Visualization.Forms {
    public partial class TableFormCustomization : Form {
        public TableFormCustomization() {
            InitializeComponent();
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
}
