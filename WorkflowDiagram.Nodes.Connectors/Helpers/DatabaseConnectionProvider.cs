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
        public WfDatabaseConnectionProvider(WfDiagnosticHelper helper) {
            DiagnosticHelper = helper;
        }

        protected WfDiagnosticHelper DiagnosticHelper { get; private set; }
        public virtual DbConnection Connection { get; set; }

        public abstract string GetColumnDescription(WfDataTableColumnInfo columnInfo);
        public abstract string GetColumnType(WfDataTableColumnInfo columnInfo);

        public abstract string GetConnectionString(string host, string dbName, string userName, string password);
        public abstract bool ExecuteBooleanQuery(string query);
        public abstract int ExecuteNonQuery(string query);

        public string GetInsertQueryString(string tableName, ColumnRefCollection columns) {
            StringBuilder b = new StringBuilder();
            StringBuilder v = new StringBuilder();
            for(int i = 0; i < columns.Count; i++) {
                b.Append(columns[i].LowCaseName);
                v.Append(FormatValue(columns[i].ColumnInfo, columns[i].Point));
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

        protected string FormatValue(WfDataTableColumnInfo columnInfo, WfConnectionPoint point) {
            return FormatValue(columnInfo, point.Value);
        }
        protected abstract string FormatValue(WfDataTableColumnInfo columnInfo, object value);
        public abstract bool Connect(string host, string database, string username, string password);
        public abstract bool DatabaseExist(string dbName);
        public abstract bool CreateDatabase(string dbName);
        public abstract bool TableExist(string tableName);
        public abstract bool CreateTable(string tableName, WfDataTableColumnInfoCollection columns);
        public abstract List<WfDataTableColumnInfo> GetTableInfo(string tableName);
        public bool CheckUpdateTableSchema(string tableName, WfDataTableColumnInfoCollection columns) {
            List<WfDataTableColumnInfo> current = GetTableInfo(tableName);
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

            if(!RemoveColumns(tableName, remove))
                return false;
            if(!UpdateColumns(tableName, update))
                return false;
            if(!AddColumns(tableName, add))
                return false;
            return true;
        }

        protected bool RemoveColumns(string tableName, List<WfDataTableColumnInfo> columns) {
            for(int i = 0; i < columns.Count; i++) {
                if(!RemoveColumn(tableName, columns[i]))
                    return false;
            }
            return true;
        }

        protected bool UpdateColumns(string tableName, List<WfDataTableColumnInfo> columns) {
            for(int i = 0; i < columns.Count; i++) {
                if(!UpdateColumn(tableName, columns[i]))
                    return false;
            }
            return true;
        }

        protected bool AddColumns(string tableName, List<WfDataTableColumnInfo> columns) {
            for(int i = 0; i < columns.Count; i++) {
                if(!AddColumn(tableName, columns[i]))
                    return false;
            }
            return true;
        }

        protected abstract bool AddColumn(string tableName, WfDataTableColumnInfo info);
        protected abstract bool RemoveColumn(string tableName, WfDataTableColumnInfo info);
        protected abstract bool UpdateColumn(string tableName, WfDataTableColumnInfo info);

        public abstract bool Insert(string table, ColumnRefCollection columns);
        public abstract DataTable Select(string actualQuery);
    }
}
