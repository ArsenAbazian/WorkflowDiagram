using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WorkflowDiagram.UI.Blazor.ServiceModel {

    public interface ISupportId {
        int Oid { get; set; }
    }

    public interface ISupportOwner {
        int OwnerId { get; }
    }

    public interface ITokenOwner {
        int Oid { get; set; }
        string Token { get; set; }
    }

    public class AppSettings {
        public string Secret { get; set; }
    }
}
