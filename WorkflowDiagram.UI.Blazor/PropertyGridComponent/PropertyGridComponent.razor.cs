using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Drawing;
using WorkflowDiagram.UI.Blazor.Helpers;
using WorkflowDiagram.UI.Blazor.PropertyGridComponent.RowViews;
using WorkflowDiagram.UI.Blazor.PropertyGridComponent.ValueCellViews;

namespace WorkflowDiagram.UI.Blazor.PropertyGridComponent {
    public partial class PropertyGridComponent : IDisposable {
        private bool disposedValue;

        public PropertyGridComponent() {

        }

        protected virtual void Dispose(bool disposing) {
            if(!disposedValue) {
                if(disposing) {
                    // TODO: dispose managed state (managed objects)


                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        void IDisposable.Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        object[] selectedObjects;
        public object[] SelectedObjects {
            get { return selectedObjects; }
            set {
                if(SelectedObjects == value)
                    return;
                selectedObjects = value;
                OnSelectedObjectsChanged();
            }
        }

        public object SelectedObject {
            get { return SelectedObjects == null || SelectedObjects.Length == 0 ? null : SelectedObjects[0]; }
            set {
                if(SelectedObject == value)
                    return;
                if(value == null)
                    SelectedObjects = new object[0];
                else
                    SelectedObjects = new object[] { value };
            }
        }

        protected internal virtual Type CreateViewTypeFor(PgValueItem pgValueItem) {
            if(pgValueItem.Value.GetCustomEditorType() != null)
                return typeof(PgCustomValueView);
            Type propType = pgValueItem.Value.Row.Property.PropertyType;
            if(propType == typeof(bool))
                return typeof(PgBooleanValueView);
            if(propType.IsEnum)
                return typeof(PgEnumValueView);
            if(propType == typeof(string))
                return typeof(PgTextValueView);
            if(propType == typeof(float) || propType == typeof(double) || propType == typeof(int))
                return typeof(PgNumericValueView);
            if(propType == typeof(DateTime))
                return typeof(PgDateValueView);
            if(propType == typeof(Guid))
                return typeof(PgTextValueView);

            return null;
        }

        protected virtual void OnSelectedObjectsChanged() {
            BuildModel();
            RaisePropertyGridChanged();
        }

        protected virtual void BuildModel() {
            DataModel = new PropertiesDataModel(SelectedObjects);
            Rows = DataModel.GetPlainRows();
        }

        public event EventHandler Changed;
        protected virtual void RaisePropertyGridChanged() {
            if(Changed != null)
                Changed(this, EventArgs.Empty);
        }

        PropertiesDataModel dataModel;
        protected PropertiesDataModel DataModel {
            get { return dataModel; }
            set {
                if(DataModel == value)
                    return;
                OnDataModelChangedCore(DataModel, dataModel = value);
            }
        }
        protected virtual void OnDataModelChangedCore(PropertiesDataModel prev, PropertiesDataModel current) {
            if(prev != null)
                prev.Changed -= OnDataModelChanged;
            if(current != null)
                current.Changed += OnDataModelChanged;
        }

        protected virtual void OnDataModelChanged(object sender, EventArgs e) {
            Rows = DataModel.GetPlainRows();
            RaisePropertyGridChanged();
        }

        public List<PropertyGridRowBase> Rows { get; protected set; }

        protected internal virtual Type CreateViewTypeFor(PgRowItem item) {
            if(item.Row is PropertyGridObjectValueRow) {
                PropertyGridObjectValueRow row = (PropertyGridObjectValueRow)item.Row;
                if(row.GetCustomEditorType() != null)
                    return typeof(PgValueRowView);
                return typeof(PgObjectRowView);
            }
            else if(item.Row is PropertyGridValueRow)
                return typeof(PgValueRowView);
            else if(item.Row is PropertyGridCategoryRow)
                return typeof(PgCategoryRowView);
            return null;
        }
    }
}
