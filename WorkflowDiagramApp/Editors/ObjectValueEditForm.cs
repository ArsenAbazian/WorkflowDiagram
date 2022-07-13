using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkflowDiagramApp.StrategyDocument;

namespace WorkflowDiagramApp.Editors {
    public partial class ObjectValueEditForm : XtraForm {
        public ObjectValueEditForm() {
            InitializeComponent();

            this.memoEdit1.Visible = false;
            this.radioGroup1.Visible = false;
            this.spinEdit1.Visible = true;
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(((BarCheckItem)e.Item).Checked)
                Type = WfValueType.Decimal;
        }

        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(((BarCheckItem)e.Item).Checked)
                Type = WfValueType.Boolean;
        }

        private void barCheckItem3_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(((BarCheckItem)e.Item).Checked)
                Type = WfValueType.String;
        }

        WfValueType type = WfValueType.Decimal;
        protected WfValueType Type {
            get { return type; }
            set {
                if(Type == value)
                    return;
                type = value;
                OnTypeChanged();
            }
        }

        protected virtual void OnTypeChanged() {
            if(Type == WfValueType.Decimal) {
                this.memoEdit1.Visible = false;
                this.radioGroup1.Visible = false;
                this.spinEdit1.Visible = true;
                this.spinEdit1.EditValue = Convert.ToDecimal(Value);
            }
            else if(Type == WfValueType.Boolean) {
                this.memoEdit1.Visible = false;
                this.spinEdit1.Visible = false;
                this.radioGroup1.Visible = true;
                this.radioGroup1.EditValue = Convert.ToBoolean(Value);
            }
            else {
                this.spinEdit1.Visible = false;
                this.radioGroup1.Visible = false;
                this.memoEdit1.Visible = true;
                this.radioGroup1.EditValue = Convert.ToString(Value);
            }
        }

        object val;
        public object Value {
            get { return val; }
            set {
                if(object.Equals(Value, value)) {
                    UpdateView();
                    return;
                }
                val = value;
                OnValueChanged();
            }
        }

        private void UpdateView() {
            if(Value is string) {
                Type = WfValueType.String;
            }
            else if(Value is bool) {
                Type = WfValueType.Boolean;
            }
            else {
                Type = WfValueType.Decimal;
            }
        }

        protected virtual void OnValueChanged() {
            if(Value is string) {
                Type = WfValueType.String;
                this.memoEdit1.EditValue = Convert.ToString(Value);
            }
            else if(Value is bool) {
                Type = WfValueType.Boolean;
                this.radioGroup1.EditValue = Convert.ToBoolean(Value);
            }
            else {
                Type = WfValueType.Decimal;
                this.spinEdit1.EditValue = Convert.ToDecimal(Value);
            }
                
        }

        private void sbOk_Click(object sender, EventArgs e) {
            if(Type == WfValueType.Decimal)
                Value = Convert.ToDouble(this.spinEdit1.Value);
            else if(Type == WfValueType.Boolean)
                Value = Convert.ToBoolean(this.radioGroup1.EditValue);
            else
                Value = this.memoEdit1.Text == null? string.Empty: this.memoEdit1.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
