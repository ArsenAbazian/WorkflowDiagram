using DevExpress.Accessibility;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WokflowDiagram.Nodes.Visualization;

namespace WorkflowDiagram.UI.Win {
    public class RepositoryItemWfColorEdit : RepositoryItemColorPickEdit {
        static RepositoryItemWfColorEdit() { RegisterCustomEdit(); }

        public const string CustomEditName = "WfColorEdit";

        //Return the unique name
        public override string EditorTypeName { get { return CustomEditName; } }

        //Register the editor
        public static void RegisterCustomEdit() {
            //Icon representing the editor within a container editor's Designer
            Image img = null;
            try {
                img = (Bitmap)Bitmap.FromStream(Assembly.GetExecutingAssembly().
                  GetManifestResourceStream("DevExpress.CustomEditors.CustomEdit.bmp"));
            }
            catch {
            }
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName,
              typeof(WfColorEdit), typeof(RepositoryItemWfColorEdit),
              typeof(ColorEditViewInfo), new ColorEditPainter(), true, img, typeof(PopupEditAccessible)));
        }
        protected override Color ConvertToColor(object editValue) {
            if(editValue is WfColor) {
                WfColor color = (WfColor)editValue;
                return color.ToColor();
            }
            return base.ConvertToColor(editValue);
        }
        protected override object ConvertToEditValue(object val) {
            if(val is Color) {
                Color color = (Color)val;
                return color.ToWfColor();
            }
            return base.ConvertToEditValue(val);
        }
    }
    public class WfColorEdit : ColorPickEdit {

        static WfColorEdit() { RepositoryItemWfColorEdit.RegisterCustomEdit(); }

        public WfColorEdit() {
        }

        //Return the unique name
        public override string EditorTypeName {
            get { return RepositoryItemWfColorEdit.CustomEditName; }
        }
    }
}
