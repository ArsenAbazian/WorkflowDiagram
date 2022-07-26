using DevExpress.XtraEditors;
using DialogueSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogueSystemEditor {
    public partial class PropertyPathForm : XtraForm {
        public PropertyPathForm() {
            InitializeComponent();
            
            this.propertyPathControl1.Control.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.propertyPathControl1.Control.OptionsFind.AlwaysVisible = true;
            this.propertyPathControl1.Control.OptionsFind.AllowIncrementalSearch = true;

            this.storageControl1.PropertySelected += OnStorageControlPropertySelected;
        }

        private void OnStorageControlPropertySelected(object sender, StoragePropertyEventArgs e) {
            this.propertyPathControl1.PropertyPath = "Data.Properties[" + e.Property.Key + "]";
        }

        public Type Context {
            get { return this.propertyPathControl1.Context; }
            set { this.propertyPathControl1.Context = value; }
        }

        DialogueData data;
        public DialogueData Data {
            get { return data; }
            set {
                if(Data == value)
                    return;
                data = value;
                OnDataChanged();
            }
        }

        protected virtual void OnDataChanged() {
            this.propertyPathControl1.Data = Data;
            this.storageControl1.Data = Data.Storage;
        }

        public string PropertyPath {
            get { return this.propertyPathControl1.PropertyPath; }
            set { this.propertyPathControl1.PropertyPath = value; }
        }

        public bool ShowFilterPanel { get; internal set; }

        private void simpleButton11_Click(object sender, EventArgs e) {
            
        }
    }
}
