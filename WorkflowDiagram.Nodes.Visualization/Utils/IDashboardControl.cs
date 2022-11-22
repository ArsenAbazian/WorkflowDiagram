using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WokflowDiagram.Nodes.Visualization.Utils {
    public interface IWfDashboardControl {
        void OnApplyWorkspace();
        void OnInitialized();
    }
}
