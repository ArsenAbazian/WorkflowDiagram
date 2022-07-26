using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using WorkflowDiagramApp.Helpers;

namespace WfBaseScript.Editors {
    public partial class PropertyPathControl : XtraUserControl {
        public PropertyPathControl() {
            InitializeComponent();
        }

        public TreeList Control { get { return this.tlProperties; } }

        private void tlGroups_VirtualTreeGetCellValue(object sender, DevExpress.XtraTreeList.VirtualTreeGetCellValueInfo e) {
            if(e.Column == tlcName) {
                if(e.Node is Type)
                    e.CellData = "Context";
                else if(e.Node is PropertyInfo)
                    e.CellData = ((PropertyInfo)e.Node).Name;
            }
            else {
                if(e.Node is Type)
                    e.CellData = ((Type)e.Node).Name;
                else if(e.Node is PropertyInfo)
                    e.CellData = ((PropertyInfo)e.Node).PropertyType.Name;
            }
        }

        private void tlGroups_VirtualTreeGetChildNodes(object sender, DevExpress.XtraTreeList.VirtualTreeGetChildNodesInfo e) {
            if(e.Node == null)
                return;
            if(e.Node is Type)
                e.Children = ScriptBindingInfo.GetPublicProperties((Type)e.Node).Values.ToList();
            else if(e.Node is PropertyInfo) {
                PropertyInfo p = (PropertyInfo)e.Node;
                if(p.PropertyType == typeof(string) || p.PropertyType == typeof(DateTime))
                    return;
                else
                    e.Children = ScriptBindingInfo.GetPublicProperties(((PropertyInfo)e.Node).PropertyType).Values.ToList();
            }
        }

        Type context = null;
        public Type Context {
            get { return context; }
            set {
                if(Context == value)
                    return;
                context = value;
                OnContextChanged();
            }
        }

        private void OnContextChanged() {
            this.tlProperties.DataSource = Context;
            this.tlProperties.RootValue = Context;
            OnPropertyPathChanged();
        }

        string propertyPath;
        public string PropertyPath {
            get {
                if(propertyPath == null)
                    propertyPath = GetPropertyPath();
                return propertyPath;
            }
            set {
                if(propertyPath == value)
                    return;
                propertyPath = value;
                OnPropertyPathChanged();
            }
        }

        protected virtual void OnPropertyPathChanged() {
            string[] props = PropertyPath.Split('.');
            List<TreeListNode> nodes = this.tlProperties.GetNodeList();
            foreach(string prop in props) {
                nodes = nodes.Where(n => n.GetValue(this.tlcName).ToString() == prop).ToList();
                if(nodes.Count == 0)
                    return;
            }
            this.tlProperties.FocusedNode = nodes[0];
        }

        private void tlProperties_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e) {
            this.propertyPath = GetPropertyPath();
        }

        private string GetPropertyPath() {
            if(this.tlProperties.FocusedNode == null)
                return "";
            StringBuilder b = new StringBuilder();
            TreeListNode current = this.tlProperties.FocusedNode;
            b.Append(current.GetValue(this.tlcName));
            current = current.ParentNode;
            while(current != null) {
                b.Insert(0, ".");
                b.Insert(0, current.GetValue(this.tlcName));
                current = current.ParentNode;
            }
            return b.ToString();
        }

        private void tlProperties_RowCellClick(object sender, DevExpress.XtraTreeList.RowCellClickEventArgs e) {
            if(e.Clicks == 2 && e.Button == MouseButtons.Left) {
                this.propertyPath = GetPropertyPath();
                RaiseEditValueChanged();
            }
        }

        protected void RaiseEditValueChanged() {
            if(UserValueSelected != null)
                UserValueSelected.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler UserValueSelected;
    }
}
