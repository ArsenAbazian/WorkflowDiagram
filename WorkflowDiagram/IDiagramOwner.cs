using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public interface IWfDocumentOwner {
        void BeforeDocumentInitialize(WfRunner runner, WfDocument document);
        void AfterDocumentInitialize(WfRunner runner, WfDocument document);
        void OnReset(WfDocument document);
        void OnReset(WfRunner runner);
        WfRunner CreateRunner(WfDocument document);
    }
}
