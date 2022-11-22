using System.Collections.ObjectModel;
using WorkflowDiagram.UI.Blazor.DiagramComponents;

namespace WorkflowDiagram.UI.Blazor.Helpers {
    public class WfSelectedItemsCollection : Collection<IDiagramItem> {
        public WfSelectedItemsCollection(WfDiagramComponent owner) { 
            Owner = owner;
        }

        public WfDiagramComponent Owner { get; private set; }
        protected override void InsertItem(int index, IDiagramItem item) {
            base.InsertItem(index, item);
            item.Selected = true;
            Owner.OnSelectedItemsChanged();
        }
        protected override void ClearItems() {
            List<IDiagramItem> toClear = this.ToList();
            base.ClearItems();
            foreach(IDiagramItem item in toClear)
                item.Selected = false;
            Owner.OnSelectedItemsChanged();
        }
        protected override void RemoveItem(int index) {
            IDiagramItem item = this[index];
            base.RemoveItem(index);
            item.Selected = false;
            Owner.OnSelectedItemsChanged();
        }
    }
}
