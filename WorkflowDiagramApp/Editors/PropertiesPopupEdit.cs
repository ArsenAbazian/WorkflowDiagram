using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Popup;
using System.Windows.Forms;
using DevExpress.Utils;
using WorkflowDiagram.UI.Win;

namespace WorkflowDiagramApp.Editors {
    [UserRepositoryItem("RegisterPropertiesPopupEdit")]
    public class RepositoryItemPropertiesPopupEdit : RepositoryItemPopupBase, IPropertyEditor {
        static RepositoryItemPropertiesPopupEdit() {
            RegisterPropertiesPopupEdit();
        }

        public const string CustomEditName = "PropertiesPopupEdit";

        public RepositoryItemPropertiesPopupEdit() {
        }

        public override string EditorTypeName => CustomEditName;

        public static void RegisterPropertiesPopupEdit() {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(PropertiesPopupEdit), typeof(RepositoryItemPropertiesPopupEdit), typeof(PropertiesPopupEditViewInfo), new PropertiesPopupEditPainter(), true, img));
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

            RepositoryItemPropertiesPopupEdit ed = item as RepositoryItemPropertiesPopupEdit;
            if(ed == null)
                return;
            Owner = ed.Owner;
            Property = ed.Property;
            Value = ed.Value;
        }
    }

    [ToolboxItem(true)]
    public class PropertiesPopupEdit : PopupBaseEdit {
        static PropertiesPopupEdit() {
            RepositoryItemPropertiesPopupEdit.RegisterPropertiesPopupEdit();
        }

        public PropertiesPopupEdit() {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemPropertiesPopupEdit Properties => base.Properties as RepositoryItemPropertiesPopupEdit;

        public override string EditorTypeName => RepositoryItemPropertiesPopupEdit.CustomEditName;

        protected override PopupBaseForm CreatePopupForm() {
            return new PropertiesPopupEditPopupForm(this);
        }
        protected override bool CanShowPopup => base.CanShowPopup && Properties.Owner != null;
    }

    public class PropertiesPopupEditViewInfo : PopupBaseEditViewInfo {
        public PropertiesPopupEditViewInfo(RepositoryItem item) : base(item) {
        }
    }

    public class PropertiesPopupEditPainter : ButtonEditPainter {
        public PropertiesPopupEditPainter() {
        }
    }

    public class PropertiesPopupEditPopupForm : CustomBlobPopupForm {
        public PropertiesPopupEditPopupForm(PropertiesPopupEdit ownerEdit) : base(ownerEdit) {
            Control = CreateContainerControl();
            this.Controls.Add(Control);
            AllowSizing = true;
            SizeGripStyle = SizeGripStyle.Show;
        }
        protected PropertyPathControl Control { get; private set; }

        private PropertyPathControl CreateContainerControl() {
            PropertyPathControl res = new PropertyPathControl() { Dock = DockStyle.Fill };
            res.UserValueSelected += (d, e) => ClosePopup(PopupCloseMode.Normal);
            return res;
        }

        protected override Size CalcFormSizeCore() {
            Size res = base.CalcFormSizeCore();
            res.Width = Math.Max(OwnerEdit.Width, Math.Max(res.Width, ScaleUtils.ScaleValue(200)));
            res.Height = Math.Max(res.Height, ScaleUtils.ScaleValue(200));
            return res;
        }

        protected override void OnBeforeShowPopup() {
            Control.Context = ((RepositoryItemPropertiesPopupEdit)OwnerEdit.Properties).Owner.GetType();
            Control.PropertyPath = OwnerEdit.Text;
            base.OnBeforeShowPopup();
        }

        public override object ResultValue => Control.PropertyPath;
    }
}
