using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram.Nodes.Base;

namespace WorkflowDiagram.Nodes.Connectors {
    public class WfExportDatabaseTable : WfVisualNodeBase {
        public override string VisualTemplateName => "ExportDataTable";

        public override string Type => "Export DataTable";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint(){ Name = "Table", Text = "Table", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new List<WfConnectionPoint>();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            if(ExportType == WfDatabaseOutputFileType.Csv || 
                ExportType == WfDatabaseOutputFileType.Xml) {
                if(string.IsNullOrEmpty(FileName))
                    return;
            }

            DataTable tb = Inputs["Table"].Value as DataTable;
            if(tb == null)
                return;

            if(string.IsNullOrEmpty(tb.TableName))
                tb.TableName = GetActualTableName();
            WfFile file = new WfFile(FileName);
            file.Generated = true;
            file.Extension = ExportType.ToString().ToLower();
            try {
                if(System.IO.File.Exists(file.ItemId))
                    System.IO.File.Delete(file.ItemId);
            }
            catch(Exception) { }
            if(ExportType == WfDatabaseOutputFileType.Xml) {
                using(var wr = System.IO.File.CreateText(file.ItemId)) {
                    tb.WriteXml(wr);
                    wr.Flush();
                }
            }
            else if(ExportType == WfDatabaseOutputFileType.Csv) {
                using(var wr = System.IO.File.CreateText(file.ItemId)) {
                    for(int i = 0; i < tb.Columns.Count; i++) {
                        wr.Write(tb.Columns[i].ColumnName);
                        if(i < tb.Columns.Count - 1)
                            wr.Write(',');
                    }
                    wr.Write('\n');
                    for(int row = 0; row < tb.Rows.Count; row++) {
                        var rowData = tb.Rows[row];
                        for(int i = 0; i < tb.Columns.Count; i++) {
                            if(rowData.ItemArray[i] is string)
                                wr.Write(String.Format("\"{0}\"", rowData.ItemArray[i]));
                            else
                                wr.Write(Convert.ToString(rowData.ItemArray[i]));
                            if(i < tb.Columns.Count - 1)
                                wr.Write(',');
                        }
                        wr.Write('\n');
                    }
                    wr.Flush();
                }
                //file.Data = m.GetBuffer();
            }
            file.FullName = file.Name = FileName + "." + ExportType.ToString().ToLower();
            Document.Files.Add(file);
            File = file;
        }

        protected virtual string GetActualTableName() {
            if(string.IsNullOrEmpty(TableName))
                return "Default";
            return TableName;
        }

        [XmlIgnore]
        [Browsable(false)]
        public WfFile File { get; protected set; }
        public string FileName { get; set; }
        public string TableName { get; set; }
        public WfDatabaseOutputFileType ExportType { get; set; }
    }

    public enum WfDatabaseOutputFileType {
        Csv,
        Xml
    }
}
