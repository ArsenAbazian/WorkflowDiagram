using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization.Forms;

namespace WokflowDiagram.Nodes.Visualization.Managers {
    public class TableVisualizationManager {
        static TableVisualizationManager defaultManager;
        public static TableVisualizationManager Default {
            get {
                if(defaultManager == null)
                    defaultManager = new TableVisualizationManager();
                return defaultManager;
            }
            set { defaultManager = value; }
        }

        public void InitializeTable(ITableNode node, TableUserControl control) {
            control.DataSource = node.DataSource;
        }
    }
}
