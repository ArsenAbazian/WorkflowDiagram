using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public interface IWfCommandsProvider {
        List<WfCommand> Commands { get; }
    }

    public abstract class WfCommand {
        public abstract bool Execute(WfNode node);
        public abstract string Caption { get; }
    }

    public class WfCommandsProviderAttribute : Attribute { 
        public WfCommandsProviderAttribute(string namespaceClassName) {
            NamespaceClassName = namespaceClassName;
        }
        public string NamespaceClassName { get; private set; }

        IWfCommandsProvider provider;
        public IWfCommandsProvider Provider {
            get {
                if(provider == null)
                    provider = GetProvider();
                return provider;
            }
        }

        protected virtual IWfCommandsProvider GetProvider() {
            var asms = Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList(); // AppDomain.CurrentDomain.GetAssemblies();
            asms.Add(Assembly.GetEntryAssembly().GetName());
            Dictionary<string, Assembly> processedAssemblies = new Dictionary<string, Assembly>();
            foreach(var aname in asms) {
                try {
                    Assembly assembly = Assembly.Load(aname);
                    if(processedAssemblies.ContainsKey(assembly.GetName().Name))
                        continue;
                    processedAssemblies.Add(assembly.GetName().Name, assembly);
                    var p = GetProvider(assembly);
                    if(p != null)
                        return p;
                }
                catch(Exception) { }
            }
            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                if(processedAssemblies.ContainsKey(assembly.GetName().Name))
                    continue;
                processedAssemblies.Add(assembly.GetName().Name, assembly);
                try {
                    var p = GetProvider(assembly);
                    if(p != null)
                        return p;
                }
                catch(Exception) { }
            }
            return null;
        }

        protected virtual IWfCommandsProvider GetProvider(Assembly assembly) {
            Type type = assembly.GetType(NamespaceClassName);
            if(type != null) 
                return (IWfCommandsProvider)type.GetConstructor(new Type[] { }).Invoke(new object[] { });
            return null;
        }
    }
}
