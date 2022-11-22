
namespace WokflowDiagram.Nodes.Visualization.Forms {
    partial class TableForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableUserControl1 = new WokflowDiagram.Nodes.Visualization.Forms.TableUserControl();
            this.SuspendLayout();
            // 
            // tableUserControl1
            // 
            this.tableUserControl1.DataSource = null;
            this.tableUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableUserControl1.Location = new System.Drawing.Point(0, 0);
            this.tableUserControl1.Name = "tableUserControl1";
            this.tableUserControl1.Node = null;
            this.tableUserControl1.Size = new System.Drawing.Size(1223, 732);
            this.tableUserControl1.TabIndex = 0;
            // 
            // TableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 732);
            this.Controls.Add(this.tableUserControl1);
            this.Name = "TableForm";
            this.Text = "Table Form";
            this.ResumeLayout(false);

        }

        #endregion

        private TableUserControl tableUserControl1;
    }
}