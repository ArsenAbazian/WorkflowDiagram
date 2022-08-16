using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkflowDiagram.Nodes.Base;

namespace WorkflowDiagramApp {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            WindowsFormsSettings.ForceDirectXPaint();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WfConstantValueNode wfConstantValueNode = new WfConstantValueNode(1.0);

            Application.Run(new MainForm());
        }
    }
}
