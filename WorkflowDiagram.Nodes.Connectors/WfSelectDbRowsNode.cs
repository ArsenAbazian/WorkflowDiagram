using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram.Nodes.Base;
using WorkflowDiagram.Nodes.Connectors.Helpers;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfSelectDbRowsNode : WfVisualNodeBase, IColumnRefOwner {
        public WfSelectDbRowsNode() {
            Columns = new ColumnRefCollection(this);
            SortColumns = new List<ColumnSortOrderInfo>();
        }

        public override string VisualTemplateName => "SelectDbRowsNode";

        public override string Type => "SelectDbRows";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            var res = new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Database", Text = "Database", Requirement = WfRequirementType.Mandatory },
                new WfConnectionPoint() { Name = "Table", Text = "Table", Requirement = WfRequirementType.Optional }
            }.ToList();
            return res;
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            var res = new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Result", Text = "Result", Requirement = WfRequirementType.Optional },
            }.ToList();
            return res;
        }

        [WinPropertyEditor("WorkflowDiagram.UI.Win.Editors", "WorkflowDiagram.UI.Win.Editors.RepositoryItemExpressionEditor"),
            BlazorPropertyEditor("WorkflowDiagram.UI.Blazor", "WorkflowDiagram.UI.Blazor.NodeEditors.MultilineEditor")]
        public string Query { get; set; }
        public ColumnRefCollection Columns { get; }
        public List<ColumnSortOrderInfo> SortColumns { get; }

        [XmlIgnore]
        public List<string> ResultColumns { get; private set; }

        protected override bool OnInitializeCore(WfRunner runner) {
            ResultColumns = new List<string>();
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            var provider = Inputs["Database"].Value as WfDatabaseConnectionProvider;
            if(provider == null) {
                Outputs["Result"].SkipVisit(runner, null);
                return;
            }
            string actualQuery = GetActualQuery();
            DataTable dataTable = provider.Select(this, actualQuery);
            if(dataTable != null) {
                foreach(DataColumn column in dataTable.Columns)
                    ResultColumns.Add(column.ColumnName);
            }
            Outputs["Result"].Visit(runner, dataTable);
        }

        protected virtual string GetActualQuery() {
            if(!string.IsNullOrEmpty(Query))
                return Query;
            WfDatabaseTableNode table = Inputs["Table"].Value as WfDatabaseTableNode;
            if(table == null)
                return String.Empty;

            StringBuilder b = new StringBuilder();
            b.Append("SELECT ");
            if(Columns.Count == 0)
                b.Append("* ");
            else {
                for(int i = 0; i < Columns.Count; i++) {
                    b.Append(Columns[i].LowCaseName);
                    if(i < Columns.Count - 1)
                        b.Append(", ");
                }
            }
            b.Append(" FROM ");
            b.Append(table.Table);
            
            if(SortColumns.Count > 0) {
                b.Append(" ORDER BY ");
                bool firstItem = true;
                for(int i = 0; i < SortColumns.Count; i++) {
                    if(SortColumns[i].Mode != ColumnSortDirection.None) {
                        if(!firstItem)
                            b.Append(", ");
                        firstItem = false;
                        b.Append(SortColumns[i].ColumnName);
                        b.Append(' ');
                        b.Append(SortColumns[i].Mode == ColumnSortDirection.Ascending ? "ASC" : "DESC");
                    }
                }
            }
            
            b.Append(';');

            return b.ToString();
        }

        void IColumnRefOwner.OnCollectionChanged() {
            
        }
    }
}
