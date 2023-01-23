using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram.Nodes.Base;
using WorkflowDiagram.Nodes.Connectors.Helpers;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfDatabaseTableNode : WfVisualNodeBase, IWfColumnsOwner {
        public override string Category => "Database";

        public override string VisualTemplateName => "DatabaseTable";

        public override string Type => "Db Table";

        public WfDatabaseTableNode() {
            Columns = new WfDataTableColumnInfoCollection(this);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            var res = new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Database", Text = "Database", Requirement = WfRequirementType.Optional },
            }.ToList();
            return res;
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            var res = new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Table", Text = "Table", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Name = "Failed", Text = "Failed", Requirement = WfRequirementType.Optional },
            }.ToList();
            return res;
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        [Browsable(false)]
        [XmlIgnore]
        public WfDatabaseConnectionProvider Provider { get; private set; }
        protected override void OnVisitCore(WfRunner runner) {
            Provider = Inputs["Database"].Value as WfDatabaseConnectionProvider;
            if(Provider == null) {
                Outputs["Table"].SkipVisit(runner, this);
                return;
            }
            if(string.IsNullOrEmpty(Table)) {
                Outputs["Table"].SkipVisit(runner, this);
                Outputs["Failed"].Visit(runner, null);
                OnError("Table name is not specified");
                return;
            }
            if(!Provider.TableExist(this, Table)) {
                if(!CreateIfNotExist) {
                    OnError("There is no table with name " + Name + "in database " + Provider.Connection.Database);
                    Outputs["Table"].SkipVisit(runner, this);
                    Outputs["Failed"].Visit(runner, null);
                    return;
                }

                if(!Provider.CreateTable(this, Table, Columns)) {
                    OnError("Cannot create table with name " + Name + "in database " + Provider.Connection.Database);
                    Outputs["Table"].SkipVisit(runner, this);
                    Outputs["Failed"].Visit(runner, null);
                    return;
                }
            }
            if(AllowUpdateTableSchema && !Provider.CheckUpdateTableSchema(this,Table, Columns)) {
                OnError("Cannot check schema for table with name " + Name + "in database " + Provider.Connection.Database);
                Outputs["Table"].SkipVisit(runner, this);
                Outputs["Failed"].Visit(runner, null);
                return;
            }
            ActualColumns = Provider.GetTableInfo(this,Table);
            Outputs["Table"].Visit(runner, this);
            Outputs["Failed"].SkipVisit(runner, null);
        }

        void IWfColumnsOwner.OnColumnInfoChanged(WfDataTableColumnInfo info) {
            //ResetPoints();
        }

        void IWfColumnsOwner.OnColumnInfoAdded(WfDataTableColumnInfo info) {
            if(IsRestoringFromXml())
                return;
            //ResetPoints();
        }

        void IWfColumnsOwner.OnColumnInfoRemoved(WfDataTableColumnInfo info) {
            //ResetPoints();
        }

        public WfDataTableColumnInfoCollection Columns { get; }
        [XmlIgnore]
        public List<WfDataTableColumnInfo> ActualColumns { get; private set; }

        public bool CreateIfNotExist { get; set; }
        public string Table { get; set; }
        public bool AllowUpdateTableSchema { get; set; }
    }
}
