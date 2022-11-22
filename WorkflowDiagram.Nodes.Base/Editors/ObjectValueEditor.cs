using DevExpress.Accessibility;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram.Editors;

namespace WorkflowDiagram.Nodes.Base.Editors {
    [UserRepositoryItem("RegisterCustomEdit")]
    public class RepositoryItemObjectValueEditor : RepositoryItemButtonEdit, IPropertyEditor {
        public RepositoryItemObjectValueEditor() {
        }

        static RepositoryItemObjectValueEditor() {
            RegisterCustomEdit();
        }

        //The unique name for the custom editor
        public const string CustomEditName = "ObjectValueEditor";

        //Return the unique name
        public override string EditorTypeName { get { return CustomEditName; } }

        //Register the editor
        public static void RegisterCustomEdit() {
            //Icon representing the editor within a container editor's Designer
            Image img = null;
            try {
                img = new Bitmap(16, 16);
            }
            catch {
            }
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName,
              typeof(ObjectValueEditor), typeof(RepositoryItemObjectValueEditor),
              typeof(ButtonEditViewInfo), new ButtonEditPainter(), true, img, typeof(ButtonEditAccessible)));
        }

        protected object Owner { get; set; }
        protected string Property { get; set; }
        protected internal object Value { get; set; }
        void IPropertyEditor.Initialize(object owner, string property, object value) {
            Owner = owner;
            Property = property;
            Value = value;
        }

        public override void Assign(RepositoryItem item) {
            base.Assign(item);

            RepositoryItemObjectValueEditor ed = item as RepositoryItemObjectValueEditor;
            if(ed == null)
                return;
            Owner = ed.Owner;
            Property = ed.Property;
            Value = ed.Value;
        }
    }

    
    public class ObjectValueEditor : ButtonEdit {
        static ObjectValueEditor() { RepositoryItemObjectValueEditor.RegisterCustomEdit(); }

        public ObjectValueEditor() {
        
        }

        //Return the unique name
        public override string EditorTypeName {
            get {
                return RepositoryItemObjectValueEditor.CustomEditName;
            }
        }

        //Override the Properties property
        //Simply type-cast the object to the custom repository item type
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemObjectValueEditor Properties {
            get { return base.Properties as RepositoryItemObjectValueEditor; }
        }

        protected override void OnClickButton(EditorButtonObjectInfoArgs buttonInfo) {
            base.OnClickButton(buttonInfo);
            using(ObjectValueEditForm form = new ObjectValueEditForm()) {
                form.Value = EditValue;
                if(form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                Properties.Value = form.Value;
                EditValue = form.Value;
            }
        }
    }
}
