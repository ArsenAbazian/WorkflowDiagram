using System.ComponentModel;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public class PropertiesDataModel {
        public PropertiesDataModel(object[] objects) {
            Objects = objects;
            BuildModel();
        }

        public object[] Objects { get; private set; }
        public List<PropertyGridRowBase> Rows { get; protected set; }
        protected int UpdateLock { get; set; }
        public bool IsUpdateLocked { get { return UpdateLock > 0; } }
        public void BeginUpdate() {
            UpdateLock++;
        }
        public void EndUpdate() {
            if(UpdateLock == 0)
                return;
            UpdateLock--;
            if(UpdateLock == 0)
                Changed?.Invoke(this, EventArgs.Empty);
        }

        protected internal virtual void OnRowChanged(PropertyGridRowBase row) {
            if(IsUpdateLocked)
                return;
            Changed?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void BuildModel() {
            if(Objects == null || Objects.Length == 0) {
                Rows = new List<PropertyGridRowBase>();
                return;
            }
            Level = 0;
            BeginUpdate();
            try {
                Rows = GetRowsFor(Objects);
            }
            finally {
                EndUpdate();
            }
        }

        protected int MaxLevel { get { return 5; } }
        protected int Level { get; set; } = 0;
        protected internal virtual List<PropertyGridRowBase> GetRowsFor(object[] objects) {
            if(Level >= MaxLevel)
                return new List<PropertyGridRowBase>();
            Level++;
            try {
                List<PropertyGridRowBase> res = new List<PropertyGridRowBase>();
                List<PropertyDescriptorCollection> properties = objects.Select(o => TypeDescriptor.GetProperties(o)).ToList();
                List<PropertyDescriptor> filtered = FilterProperties(properties);
                var groups = filtered.GroupBy(f => f.Category).OrderBy(g => g.Key).ToList();
                foreach(var group in groups) {
                    var row = new PropertyGridCategoryRow(this, objects, group.Key, group.ToArray()) { Expanded = true };
                    if(row.Children.Count == 0)
                        continue;
                    res.Add(row);
                }
                return res;
            }
            finally {
                Level--;
            }
        }

        protected internal virtual PropertyGridRowBase CreateRow(object[] objects, PropertyDescriptor prop) {
            if(prop.PropertyType.IsValueType || prop.PropertyType == typeof(string) || prop.PropertyType.IsArray)
                return new PropertyGridValueRow(this, objects, prop);
            if(prop.PropertyType.GetInterface("IList") != null)
                return new PropertyGridValueRow(this, objects, prop);
            return new PropertyGridObjectValueRow(this, objects, prop);
        }

        protected virtual List<PropertyDescriptor> FilterProperties(List<PropertyDescriptorCollection> properties) {
            List<PropertyDescriptor> res = new List<PropertyDescriptor>();
            for(int i = 0; i < properties[0].Count; i++) {
                PropertyDescriptor pd = properties[0][i];
                if(!pd.IsBrowsable)
                    continue;
                bool found = true;
                for(int j = 1; j < properties.Count; j++) {
                    PropertyDescriptor pd2 = properties[j].Find(pd.Name, false);
                    if(pd2 == null || pd2.PropertyType != pd.PropertyType) {
                        found = false;
                        break;
                    }
                }
                if(found)
                    res.Add(pd);
            }
            return res;
        }

        public event EventHandler Changed;

        public List<PropertyGridRowBase> GetPlainRows() {
            List<PropertyGridRowBase> res = new List<PropertyGridRowBase>();
            GetPlainRows(res, Rows);
            return res;
        }

        protected void GetPlainRows(List<PropertyGridRowBase> dest, List<PropertyGridRowBase> src) {
            foreach(var row in src) {
                dest.Add(row);
                if(row.Expanded)
                    GetPlainRows(dest, row.Children);
            }
        }
    }

}
