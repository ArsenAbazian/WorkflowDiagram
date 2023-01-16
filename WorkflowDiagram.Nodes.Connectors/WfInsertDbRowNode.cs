using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram.Nodes.Base;
using WorkflowDiagram.Nodes.Connectors.Helpers;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfInsertDbRowNode : WfVisualNodeBase, IColumnRefOwner {
        public override string Category => "Database";

        public override string VisualTemplateName => "InsertDbRow";

        public override string Type => "Insert DbRow";

        public WfInsertDbRowNode() {
            Columns = new ColumnRefCollection(this);
        }

        void IColumnRefOwner.OnCollectionChanged() {
            if(IsRestoringFromXml())
                return;
            ResetPoints();
        }

        public ColumnRefCollection Columns { get; }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            var res = new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Table", Text = "Table", Requirement = WfRequirementType.Optional }
            }.ToList();

            for(int i = 0; i < Columns.Count; i++) {
                WfConnectionPoint pt = PrevInputs?.FirstOrDefault(p => p.Name == Columns[i].Name);
                if(pt == null)
                    pt = new WfConnectionPoint() { Name = Columns[i].Name, Text = Columns[i].Name, Requirement = WfRequirementType.Optional };
                Columns[i].Point = pt;
                res.Add(pt);
            }
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
            ColumnsInitialized = false;
            return true;
        }

        protected bool ColumnsInitialized { get; set; }
        protected override void OnVisitCore(WfRunner runner) {
            WfDatabaseTableNode table = Inputs["Table"].Value as WfDatabaseTableNode;
            CheckInitializeColumns(table);
            if(table == null || table.Provider == null) {
                Outputs["Table"].SkipVisit(runner, null);
                Outputs["Failed"].Visit(runner, null);
                return;
            }
            bool completed = table.Provider.Insert(table.Table, Columns);
            if(!completed) {
                Outputs["Table"].SkipVisit(runner, null);
                Outputs["Failed"].Visit(runner, null);
                return;
            }
            else {
                Outputs["Table"].Visit(runner, table);
                Outputs["Failed"].SkipVisit(runner, null);
            }
        }

        protected virtual void CheckInitializeColumns(WfDatabaseTableNode table) {
            if(ColumnsInitialized || table == null)
                return;
            for(int i = 0; i < Columns.Count; i++) {
                if(Columns[i].ColumnInfo == null)
                    Columns[i].ColumnInfo = table.ActualColumns.FirstOrDefault(c => c.LowCaseName == Columns[i].LowCaseName);
                if(Columns[i].Point == null)
                    Columns[i].Point = Inputs.FirstOrDefault(p => p.LowCaseName == Columns[i].LowCaseName);
            }
            ColumnsInitialized = true;
        }
    }

    public class ColumnName {
        public string Name { get; set; }
        public override string ToString() {
            return string.Format("[{0}]", Name);
        }
        [Browsable(false)]
        [XmlIgnore]
        public WfConnectionPoint Point { get; set; }
        [Browsable(false)]
        [XmlIgnore]
        public WfDataTableColumnInfo ColumnInfo { get; set; }

        string lowCaseName;
        [Browsable(false)]
        public string LowCaseName {
            get {
                if(lowCaseName == null && Name != null)
                    lowCaseName = Name.ToLower();
                return lowCaseName;
            }
        }
    }

    public interface IColumnRefOwner {
        void OnCollectionChanged();
    }

    public class ColumnRefCollection : ObservableCollection<ColumnName> {
        public ColumnRefCollection(IColumnRefOwner owner) {
            Owner = owner;
        }

        public IColumnRefOwner Owner { get; set; }
        protected override void InsertItem(int index, ColumnName item) {
            base.InsertItem(index, item);
            Owner?.OnCollectionChanged();
        }
        protected override void RemoveItem(int index) {
            base.RemoveItem(index);
            Owner?.OnCollectionChanged();
        }
        protected override void SetItem(int index, ColumnName item) {
            base.SetItem(index, item);
            Owner?.OnCollectionChanged();
        }
        protected override void ClearItems() {
            base.ClearItems();
            Owner?.OnCollectionChanged();
        }
    }
}
