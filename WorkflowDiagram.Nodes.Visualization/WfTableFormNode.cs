using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WokflowDiagram.Nodes.Visualization.Forms;
using WorkflowDiagram;
using WorkflowDiagram.Nodes.Base;

namespace WokflowDiagram.Nodes.Visualization {
    [WfCommandsProvider("WokflowDiagram.Nodes.Visualisation.Commands.WfTableFormNodeCommandsProvider")]
    public class WfTableFormNode : WfVisualNodeBase {
        public override string VisualTemplateName => "TableForm";

        public override string Type => "TableForm";
        public override string Category => "Visualization";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "DataSource", Text = "DataSource", Requirement = WfRequirementType.Mandatory },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new List<WfConnectionPoint>();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            Progress = new Progress<object>(dataSource => {
                Form.Node = this;
                Form.DataSource = dataSource;
                Form.Show();
            });
            return true;
        }

        TableForm form;
        [XmlIgnore]
        public TableForm Form {
            get {
                if(form == null || form.IsDisposed)
                    form = new TableForm();
                return form;
            }
            set {
                form = value;
            }
        }

        [Browsable(false)]
        public string XmlConfigurationText { get; set; }

        protected IProgress<object> Progress { get; set; }
        protected override void OnVisitCore(WfRunner runner) {
            object dataSource = Inputs["DataSource"].Value;
            Progress.Report(dataSource);
        }
    }

    public class FilteredGridViewProperties {
        public FilteredGridViewProperties(GridView view) {
            View = view;
        }
        protected GridView View {
            get; set;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DXCategory("Appearance")]
        public GridViewAppearances Appearance {
            get { return View.Appearance; }
        }

        [DefaultValue(DrawFocusRectStyle.CellFocus)]
        [DXCategory("Appearance")]
        public DrawFocusRectStyle FocusRectStyle { get { return View.FocusRectStyle; } set { View.FocusRectStyle = value; } }

        FilteredOptionsView optionsView;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DXCategory("Options")]
        public FilteredOptionsView OptionsView { 
            get {
                if(optionsView == null)
                    optionsView = new FilteredOptionsView(View.OptionsView);
                return optionsView; 
            } 
        }

        [DefaultValue(-1)]
        [DXCategory("Appearance")]
        public int RowHeight { get { return View.RowHeight; } set { View.RowHeight = value; } }

        [DefaultValue(0)]
        [DXCategory("Appearance")]
        public virtual int RowSeparatorHeight { get { return View.RowSeparatorHeight; } set { View.RowSeparatorHeight = value; } }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FilteredOptionsBehavior {
        public FilteredOptionsBehavior(GridOptionsBehavior opt) {
            Options = opt;
        }
        protected GridOptionsBehavior Options { get; set; }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FilteredOptionsView {
        public FilteredOptionsView(GridOptionsView opt) {
            Options = opt;
        }
        protected GridOptionsView Options { get; set; }

        [DefaultValue(false)]
        public virtual bool EnableAppearanceEvenRow { get { return Options.EnableAppearanceEvenRow; } set { Options.EnableAppearanceEvenRow = value; } }

        [DefaultValue(false)]
        public virtual bool EnableAppearanceOddRow { get { return Options.EnableAppearanceOddRow; } set { Options.EnableAppearanceOddRow = value; } }

        [DefaultValue(false)]
        public virtual bool RowAutoHeight { get { return Options.RowAutoHeight; } set { Options.RowAutoHeight = value; } }

        [DefaultValue(false)]
        public virtual bool ShowAutoFilterRow { get { return Options.ShowAutoFilterRow; } set { Options.ShowAutoFilterRow = value; } }

        [DefaultValue(true)]
        public virtual bool ShowColumnHeaders { get { return Options.ShowColumnHeaders; } set { Options.ShowColumnHeaders = value; } }

        [DefaultValue(true)]
        public virtual bool ShowGroupedColumns{ get { return Options.ShowGroupedColumns; } set { Options.ShowGroupedColumns = value; } }

        [DefaultValue(true)]
        public virtual bool ShowGroupPanel { get { return Options.ShowGroupPanel; } set { Options.ShowGroupPanel = value; } }

        [DefaultValue(DefaultBoolean.Default)]
        public virtual DefaultBoolean ShowHorizontalLines { get { return Options.ShowHorizontalLines; } set { Options.ShowHorizontalLines = value; } }

        [DefaultValue(DefaultBoolean.Default)]
        public virtual DefaultBoolean ShowVerticalLines { get { return Options.ShowVerticalLines; } set { Options.ShowVerticalLines = value; } }

        [DefaultValue(true)]
        public virtual bool ShowIndicator { get { return Options.ShowIndicator; } set { Options.ShowIndicator = value; } }

    }
}
