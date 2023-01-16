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
    public class WfDatabaseConnectorNode : WfVisualNodeBase {
        public override string Category => "Database";

        public override string VisualTemplateName => "DatabaseConnector";

        public override string Type => "Database Connector";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Host", Text = "Host", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Name = "Username", Text = "User", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Name = "Password", Text = "Password", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Name = "Database", Text = "Database", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Name = "Database", Text = "Database" },
                new WfConnectionPoint() { Name = "Failed", Text = "Failed" }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            if(!IsConnected) {
                TryConnect();

                if(!IsConnected) {
                    Outputs["Failed"].Visit(runner, true);
                    Outputs["Database"].Visit(runner, null);
                    return;
                }
            }
            Outputs["Failed"].Visit(runner, false);
            Outputs["Database"].Visit(runner, Provider);
        }

        [XmlIgnore]
        [Browsable(false)]
        public WfDatabaseConnectionProvider Provider { get; protected set; }
        protected virtual bool TryConnect() {
            Provider = CreateProvider();
            if(Provider == null) {
                DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "Cannot get provider for : " + ConnectionType);
                return false;
            }
            if(string.IsNullOrEmpty(Host) && Inputs["Host"].Value != null)
                Host = Convert.ToString(Inputs["Host"].Value).Trim();
            if(string.IsNullOrEmpty(Username) && Inputs["Username"].Value != null)
                Username = Convert.ToString(Inputs["Username"].Value).Trim();
            if(string.IsNullOrEmpty(Password) && Inputs["Password"].Value != null)
                Password = Convert.ToString(Inputs["Password"].Value).Trim();
            if(string.IsNullOrEmpty(Database) && Inputs["Database"].Value != null)
                Database = Convert.ToString(Inputs["Database"].Value).Trim();
            ConnectionString = string.Format("Host={0};Username={1};Password={2};Database=postgres;", Host, Username, Password);
            try {
                if(!Provider.Connect(Host, "postgres", Username, Password)) {
                    DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "Cannot connect to postgres database: " + Host.ToString() + "/" + Database);
                    return false;
                }

                if(!Provider.DatabaseExist(Database)) {
                    if(!CreateIfNotExist || !Provider.CreateDatabase(Database))
                        return false;
                }

                IsConnected = Provider.Connect(Host, Database, Username, Password);
                return IsConnected;
            }
            catch(Exception e) {
                DiagnosticHelper.Add(WfDiagnosticSeverity.Error, "Cannot connect to postgres database: " + e.ToString());
            }
            return true;
        }

        protected virtual WfDatabaseConnectionProvider CreateProvider() {
            if(ConnectionType == ConnectionType.PostgreSql)
                return new WfPostgreConnectionProvider(DiagnosticHelper);
            return null;
        }

        protected bool IsConnected { get; set; }
        protected string ConnectionString { get; set; }
        public bool CreateIfNotExist { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public ConnectionType ConnectionType { get; set; }
    }

    public enum ConnectionType {
        PostgreSql
    }
}
