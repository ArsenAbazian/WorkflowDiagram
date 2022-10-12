namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public class PropertyGridRowBase {
        public PropertyGridRowBase(PropertiesDataModel owner) {
            Owner = owner;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public PropertiesDataModel Owner { get; protected set; }
        public virtual string Name { get; set; }
        public virtual List<PropertyGridRowBase> Children { get; protected set; }
        bool expanded;
        public bool Expanded {
            get { return expanded; }
            set {
                if(Expanded == value)
                    return;
                expanded = value;
                OnExpandedChanged();
            }
        }
        protected virtual void OnExpandedChanged() {
            if(Owner != null)
                Owner.OnRowChanged(this);
        }
    }
}
