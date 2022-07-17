
namespace WorkflowDiagram.UI.Win {
    partial class ConnectionsEditor {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionsEditor));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.wfConnectionPointBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colColorString = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRequirement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocument = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAllowedOperations = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.biAdd = new DevExpress.XtraBars.BarButtonItem();
            this.biRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfConnectionPointBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.wfConnectionPointBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 46);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1383, 585);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // wfConnectionPointBindingSource
            // 
            this.wfConnectionPointBindingSource.DataSource = typeof(WorkflowDiagram.WfConnectionPoint);
            // 
            // gridView1
            // 
            this.gridView1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNode,
            this.colValue,
            this.colName,
            this.colText,
            this.colColorString,
            this.colRequirement,
            this.colDocument,
            this.colType,
            this.colAllowedOperations});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEdit);
            this.gridView1.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView1_CustomRowCellEditForEditing);
            this.gridView1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridView1_ShowingEditor);
            this.gridView1.ShownEditor += new System.EventHandler(this.gridView1_ShownEditor);
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            // 
            // colNode
            // 
            this.colNode.FieldName = "Node";
            this.colNode.MinWidth = 40;
            this.colNode.Name = "colNode";
            this.colNode.OptionsColumn.ReadOnly = true;
            this.colNode.Width = 150;
            // 
            // colValue
            // 
            this.colValue.FieldName = "Value";
            this.colValue.MinWidth = 40;
            this.colValue.Name = "colValue";
            this.colValue.Visible = true;
            this.colValue.VisibleIndex = 3;
            this.colValue.Width = 150;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.MinWidth = 40;
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 150;
            // 
            // colText
            // 
            this.colText.FieldName = "Text";
            this.colText.MinWidth = 40;
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 1;
            this.colText.Width = 150;
            // 
            // colColorString
            // 
            this.colColorString.FieldName = "ColorString";
            this.colColorString.MinWidth = 40;
            this.colColorString.Name = "colColorString";
            this.colColorString.Width = 150;
            // 
            // colRequirement
            // 
            this.colRequirement.FieldName = "Requirement";
            this.colRequirement.MinWidth = 40;
            this.colRequirement.Name = "colRequirement";
            this.colRequirement.Visible = true;
            this.colRequirement.VisibleIndex = 2;
            this.colRequirement.Width = 150;
            // 
            // colDocument
            // 
            this.colDocument.FieldName = "Document";
            this.colDocument.MinWidth = 40;
            this.colDocument.Name = "colDocument";
            this.colDocument.OptionsColumn.ReadOnly = true;
            this.colDocument.Width = 150;
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.MinWidth = 40;
            this.colType.Name = "colType";
            this.colType.Width = 150;
            // 
            // colAllowedOperations
            // 
            this.colAllowedOperations.FieldName = "AllowedOperations";
            this.colAllowedOperations.MinWidth = 40;
            this.colAllowedOperations.Name = "colAllowedOperations";
            this.colAllowedOperations.Width = 150;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.biAdd,
            this.biRemove});
            this.barManager1.MaxItemId = 2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.biAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.biRemove)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // biAdd
            // 
            this.biAdd.Caption = "Add";
            this.biAdd.Id = 0;
            this.biAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biAdd.ImageOptions.SvgImage")));
            this.biAdd.Name = "biAdd";
            this.biAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // biRemove
            // 
            this.biRemove.Caption = "Remove";
            this.biRemove.Id = 1;
            this.biRemove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("biRemove.ImageOptions.SvgImage")));
            this.biRemove.Name = "biRemove";
            this.biRemove.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.biRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.biRemove_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1383, 46);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 631);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1383, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 46);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 585);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1383, 46);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 585);
            // 
            // ConnectionsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ConnectionsEditor";
            this.Size = new System.Drawing.Size(1383, 631);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wfConnectionPointBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem biAdd;
        private DevExpress.XtraBars.BarButtonItem biRemove;
        private System.Windows.Forms.BindingSource wfConnectionPointBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colNode;
        private DevExpress.XtraGrid.Columns.GridColumn colValue;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colColorString;
        private DevExpress.XtraGrid.Columns.GridColumn colRequirement;
        private DevExpress.XtraGrid.Columns.GridColumn colDocument;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colAllowedOperations;
    }
}
