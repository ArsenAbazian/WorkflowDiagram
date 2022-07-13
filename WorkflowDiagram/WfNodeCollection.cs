using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class WfNodeCollection : ObservableCollection<WfNode> {
        public WfNodeCollection(WfDocument owner) {
            Document = owner;
        }

        public WfDocument Document { get; private set; }
        protected override void InsertItem(int index, WfNode item) {
            base.InsertItem(index, item);
            item.OwnerCollection = this;
        }
        protected override void RemoveItem(int index) {
            WfNode node = this[index];
            this[index].OwnerCollection = null;
            base.RemoveItem(index);
            node.OnRemoved();
        }
        protected override void SetItem(int index, WfNode item) {
            this[index].OwnerCollection = null;
            item.OwnerCollection = this;
            base.SetItem(index, item);
        }
    }
}
