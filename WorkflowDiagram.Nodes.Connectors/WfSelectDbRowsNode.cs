using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram.Nodes.Base;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfSelectDbRowsNode : WfVisualNodeBase, IColumnRefOwner {
        public WfSelectDbRowsNode() {
             Columns = new ColumnRefCollection(this);
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

        public string Query { get; set; }
        public ColumnRefCollection Columns { get; }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            WfDatabaseConnectorNode database = Inputs["Database"].Value as WfDatabaseConnectorNode;
            if(database == null || database.Provider == null) {
                Outputs["Result"].SkipVisit(runner, null);
                return;
            }
            string actualQuery = GetActualQuery();
            DataTable dataTable = database.Provider.Select(actualQuery);
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
            b.Append(';');

            return b.ToString();
        }

        void IColumnRefOwner.OnCollectionChanged() {
            
        }
    }
}
