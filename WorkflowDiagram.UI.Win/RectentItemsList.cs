using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlSerialization;

namespace WorkflowDiagram.UI.Win {
    [Serializable]
    public class RecentItemsList : ISupportSerialization {
        public string ThemeName { get; set; }
        public string PalletteName { get; set; }

        public List<string> Items { get; } = new List<string>();
        public string FileName { get; set; }

        public void AddFile(string file) {
            if(Items.Contains(file))
                return;
            Items.Add(file);
        }
        void ISupportSerialization.OnBeginSerialize() { }
        void ISupportSerialization.OnEndSerialize() { }
        void ISupportSerialization.OnEndDeserialize() {
        }
        void ISupportSerialization.OnBeginDeserialize() {
            Items.Clear();
        }
    }
}
