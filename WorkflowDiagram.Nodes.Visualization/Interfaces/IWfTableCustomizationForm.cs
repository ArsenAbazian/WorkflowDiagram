using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Visualization.Interfaces {
    public interface IWfTableCustomizationForm : IWfPlatformForm {
        string XmlCustomizationText { get; set; }
        object DataSource { get; set; }

        bool ShowFormDialog();
    }
}
