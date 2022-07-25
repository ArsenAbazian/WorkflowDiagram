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

namespace WfBaseScript.Editors {
    [UserRepositoryItem("RegisterCustomEdit")]
    public class RepositoryItemExpressionEditor : RepositoryItemButtonEdit, IPropertyEditor {
        public RepositoryItemExpressionEditor() {
            TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        static RepositoryItemExpressionEditor() {
            RegisterCustomEdit();
        }

        //The unique name for the custom editor
        public const string CustomEditName = "ExpressionEditor";

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
              typeof(ExpressionEditor), typeof(RepositoryItemExpressionEditor),
              typeof(ButtonEditViewInfo), new ButtonEditPainter(), true, img, typeof(ButtonEditAccessible)));
        }

        protected internal object Owner { get; set; }
        protected string Property { get; set; }
        protected internal object Value { get; set; }
        void IPropertyEditor.Initialize(object owner, string property, object value) {
            Owner = owner;
            Property = property;
            Value = value;
        }

        public override void Assign(RepositoryItem item) {
            base.Assign(item);

            RepositoryItemExpressionEditor ed = item as RepositoryItemExpressionEditor;
            if(ed == null)
                return;
            Owner = ed.Owner;
            Property = ed.Property;
            Value = ed.Value;
        }
    }


    public class ExpressionEditor : ButtonEdit {
        static ExpressionEditor() { RepositoryItemExpressionEditor.RegisterCustomEdit(); }

        public ExpressionEditor() {

        }

        //Return the unique name
        public override string EditorTypeName {
            get {
                return RepositoryItemExpressionEditor.CustomEditName;
            }
        }

        //Override the Properties property
        //Simply type-cast the object to the custom repository item type
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemExpressionEditor Properties {
            get { return base.Properties as RepositoryItemExpressionEditor; }
        }

        protected override void OnClickButton(EditorButtonObjectInfoArgs buttonInfo) {
            base.OnClickButton(buttonInfo);
            using(ExpressionEditorForm form = new ExpressionEditorForm()) {
                form.Expression = Convert.ToString(EditValue);
                IGlobalsTypeProvider provider = Properties.Owner as IGlobalsTypeProvider;
                form.GlobalsType = provider == null? Properties.Owner.GetType(): provider.GetGlobalsType();
                if(form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                Properties.Value = form.Expression;
                EditValue = form.Expression;
            }
        }
    }

    public interface IGlobalsTypeProvider {
        Type GetGlobalsType();
    }
}
