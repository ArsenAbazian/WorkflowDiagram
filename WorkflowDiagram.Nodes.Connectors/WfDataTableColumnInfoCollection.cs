using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfDataTableColumnInfoCollection : ObservableCollection<WfDataTableColumnInfo> {
        public WfDataTableColumnInfoCollection(IWfColumnsOwner owner) {
            Owner = owner;
        }

        public IWfColumnsOwner Owner { get; private set; }
        protected override void InsertItem(int index, WfDataTableColumnInfo item) {
            WfDataTableColumnInfo prev = this.FirstOrDefault(p => p.Name == item.Name);
            if(prev != null)
                return;
            base.InsertItem(index, item);
            item.Collection = this;
            Owner.OnColumnInfoAdded(item);
        }
        protected override void RemoveItem(int index) {
            var info = this[index];
            this[index].Collection = null;
            base.RemoveItem(index);
            info.Collection = null;
            Owner.OnColumnInfoRemoved(info);
        }
        protected override void SetItem(int index, WfDataTableColumnInfo item) {
            var info = this[index];
            info.Collection = null;
            Owner.OnColumnInfoRemoved(info);
            item.Collection = this;
            base.SetItem(index, item);
            Owner.OnColumnInfoAdded(item);
        }
    }

    public interface IWfColumnsOwner {
        void OnColumnInfoChanged(WfDataTableColumnInfo info);
        void OnColumnInfoAdded(WfDataTableColumnInfo info);
        void OnColumnInfoRemoved(WfDataTableColumnInfo info);
    }
}
