namespace WfBaseScript.Editors {
    partial class PropertyPathControl {
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
            this.tlProperties = new DevExpress.XtraTreeList.TreeList();
            this.tlcName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.tlcType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tlProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tlProperties
            // 
            this.tlProperties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tlProperties.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.tlcName,
            this.tlcType});
            this.tlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlProperties.FixedLineWidth = 1;
            this.tlProperties.HorzScrollStep = 2;
            this.tlProperties.KeyFieldName = "Id";
            this.tlProperties.Location = new System.Drawing.Point(0, 0);
            this.tlProperties.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tlProperties.MinWidth = 16;
            this.tlProperties.Name = "tlProperties";
            this.tlProperties.OptionsBehavior.Editable = false;
            this.tlProperties.OptionsBehavior.EditingMode = DevExpress.XtraTreeList.TreeListEditingMode.EditForm;
            this.tlProperties.OptionsBehavior.EditorShowMode = DevExpress.XtraTreeList.TreeListEditorShowMode.DoubleClick;
            this.tlProperties.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.RowFocus;
            this.tlProperties.OptionsView.ShowColumns = false;
            this.tlProperties.OptionsView.ShowHorzLines = false;
            this.tlProperties.OptionsView.ShowIndicator = false;
            this.tlProperties.OptionsView.ShowVertLines = false;
            this.tlProperties.ParentFieldName = "ParentId";
            this.tlProperties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.tlProperties.Size = new System.Drawing.Size(768, 562);
            this.tlProperties.TabIndex = 6;
            this.tlProperties.TreeLevelWidth = 10;
            this.tlProperties.RowCellClick += new DevExpress.XtraTreeList.RowCellClickEventHandler(this.tlProperties_RowCellClick);
            this.tlProperties.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.tlProperties_FocusedNodeChanged);
            this.tlProperties.VirtualTreeGetChildNodes += new DevExpress.XtraTreeList.VirtualTreeGetChildNodesEventHandler(this.tlGroups_VirtualTreeGetChildNodes);
            this.tlProperties.VirtualTreeGetCellValue += new DevExpress.XtraTreeList.VirtualTreeGetCellValueEventHandler(this.tlGroups_VirtualTreeGetCellValue);
            // 
            // tlcName
            // 
            this.tlcName.Caption = "Name";
            this.tlcName.FieldName = "Name";
            this.tlcName.MinWidth = 12;
            this.tlcName.Name = "tlcName";
            this.tlcName.Visible = true;
            this.tlcName.VisibleIndex = 0;
            this.tlcName.Width = 44;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            //new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", DialogueSystem.ScriptNodeType.Default, 0),
            //new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Group", DialogueSystem.ScriptNodeType.Group, 1),
            //new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Script", DialogueSystem.ScriptNodeType.Script, 2)
            });
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // tlcType
            // 
            this.tlcType.Caption = "Type";
            this.tlcType.FieldName = "TypeName";
            this.tlcType.Name = "tlcType";
            this.tlcType.Visible = true;
            this.tlcType.VisibleIndex = 1;
            // 
            // PropertyPathControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlProperties);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PropertyPathControl";
            this.Size = new System.Drawing.Size(768, 562);
            ((System.ComponentModel.ISupportInitialize)(this.tlProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList tlProperties;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn tlcType;
    }
}
