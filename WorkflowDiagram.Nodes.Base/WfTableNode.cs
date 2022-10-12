using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfTableNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Table";

        public override string Type => "Data Table";
        public override string Category => "Data";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Rows", Text = "Rows", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Table", Text = "Table", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected DataTable Table { get; set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            Table = new DataTable();
            return true;
        }

        protected virtual void AddRow(object obj) {
            Dictionary<string, object> drow = obj as Dictionary<string, object>;
            DataRow trow = obj as DataRow;
            if(drow != null) {
                object[] values = drow.Values.ToArray();
                Table.Rows.Add(values);
            }
            else {
                Table.Rows.Add(trow.ItemArray);
            }
        }

        protected virtual void TryAddRowsToTable(IEnumerable en) {
            foreach(var obj in en) {
                AddRow(obj);
            }
        }

        protected virtual void CheckAddColumns(object obj) {
            if(Table.Columns.Count > 0)
                return;
            Dictionary<string, object> drow = obj as Dictionary<string, object>;
            DataRow trow = obj as DataRow;
            if(drow != null)
                CheckAddColumns(drow);
            else
                CheckAddColumns(trow);
        }
        protected virtual void CheckAddColumns(IEnumerable en) {
            if(Table.Columns.Count > 0)
                return;
            foreach(var obj in en) {
                CheckAddColumns(obj);
                break;
            }
        }

        void CheckAddColumns(Dictionary<string, object> row) {
            foreach(string key in row.Keys) {
                Table.Columns.Add(new DataColumn(key) { Caption = key });
            }
        }

        void CheckAddColumns(DataRow row) {
            foreach(DataColumn column in row.Table.Columns) {
                Table.Columns.Add(new DataColumn(column.ColumnName) { Caption = column.ColumnName, DataType = column.DataType});
            }
        }

        protected override void OnVisitCore(WfRunner runner) {
            DataContext = Table;
            object value = Inputs["Rows"].Value;
            IEnumerable en = value as IEnumerable;
            if(Inputs["Rows"].Value != null && en == null)
                Diagnostic.Add(new WfDiagnosticInfo() { Type = WfDiagnosticSeverity.Warning, Text = "Value in Rows Input cannot be used as a source for table" });
            if(en != null) {
                CheckAddColumns(en);
                TryAddRowsToTable(en);
            }
            else { 
                CheckAddColumns(value);
                AddRow(value);
            }
            Outputs["Table"].Visit(runner, Table);
        }
    }
}
