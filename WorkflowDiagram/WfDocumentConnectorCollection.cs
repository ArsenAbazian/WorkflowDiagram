using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class WfDocumentConnectorCollection : ObservableCollection<WfConnector> {
        public WfDocumentConnectorCollection(WfDocument owner) {
            Document = owner;
        }

        public WfDocument Document { get; private set; }
        protected override void InsertItem(int index, WfConnector item) {
            base.InsertItem(index, item);
            Document.OnConnectorsCollectionChanged();
        }
        protected override void RemoveItem(int index) {
            WfConnector connector = this[index];
            base.RemoveItem(index);
            connector.OnRemoved();
            Document.OnConnectorsCollectionChanged();
        }
        protected override void SetItem(int index, WfConnector item) {
            base.SetItem(index, item);
            Document.OnConnectorsCollectionChanged();
        }
    }
}
