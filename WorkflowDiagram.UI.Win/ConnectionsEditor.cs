using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkflowDiagram.Editors;

namespace WorkflowDiagram.UI.Win {
    public partial class ConnectionsEditor : XtraUserControl {
        public ConnectionsEditor() {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Connections.Add(Connections.Node.CreateConnectionPoint(Connections.Type));
        }

        private void biRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            WfConnectionPoint pt = (WfConnectionPoint)this.gridView1.GetFocusedRow();
            if(!pt.AllowedOperations.HasFlag(WfEditOperation.Remove))
                return;
            Connections.Remove(pt);
        }

        WfConnectionPointCollection connections;
        public WfConnectionPointCollection Connections {
            get { return connections; }
            set {
                if(Connections == value)
                    return;
                connections = value;
                OnConnectionsChanged();
            }
        }

        protected virtual void OnConnectionsChanged() {
            this.gridControl1.DataSource = Connections;
            UpdateButtons();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) {
            UpdateButtons();
            WfConnectionPoint pt = (WfConnectionPoint)this.gridView1.GetFocusedRow();
            if(pt == null)
                return;
                ShowPropertiesForm(pt);
        }

        protected virtual void UpdateButtons() {
            if(Connections == null) {
                this.biAdd.Enabled = false;
                this.biRemove.Enabled = false;
                return;
            }
            WfConnectionPoint pt = (WfConnectionPoint)this.gridView1.GetFocusedRow();
            if(pt == null || !pt.AllowedOperations.HasFlag(WfEditOperation.Remove))
                this.biRemove.Enabled = false;
            else
                this.biRemove.Enabled = true;
            this.biAdd.Enabled = Connections.AllowedOperations.HasFlag(WfEditOperation.Add);
        }

        private void gridView1_ShownEditor(object sender, EventArgs e) {
            if(this.gridView1.FocusedColumn == this.colValue) {
                WfConnectionPoint pt = (WfConnectionPoint)this.gridView1.GetFocusedRow();
                var editor = this.gridView1.ActiveEditor.Properties as IPropertyEditor;
                if(editor != null)
                    editor.Initialize(pt, nameof(pt.Value), pt.Value);
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e) {
            WfConnectionPoint pt = (WfConnectionPoint)this.gridView1.GetFocusedRow();
            if(!pt.AllowedOperations.HasFlag(WfEditOperation.Edit)) {
                if(this.gridView1.FocusedColumn != this.colText || this.gridView1.FocusedColumn != this.colName)
                    e.Cancel = true;
            }
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e) {
            
        }

        private void gridView1_CustomRowCellEditForEditing(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e) {
            if(e.Column == colValue) {
                WfConnectionPoint pt = (WfConnectionPoint)this.gridView1.GetRow(e.RowHandle);
                var attrs = pt.GetType().GetProperty(nameof(pt.Value)).GetCustomAttributes(typeof(PropertyEditorAttribute), true);
                if(attrs.Count() == 0)
                    return;
                PropertyEditorAttribute attr = (PropertyEditorAttribute)attrs.First();
                if(attr == null)
                    return;
                e.RepositoryItem = (RepositoryItem)attr.EditorType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                ((IPropertyEditor)e.RepositoryItem).Initialize(pt, nameof(pt.Value), e.CellValue);
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            WfConnectionPoint pt = (WfConnectionPoint)this.gridView1.GetFocusedRow();
            ShowPropertiesForm(pt);
        }

        private readonly object showPointProperties = new object();
        public event ShowPointValueEventHandler ShowPointProperties {
            add { Events.AddHandler(showPointProperties, value); }
            remove { Events.RemoveHandler(showPointProperties, value); }
        }
        protected virtual void ShowPropertiesForm(WfConnectionPoint pt) {
            ShowPointValueEventHandler handler = Events[showPointProperties] as ShowPointValueEventHandler;
            if(handler != null)
                handler.Invoke(this, new WfPointValueEventArgs() { Point = pt });
        }
    }

    public delegate void ShowPointValueEventHandler(object sender, WfPointValueEventArgs e);

    public class WfPointValueEventArgs : EventArgs {
        public WfConnectionPoint Point { get; set; }
    }
}
