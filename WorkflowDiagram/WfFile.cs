using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram {
    public class WfFile {
        public WfFile() { }
        public WfFile(string name) {
            Name = name;
        }

        public string Name { get; set; }
        public string FullName { get; set; }
        public string Extension { get; set; }
        public bool Generated { get; set; }

        string itemId;
        public string ItemId { 
            get { 
                if(itemId == null)
                    itemId = string.Format("file_{0}_{1}.{2}",Name, Guid.NewGuid().ToString().Replace("-", ""), Extension);
                return itemId;
            } 
        }
        public string GeneratedDownloadName { get; set; }
        //[XmlIgnore]
        //public byte[] Data { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}
