using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WorkflowDiagram.Nodes.Base {
    public class WfGroupNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Group";

        public override string Type => "Group";
        public override string Category => "Data";

        protected override void OnVisitCore(WfRunner runner) {
            Dictionary<string, object> row = new Dictionary<string, object>();
            foreach(var point in Inputs) {
                row.Add(point.Name, point.Value);
            }
            DataContext = row;
            Outputs["Group"].Visit(runner, row);
        }

        protected override WfConnectionPointCollection CreateInputCollection() {
            WfConnectionPointCollection res = base.CreateInputCollection();
            res.AllowedOperations |= WfEditOperation.Add | WfEditOperation.Edit | WfEditOperation.Remove;
            return res;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Item0", Text = "Item0", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Group", Text = "Group", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            for(int i = 0; i < Inputs.Count; i++) {
                for(int j = i + 1; j < Inputs.Count; j++) {
                    if(Inputs[i].Name == Inputs[j].Name) {
                        DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "Dublicate names detected. " + Inputs[i].Name);
                        return false;
                    }
                }
            }
            return true;
        }
        public override WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type) {
            if(type == WfConnectionPointType.Out)
                return base.CreateConnectionPoint(type);
            int index = Inputs.Count - 1;
            return new WfConnectionPoint() { Name = "Item" + index, Text = "Item" + index, Requirement = WfRequirementType.Optional,  AllowedOperations = WfEditOperation.Add | WfEditOperation.Edit | WfEditOperation.Remove };
        }
    }

    public class WfTableRowNode : WfGroupNode {
        public override string Type => "TableRow";
        public override string VisualTemplateName => "TableRow";

        protected virtual string TableConnectionPointName { get { return "TableIn"; } }
        protected override void OnVisitCore(WfRunner runner) {
            DataTable table = (DataTable)Inputs["TableIn"].Value;
            if(table != null)
                Table = table;
            object[] items = GetFieldPoints().Select(f => f.Value).ToArray();
            CheckInitializeColumns(items);
            DataRow row = Table.Rows.Add(items);
            DataContext = row;
            Outputs["Row"].Visit(runner, row);
            Outputs["TableOut"].Visit(runner, Table);
        }

        protected override WfConnectionPointCollection CreateInputCollection() {
            WfConnectionPointCollection res = base.CreateInputCollection();
            res.AllowedOperations |= WfEditOperation.Add | WfEditOperation.Edit | WfEditOperation.Remove;
            return res;
        }

        public override WfConnectionPoint CreateConnectionPoint(WfConnectionPointType type) {
            if(type == WfConnectionPointType.In) {
                int index = Inputs.Count - 1;
                return new WfConnectionPoint() { Name = "Field" + index, Text = "Field" + index, Requirement = WfRequirementType.Optional, Type = type, AllowedOperations = WfEditOperation.Add | WfEditOperation.Edit | WfEditOperation.Remove };
            }
            return base.CreateConnectionPoint(type);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "TableIn", Text = "Table", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Field0", Text = "Field0", Requirement = WfRequirementType.Optional, AllowedOperations = WfEditOperation.Edit },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Row", Text = "Row", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "TableOut", Text = "Table", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected virtual void CheckInitializeColumns(object[] items) {
            if(Table.Columns.Count != 0)
                return;
            List<WfConnectionPoint> fields = GetFieldPoints();
            for(int i = 0; i < fields.Count; i++) { 
                Table.Columns.Add(new DataColumn(fields[i].Name) { DataType = items[i].GetType() });
            }
        }

        protected List<WfConnectionPoint> GetFieldPoints() {
            List<WfConnectionPoint> list = new List<WfConnectionPoint>();
            foreach(var point in Inputs) {
                if(point.Name == RunConnectionPointName || point.Name == TableConnectionPointName)
                    continue;
                list.Add(point);
            }
            return list;
        }
        protected DataTable Table { get; set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            if(!base.OnInitializeCore(runner))
                return false;
            Table = new DataTable();
            return true;
        }
    }
}
