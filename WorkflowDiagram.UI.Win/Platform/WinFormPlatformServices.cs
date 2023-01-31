using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WokflowDiagram.Nodes.Visualization.Forms;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WorkflowDiagram.UI.Win.Platform {
    public class WinFormPlatformServices : IWfPlatformServices {
        protected List<object> RegisteredServices { get; } = new List<object>();
        protected List<Form> RegisteredForms { get; } = new List<Form>();

        public WinFormPlatformServices() {
            RegisteredServices.Add(new WinPlatformChartService());
            RegisteredServices.Add(new WinPlatformGaugeService());
            RegisteredServices.Add(new WinPlatformTableService());

            RegisteredForms.Add(new DashboardForm());
        }

        T IWfPlatformServices.CreateForm<T>() {
            foreach(object form in RegisteredForms) {
                if(form is T)
                    return (T)form.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            return default(T);
        }

        T IWfPlatformServices.GetService<T>(WfNode forNode) {
            foreach(object svc in RegisteredServices) {
                if(svc is T)
                    return (T)svc;
            }
            return default(T);
        }

        bool IWfPlatformServices.ShouldRecreateForm(IWfPlatformForm form) {
            Form winForm = form as Form;
            return winForm == null || winForm.IsDisposed;
        }
    }
}
