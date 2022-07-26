using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DialogueSystem;

namespace DialogueSystemEditor {
    public partial class PropertyForm : DevExpress.XtraEditors.XtraForm {
        public PropertyForm() {
            InitializeComponent();
        }

        PropertyStoreBase property;
        public PropertyStoreBase Property {
            get { return property; }
            set {
                if(Property == value)
                    return;
                property = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged() {
            this.dataLayoutControl1.DataSource = Property;
        }

        public PropertyStorage Data { get; internal set; }
        public bool NewItem { get; internal set; }

        private void simpleButton1_Click(object sender, EventArgs e) {
            if(string.IsNullOrEmpty(Property.Key)) {
                XtraMessageBox.Show("Empty name is no allowed. Please specify correct name.", "Property");
                return;
            }
            Property.Key = Property.Key.Trim();
            if(NewItem && Data.Contains(Property.Key)) {
                XtraMessageBox.Show("Property with such name already defined. Please specify another name.", "Property");
                return;
            }
            if(string.IsNullOrEmpty(Property.Key)) {
                XtraMessageBox.Show("Empty name is no allowed. Please specify correct name.", "Property");
                return;
            }
            Close();
            DialogResult = DialogResult.OK;
        }
    }
}