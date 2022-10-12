using DevExpress.Utils;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraBars.Docking2010.Views.Widget;
using DevExpress.XtraEditors;
using System;
using System.Collections;
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
    public partial class DashboardContainerControl : XtraUserControl {
        public DashboardContainerControl() {
            InitializeComponent();
        }

        public WfDashboardFormNode Node { get; internal set; }
        public WidgetView WidgetView { get => this.widgetView1; }
        public DocumentManager DocumentManager { get => this.documentManager1; }

        internal void OnFormLoad(DashboardForm dashboardForm) {
            //this.workspaceManager1.TargetControl = FindForm();
            InitializeDockPanels();
            if(Node.Workspaces.Count == 0)
                return;
            LoadWorkspaces();
            ApplySelectedWorkspace();
        }
        
        protected virtual void ApplySelectedWorkspace() {
            if(string.IsNullOrEmpty(Node.SelectedWorkspaceName)) {
                if(this.workspaceManager1.Workspaces.Count > 0) {
                    Node.SelectedWorkspaceName = this.workspaceManager1.Workspaces[0].Name;
                    this.workspaceManager1.ApplyWorkspace(Node.SelectedWorkspaceName);
                }
            }
            else {
                this.workspaceManager1.ApplyWorkspace(Node.SelectedWorkspaceName);
            }
        }

        protected List<Control> VisualizationControls { get; } = new List<Control>();
        protected virtual void InitializeDockPanels() {
            DocumentManager.ForceInitialize();
            IEnumerable en = Node.DataContext as IEnumerable;
            if(en == null)
                return;
            int index = 0;
            foreach(var item in en) {
                index++;
                Control control = null;
                WfDashboardPanelNode node = item as WfDashboardPanelNode;
                if(item is Control)
                    control = (Control)item;
                else if(item is WfDashboardPanelNode) {
                    control = node.CreateVisualizationControl();
                    control.Text = node.Caption;
                }
                if(control == null)
                    continue;
                VisualizationControls.Add(control);
                if(control is IWfDashboardControl)
                    ((IWfDashboardControl)control).OnInitialized();
                Document doc = (Document)WidgetView.AddDocument(control, control.Text);
                if(node != null) {
                    if(string.IsNullOrEmpty(node.PanelName))
                        node.PanelName = "Panel" + index;
                    control.Name = node.PanelName;
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
        }

        protected internal virtual void OnFormShown(DashboardForm dashboardForm) {
            
        }

        private void biResetLayout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.workspaceManager1.Workspaces.Clear();
            SaveWorkspacesToNode();
        }

        private void workspaceManager1_PropertySerializing(object sender, DevExpress.Utils.PropertyCancelEventArgs ea) {
            
        }

        private void workspaceManager1_WorkspaceSaved(object sender, DevExpress.Utils.WorkspaceEventArgs args) {
            SaveWorkspacesToNode();
            Node.SelectedWorkspaceName = args.Workspace.Name;
        }

        protected virtual void SaveWorkspacesToNode() {
            if(SkipSave)
                return;
            List<WfWorkspaceInfo> list = new List<WfWorkspaceInfo>();
            foreach(var w in this.workspaceManager1.Workspaces) {
                WfWorkspaceInfo info = new WfWorkspaceInfo();
                info.Name = w.Name;
                info.XmlSerializationData = StreamToString(w.SerializationData);
                if(info.XmlSerializationData == null)
                    continue;
                list.Add(info);
            }
            Node.Workspaces.Clear();
            Node.Workspaces.AddRange(list);
        }

        protected bool SkipSave { get; set; }
        protected virtual void LoadWorkspaces() {
            SkipSave = true;
            try {
                foreach(var w in Node.Workspaces)
                    this.workspaceManager1.LoadWorkspace(w.Name, StringToStream(w.XmlSerializationData));
            }
            finally {
                SkipSave = false;
            }
        }

        protected virtual string StreamToString(object serializationData) {
            if(serializationData is string)
                return (string)serializationData;
            if(serializationData is byte[])
                return Encoding.UTF8.GetString((byte[])serializationData);
            return null;
        }

        protected virtual Stream StringToStream(string xml) {
            MemoryStream m = new MemoryStream(Encoding.UTF8.GetBytes(xml));
            m.Seek(0, SeekOrigin.Begin);
            return m;
        }

        private void workspaceManager1_WorkspaceCollectionChanged(object sender, DevExpress.Utils.WorkspaceCollectionChangedEventArgs ea) {
            SaveWorkspacesToNode();
            Node.SelectedWorkspaceName = ea.Workspace.Name;
        }

        private void workspaceManager1_AfterApplyWorkspace(object sender, EventArgs e) {
            WorkspaceEventArgs ee = (WorkspaceEventArgs)e;
            Node.SelectedWorkspaceName = ee.Workspace.Name;
            foreach(var control in VisualizationControls) {
                if(control is IWfDashboardControl)
                    ((IWfDashboardControl)control).OnApplyWorkspace();
            }
        }
    }
}
