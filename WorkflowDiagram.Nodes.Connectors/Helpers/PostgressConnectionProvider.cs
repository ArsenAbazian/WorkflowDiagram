using Npgsql;
using Npgsql.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace WorkflowDiagram.Nodes.Connectors.Helpers {
    public class WfPostgreConnectionProvider : WfDatabaseConnectionProvider {
        public static WfPostgreConnectionProvider Default { get; private set; }

        static WfPostgreConnectionProvider() {
            Default = new WfPostgreConnectionProvider();
        }

        public new NpgsqlConnection Connection { get { return (NpgsqlConnection)base.Connection; } set { base.Connection = value; } }

        public override string GetColumnType(WfNode owner, WfDataTableColumnInfo columnInfo) {
            switch(columnInfo.Type) {
                case WfDataTableColumnType.Boolean: return "boolean";
                case WfDataTableColumnType.Integer: return "integer";
                case WfDataTableColumnType.Integer64: return "bigint";
                case WfDataTableColumnType.Double: return "double precision";
                case WfDataTableColumnType.DateTime: return "timestamp";
                case WfDataTableColumnType.VarChar: return string.Format("varchar({0})", columnInfo.Length);
                case WfDataTableColumnType.Text: return "text";
                default: throw new NotImplementedException("No column description for type: " + columnInfo.Type);
            }
        }

        public override string GetColumnDescription(WfNode owner, WfDataTableColumnInfo columnInfo) {
            StringBuilder b = new StringBuilder();
            b.Append(columnInfo.Name);
            b.Append(' ');
            b.Append(GetColumnType(owner, columnInfo));
            if(columnInfo.PrimaryKey) {
                if(columnInfo.Type == WfDataTableColumnType.Integer)
                    b.Append(" SERIAL PRIMARY KEY");
                else if(columnInfo.Type == WfDataTableColumnType.Integer64)
                    b.Append(" BIGSERIAL PRIMARY KEY");
                else 
                    b.Append(" PRIMARY KEY");
            }
            if(columnInfo.Type == WfDataTableColumnType.Text || columnInfo.Type == WfDataTableColumnType.VarChar) {
                if(columnInfo.IsNullable)
                    b.Append(" NULL");
                else
                    b.Append(" NOT NULL");
            }
            if(!string.IsNullOrEmpty(columnInfo.DefaultValue)) {
                b.Append(" DEFAULT ");
                if(columnInfo.Type == WfDataTableColumnType.VarChar || columnInfo.Type == WfDataTableColumnType.Text)
                    b.Append('\'');
                b.Append(columnInfo.DefaultValue);
                if(columnInfo.Type == WfDataTableColumnType.VarChar || columnInfo.Type == WfDataTableColumnType.Text)
                    b.Append('\'');
            }
            return b.ToString();
        }

        protected override string FormatValue(WfNode owner, WfDataTableColumnInfo col, object value) {
            switch(col.Type) {
                case WfDataTableColumnType.VarChar:
                case WfDataTableColumnType.Text:
                    return string.Format("'{0}'", value);
                case WfDataTableColumnType.Boolean:
                    if(value is string)
                        return (string)value;
                    if(value is int)
                        return ((int)value) == 0 ? "false" : "true";
                    if(value is double)
                        return ((double)value) == 0 ? "false" : "true";
                    return ((bool)value) == true ? "true" : "false";
                case WfDataTableColumnType.Integer:
                case WfDataTableColumnType.Integer64:
                case WfDataTableColumnType.Double:
                    return Convert.ToString(value, CultureInfo.InvariantCulture);
                case WfDataTableColumnType.DateTime:
                    DateTime datetimeValue = (DateTime)value;
                    string dateTimeFormatPattern;
                    dateTimeFormatPattern = "yyyy-MM-dd HH:mm:ss";
                    return string.Format("cast('{0}' as timestamp)", datetimeValue.ToString(dateTimeFormatPattern, CultureInfo.InvariantCulture));
            }
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        public override string GetConnectionString(WfNode owner, string host, string dbName, string userName, string password) {
            return string.Format("Host={0};Username={1};Password={2};Database={3};", host, userName, password, dbName);
        }

        public int ExecuteQuery(WfNode owner, string query) {
            try {
                using(var cmd = new NpgsqlCommand(query, Connection)) {
                    int rowCount = cmd.ExecuteNonQuery();
                    return rowCount;
                }
            }
            catch(Exception e) {
                owner.OnError("Error execute postgresql query: " + e.ToString());
                return 0;
            }
        }

        public override bool ExecuteBooleanQuery(WfNode owner, string query) {
            using(var cmd = new NpgsqlCommand(query, Connection)) {
                using(var reader = cmd.ExecuteReader()) {
                    if(!reader.HasRows || !reader.Read() || !reader.IsOnRow)
                        return false;
                    return reader.GetBoolean(0);
                }
            }
        }

        public override int ExecuteNonQuery(WfNode owner, string query) {
            using(var cmd = new NpgsqlCommand(query, Connection)) {
                int rowsAffected = cmd.ExecuteNonQuery();
                return (int)rowsAffected;
            }
        }

        public override bool Connect(WfNode owner, string host, string database, string username, string password) {
            if(Connection != null && Connection.State == System.Data.ConnectionState.Open)
                Connection.Close();
            string connectionString = GetConnectionString(owner, host, database, username, password);
            Connection = new NpgsqlConnection(connectionString);
            Connection.Open();
            return true;
        }

        public override bool DatabaseExist(WfNode owner, string dbName) {
            using(NpgsqlCommand command = new NpgsqlCommand(string.Format("SELECT datname FROM pg_catalog.pg_database WHERE datname='{0}';", dbName), Connection)) {
                using(var reader = command.ExecuteReader()) {
                    if(!reader.HasRows || !reader.Read() || !reader.IsOnRow)
                        return false;
                    string row = reader.GetString(0);
                    return row.ToLowerInvariant() == dbName.ToLowerInvariant();
                }
            }
        }

        public override bool CreateDatabase(WfNode owner, string dbName) {
            if(Connection == null || Connection.State != System.Data.ConnectionState.Open)
                return false;
            try {
                using(NpgsqlCommand command = new NpgsqlCommand(string.Format("CREATE DATABASE {0};", dbName), Connection)) {
                    int rowCount = command.ExecuteNonQuery();
                }
            }
            catch(Exception e) {
                owner.OnError("Cannot create database: " + e.ToString());
                return false;
            }
            return true;
        }

        public override bool TableExist(WfNode owner, string tableName) {
            tableName = tableName.ToLowerInvariant();
            string query = string.Format("SELECT EXISTS (SELECT FROM information_schema.tables where table_name = '{0}');", tableName);
            return ExecuteBooleanQuery(owner, query);
        }

        public override bool CreateTable(WfNode owner, string tableName, WfDataTableColumnInfoCollection columns) {
            tableName = tableName.ToLowerInvariant();
            StringBuilder b = new StringBuilder();
            for(int i = 0; i < columns.Count; i++) {
                b.Append(Default.GetColumnDescription(owner, columns[i]));
                if(i < columns.Count - 1)
                    b.Append(", ");
            }

            string query = string.Format("CREATE TABLE IF NOT EXISTS {0} ({1});", tableName, b.ToString());
            return ExecuteQuery(owner, query) == 1;
        }

        public override List<WfDataTableColumnInfo> GetTableInfo(WfNode owner, string tableName) {
            List<string> primaryKeys = GetPrimaryKeys(tableName);
            
            tableName = tableName.ToLowerInvariant();
            string query = string.Format("SELECT * FROM information_schema.columns WHERE table_name = '{0}';", tableName);
            List<WfDataTableColumnInfo> list = new List<WfDataTableColumnInfo>();
            try {
                using(NpgsqlCommand command = new NpgsqlCommand(query, Connection)) {
                    using(DbDataReader reader = command.ExecuteReader()) {
                        var columns = reader.GetColumnSchema();
                        var cName = columns.FirstOrDefault(c => c.ColumnName == "column_name");
                        var cType = columns.FirstOrDefault(c => c.ColumnName == "data_type");
                        var cLength = columns.FirstOrDefault(c => c.ColumnName == "character_maximum_length");
                        var cIsNullable = columns.FirstOrDefault(c => c.ColumnName == "is_nullable");
                        var cDefault = columns.FirstOrDefault(c => c.ColumnName == "column_default");

                        while(reader.Read()) {
                            WfDataTableColumnInfo info = new WfDataTableColumnInfo();
                            if(cName.ColumnOrdinal.HasValue)
                                info.Name = reader.GetString(cName.ColumnOrdinal.Value);
                            if(cType.ColumnOrdinal.HasValue)
                                info.Type = ToWfColumnType(reader.GetString(cType.ColumnOrdinal.Value));
                            if(info.Type == WfDataTableColumnType.VarChar)
                                info.Length = reader.GetInt32(cLength.ColumnOrdinal.Value);
                            if(cIsNullable.ColumnOrdinal.HasValue) {
                                string isNullableString = reader.GetString(cIsNullable.ColumnOrdinal.Value);
                                info.IsNullable = isNullableString == "YES";
                            }
                            info.PrimaryKey = primaryKeys.Contains(info.LowCaseName);
                            if(!info.IsNullable && cDefault.ColumnOrdinal.HasValue) {
                                object val = reader.GetValue(cDefault.ColumnOrdinal.Value);
                                if(!(val is DBNull)) {
                                    string str = ExtractDefaultValue(reader.GetString(cDefault.ColumnOrdinal.Value));
                                    
                                    info.DefaultValue = str;
                                }
                            }
                            list.Add(info);
                        }
                    }
                }
            }
            catch(Exception e) {
                owner.OnError("Cannot get table info: " + e.ToString());
                return null;
            }
            return list;
        }

        private string ExtractDefaultValue(string str) {
            int index = str.IndexOf("::");
            if(index > -1)
                str = str.Substring(0, index);
            if(str[0] == '\'')
                str = str.Substring(1, str.Length - 2);
            return str;
        }

        private List<string> GetPrimaryKeys(string tableName) {
            List<string> columns = new List<string>();
            string query = string.Format("SELECT pg_attribute.attname, format_type(pg_attribute.atttypid, pg_attribute.atttypmod) " +
                "FROM pg_index, pg_class, pg_attribute, pg_namespace " +
                "WHERE pg_class.oid = '{0}'::regclass " +
                "AND indrelid = pg_class.oid " +
                "AND nspname = 'public' " +
                "AND pg_class.relnamespace = pg_namespace.oid " +
                "AND pg_attribute.attrelid = pg_class.oid " +
                "AND pg_attribute.attnum = any(pg_index.indkey) AND indisprimary", tableName);
            using(NpgsqlCommand command = new NpgsqlCommand(query, Connection)) {
                using(DbDataReader reader = command.ExecuteReader()) {
                    while(reader.Read()) {
                        columns.Add(reader.GetString(0));
                    }
                }
            }
            return columns;
        }

        protected virtual WfDataTableColumnType ToWfColumnType(string dataTypeName) {
            if(dataTypeName == "boolean")
                return WfDataTableColumnType.Boolean;
            if(dataTypeName == "integer")
                return WfDataTableColumnType.Integer;
            if(dataTypeName == "bigint")
                return WfDataTableColumnType.Integer64;
            if(dataTypeName == "double precision")
                return WfDataTableColumnType.Double;
            if(dataTypeName.StartsWith("timestamp")) {
                //if(dataTypeName == "timestamp without time zone")
                return WfDataTableColumnType.DateTime;
            }
            if(dataTypeName == "character varying")
                return WfDataTableColumnType.VarChar;
            if(dataTypeName == "text")
                return WfDataTableColumnType.Text;
            return WfDataTableColumnType.VarChar;
        }

        protected override bool AddColumn(WfNode owner, string tableName, WfDataTableColumnInfo info) {
            string query = string.Format("ALTER TABLE IF EXISTS {0} ADD COLUMN IF NOT EXISTS {1};", tableName.ToLowerInvariant(), GetColumnDescription(owner, info));
            return ExecuteQuery(owner, query) == 1;
        }

        protected override bool RemoveColumn(WfNode owner, string tableName, WfDataTableColumnInfo info) {
            string query = string.Format("ALTER TABLE IF EXISTS {0} DROP COLUMN IF EXISTS {1} CASCADE;", tableName.ToLowerInvariant(), info.LowCaseName);
            return ExecuteQuery(owner, query) == 1;
        }

        protected virtual bool UpdateDataType(WfNode owner, string tableName, WfDataTableColumnInfo info) {
            string query = string.Format("ALTER TABLE IF EXISTS {0} ALTER COLUMN {1} SET DATA TYPE {2};", tableName.ToLowerInvariant(), info.LowCaseName, GetColumnType(owner, info));
            return ExecuteQuery(owner, query) == 1;
        }

        protected override bool UpdateColumn(WfNode owner, string tableName, WfDataTableColumnInfo info) {
            if(!UpdateDataType(owner, tableName, info))
                return false;
            if(!UpdateIsNull(owner, tableName, info))
                return false;
            if(!UpdateDefaultValue(owner, tableName, info))
                return false;
            return true;
        }

        protected virtual bool UpdateDefaultValue(WfNode owner, string tableName, WfDataTableColumnInfo info) {
            string query = null;
            if(string.IsNullOrEmpty(info.DefaultValue))
                query = string.Format("ALTER TABLE IF EXISTS {0} ALTER COLUMN {1} DROP DEFAULT;", tableName.ToLowerInvariant(), info.LowCaseName);
            else if(info.Type == WfDataTableColumnType.VarChar || info.Type == WfDataTableColumnType.Text)
                query = string.Format("ALTER TABLE IF EXISTS {0} ALTER COLUMN {1} SET DEFAULT '{2}';", tableName.ToLowerInvariant(), info.LowCaseName, info.DefaultValue);
            else
                query = string.Format("ALTER TABLE IF EXISTS {0} ALTER COLUMN {1} SET DEFAULT {2};", tableName.ToLowerInvariant(), info.LowCaseName, info.DefaultValue);
            return ExecuteQuery(owner, query) == 1;
        }

        protected virtual bool UpdateIsNull(WfNode owner, string tableName, WfDataTableColumnInfo info) {
            string command = info.IsNullable ? "DROP NOT NULL" : "SET NOT NULL";
            string query = string.Format("ALTER TABLE IF EXISTS {0} ALTER COLUMN {1} {2};", tableName.ToLowerInvariant(), info.LowCaseName, command);
            return ExecuteQuery(owner, query) == 1;
        }

        public override string ToString() {
            return "PostgreSQL Connector";
        }

        public override bool Insert(WfNode owner, string table, ColumnRefCollection columns) {
            return ExecuteNonQuery(owner, GetInsertQueryString(owner, table, columns)) == 1;
        }

        public override DataTable Select(WfNode owner, string query) {
            try {
                using(var cmd = new NpgsqlCommand(query, Connection)) {
                    using(NpgsqlDataReader reader = cmd.ExecuteReader()) {
                        DataTable table = new DataTable();
                        var columns = reader.GetColumnSchema();
                        for(int i = 0; i < columns.Count; i++) 
                            table.Columns.Add(new DataColumn() { ColumnName = columns[i].ColumnName, DataType = columns[i].DataType });
                        while(reader.Read()) {
                            object[] row = new object[reader.FieldCount];
                            reader.GetValues(row);
                            table.Rows.Add(row);
                        }
                        return table;
                    }
                }
            }
            catch(Exception ex) {
                owner.OnError("Error, while executing sql query: '" + query + "'. Error: " + ex.Message);
                return null;
            }
        }
    }
}
