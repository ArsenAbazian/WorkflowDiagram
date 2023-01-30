using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public interface IWfPlatformForm : IDisposable {
        void Show();
    }
    
    public interface IWfPlatformServices {
        T CreateForm<T>() where T : IWfPlatformForm;
        bool ShouldRecreateForm(IWfPlatformForm form);
        T GetService<T>(WfNode forNode);
    }
}
