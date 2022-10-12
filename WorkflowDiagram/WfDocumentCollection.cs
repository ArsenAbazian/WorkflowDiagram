using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class WfDocumentCollection : ObservableCollection<WfDocument> {
        public WfDocumentCollection(WfProject owner) {
            Project = owner;
        }

        public WfProject Project { get; private set; }
        protected override void InsertItem(int index, WfDocument item) {
            base.InsertItem(index, item);
            item.Documents = this;
            Project.OnDocumentCollectionChanged();
        }
        protected override void RemoveItem(int index) {
            WfDocument node = this[index];
            this[index].Documents = null;
            base.RemoveItem(index);
            Project.OnDocumentCollectionChanged();
        }
        protected override void SetItem(int index, WfDocument item) {
            this[index].Documents = null;
            item.Documents = this;
            base.SetItem(index, item);
            Project.OnDocumentCollectionChanged();
        }
    }
}
