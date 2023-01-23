using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Connectors.Helpers {
    public abstract class WfDatabaseConnectionProvider {
        public WfDatabaseConnectionProvider() { }

        public virtual DbConnection Connection { get; set; }
        
        public abstract string GetColumnDescription(WfNode owner, WfDataTableColumnInfo columnInfo);
        public abstract string GetColumnType(WfNode owner, WfDataTableColumnInfo columnInfo);

        public abstract string GetConnectionString(WfNode owner, string host, string dbName, string userName, string password);
        public abstract bool ExecuteBooleanQuery(WfNode owner, string query);
        public abstract int ExecuteNonQuery(WfNode owner, string query);

        public string GetInsertQueryString(WfNode owner, string tableName, ColumnRefCollection columns) {
            StringBuilder b = new StringBuilder();
            StringBuilder v = new StringBuilder();
            for(int i = 0; i < columns.Count; i++) {
                b.Append(columns[i].LowCaseName);
                v.Append(FormatValue(owner, columns[i].ColumnInfo, columns[i].Point));
                if(i < columns.Count - 1) {
                    b.Append(',');
                    v.Append(',');
                }
            }
            string columnsList = b.ToString();
            string valueList = v.ToString();
            string queryString = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", tableName, columnsList, valueList);
             return queryString;
        }

        protected string FormatValue(WfNode owner, WfDataTableColumnInfo columnInfo, WfConnectionPoint point) {
            return FormatValue(owner, columnInfo, point.Value);
        }
        protected abstract string FormatValue(WfNode owner, WfDataTableColumnInfo columnInfo, object value);
        public abstract bool Connect(WfNode owner, string host, string database, string username, string password);
        public abstract bool DatabaseExist(WfNode owner, string dbName);
        public abstract bool CreateDatabase(WfNode owner, string dbName);
        public abstract bool TableExist(WfNode owner, string tableName);
        public abstract bool CreateTable(WfNode owner, string tableName, WfDataTableColumnInfoCollection columns);
        public abstract List<WfDataTableColumnInfo> GetTableInfo(WfNode owner, string tableName);
        public bool CheckUpdateTableSchema(WfNode owner, string tableName, WfDataTableColumnInfoCollection columns) {
            List<WfDataTableColumnInfo> current = GetTableInfo(owner, tableName);
            if(current == null)
                return false;
            List<WfDataTableColumnInfo> update = new List<WfDataTableColumnInfo>();
            List<WfDataTableColumnInfo> remove = new List<WfDataTableColumnInfo>();
            List<WfDataTableColumnInfo> add = new List<WfDataTableColumnInfo>();
            
            for(int i = 0; i < current.Count; i++) {
                var found = columns.FirstOrDefault(c => c.LowCaseName == current[i].LowCaseName);
                if(found == null)
                    remove.Add(current[i]);
                else if(found.Type != current[i].Type ||
                    found.IsNullable != current[i].IsNullable ||
                    found.DefaultValue != current[i].DefaultValue ||
                    found.PrimaryKey != current[i].PrimaryKey) 
                    update.Add(found);
            }
            for(int i = 0; i < columns.Count; i++) {
                var found = current.FirstOrDefault(c => c.LowCaseName == columns[i].LowCaseName);
                if(found == null)
                    add.Add(columns[i]);
            }

            if(!RemoveColumns(owner, tableName, remove))
                return false;
            if(!UpdateColumns(owner, tableName, update))
                return false;
            if(!AddColumns(owner, tableName, add))
                return false;
            return true;
        }

        protected bool RemoveColumns(WfNode owner, string tableName, List<WfDataTableColumnInfo> columns) {
            for(int i = 0; i < columns.Count; i++) {
                if(!RemoveColumn(owner, tableName, columns[i]))
                    return false;
            }
            return true;
        }

        protected bool UpdateColumns(WfNode owner, string tableName, List<WfDataTableColumnInfo> columns) {
            for(int i = 0; i < columns.Count; i++) {
                if(!UpdateColumn(owner, tableName, columns[i]))
                    return false;
            }
            return true;
        }

        protected bool AddColumns(WfNode owner, string tableName, List<WfDataTableColumnInfo> columns) {
            for(int i = 0; i < columns.Count; i++) {
                if(!AddColumn(owner, tableName, columns[i]))
                    return false;
            }
            return true;
        }

        protected abstract bool AddColumn(WfNode owner, string tableName, WfDataTableColumnInfo info);
        protected abstract bool RemoveColumn(WfNode owner, string tableName, WfDataTableColumnInfo info);
        protected abstract bool UpdateColumn(WfNode owner, string tableName, WfDataTableColumnInfo info);

        public abstract bool Insert(WfNode owner, string table, ColumnRefCollection columns);
        public abstract DataTable Select(WfNode owner, string actualQuery);
    }
}
