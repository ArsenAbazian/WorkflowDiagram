
namespace WokflowDiagram.Nodes.Visualization.Forms {
    partial class DashboardForm {
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
            this.dashboardContainerControl1 = new WokflowDiagram.Nodes.Visualization.Forms.DashboardContainerControl();
            this.SuspendLayout();
            // 
            // dashboardContainerControl1
            // 
            this.dashboardContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.dashboardContainerControl1.Name = "dashboardContainerControl1";
            this.dashboardContainerControl1.Size = new System.Drawing.Size(800, 450);
            this.dashboardContainerControl1.TabIndex = 0;
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dashboardContainerControl1);
            this.Name = "DashboardForm";
            this.Text = "DashboardForm";
            this.ResumeLayout(false);

        }

        #endregion

        private DashboardContainerControl dashboardContainerControl1;
    }
}