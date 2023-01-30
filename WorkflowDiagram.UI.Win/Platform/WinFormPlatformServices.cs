using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkflowDiagram.Nodes.Visualization.Interfaces;

namespace WorkflowDiagram.UI.Win.Platform {
    public class WinFormPlatformServices : IWfPlatformServices {
        protected List<object> RegisteredServices { get; } = new List<object>();
        protected List<Type> RegisteredForms { get; } = new List<Type>();

        public WinFormPlatformServices() {
            RegisteredServices.Add(new WinPlatformChartService());
            RegisteredServices.Add(new WinPlatformGaugeService());
            RegisteredServices.Add(new WinPlatformTableService());
        }

        T IWfPlatformServices.CreateForm<T>() {
            //foreach(Type type in RegisteredForms) {
            //    if(type.FindInterfaces)
            //        return (T)type.GetConstructor(new Type[] { }).Invoke(new object[] { });
            //}
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
