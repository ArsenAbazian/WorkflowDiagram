using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public abstract class WfDataInfo {
        public WfDataInfo() {
            data = CreateData();
        }

        protected abstract object CreateData();

        public abstract string Name { get; }

        object data;
        public object Data {
            get {
                return data;
            }
        }
    }
}
