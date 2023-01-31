﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;

namespace WorkflowDiagram.Nodes.Visualization.Interfaces {
    public interface IWfTableForm : IWfPlatformForm {
        ITableNode Node { get; set; }
        object DataSource { get; set; }
    }
}
