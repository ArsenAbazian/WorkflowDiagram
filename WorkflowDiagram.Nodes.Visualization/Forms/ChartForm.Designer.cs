
namespace WokflowDiagram.Nodes.Visualization.Forms {
    partial class ChartForm {
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
            this.chartUserControl1 = new WokflowDiagram.Nodes.Visualization.Forms.ChartUserControl();
            this.SuspendLayout();
            // 
            // chartUserControl1
            // 
            this.chartUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartUserControl1.Location = new System.Drawing.Point(0, 0);
            this.chartUserControl1.Name = "chartUserControl1";
            this.chartUserControl1.Node = null;
            this.chartUserControl1.Size = new System.Drawing.Size(1422, 821);
            this.chartUserControl1.TabIndex = 0;
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1422, 821);
            this.Controls.Add(this.chartUserControl1);
            this.Name = "ChartForm";
            this.Text = "ChartForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ChartUserControl chartUserControl1;
    }
}