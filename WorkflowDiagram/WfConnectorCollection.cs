using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram {
    public class WfConnectorCollection : ObservableCollection<WfConnector> {
        public WfConnectorCollection(WfConnectionPoint owner) {
            Point = owner;
        }

        public WfDocument Document { get; private set; }
        public WfConnectionPoint Point { get; private set; }
        protected override void InsertItem(int index, WfConnector item) {
            if(Contains(item))
                return;
            base.InsertItem(index, item);
            UpdatePoint(item);
        }
        protected void UpdatePoint(WfConnector item) {
            if(Point.Type == WfConnectionPointType.Out)
                item.From = Point;
            else
                item.To = Point;
        }

        public void MoveUp(WfConnector sel) {
            int index = IndexOf(sel);
            if(index == 0)
                return;
            Move(index, index - 1);
        }

        protected override void RemoveItem(int index) {
            WfConnector item = this[index];
            base.RemoveItem(index);
            item.OnRemoved();
        }

        public void MoveDown(WfConnector sel) {
            int index = IndexOf(sel);
            if(index == Count - 1)
                return;
            Move(index, index + 1);
        }

        protected override void SetItem(int index, WfConnector item) {
            if(Contains(item))
                return;
            this[index].OnRemoved();
            base.SetItem(index, item);
            UpdatePoint(item);
        }
    }
}
