using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkflowDiagram;
using WorkflowDiagram.UI.Win;
using WorkflowDiagramApp.StrategyDocument;

namespace WorkflowDiagramApp {
    public partial class MainForm : DirectXForm {
        public MainForm() {
            InitializeComponent();

            RecentItems = (RecentItemsList)SerializationHelper.FromFile(RecentFileName, typeof(RecentItemsList));
            if(RecentItems == null) {
                RecentItems = new RecentItemsList();
                RecentItems.FileName = "recent.xml";
            }
            UpdateSettings();
            UpdateRecent();
        }

        protected virtual void UpdateSettings() {
            UserLookAndFeel.Default.SetSkinStyle(RecentItems.ThemeName, RecentItems.PalletteName);
        }

        protected string RecentFileName { get { return Path.GetDirectoryName(Application.ExecutablePath) + "\\recent.xml"; } }

        protected void UpdateRecent() {
            this.bliRecent.Strings.Clear();
            this.bliRecent.Strings.AddRange(RecentItems.Items.ToArray());
        }

        protected void UpdateRecent(string file) {
            RecentItems.AddFile(file);
            SerializationHelper.Save(RecentItems, typeof(RecentItemsList), RecentFileName);
            UpdateRecent();
        }

        RecentItemsList RecentItems { get; set; }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            string name = (string)XtraInputBox.Show(new XtraInputBoxArgs() { Caption = "Enter Name" });
            if(name == null) {
                XtraMessageBox.Show("Error: you must enter name");
                return;
            }
            AddDoumentControl(CreateDocument(name));
            //this.tabbedView1.AddDocument(new DialogueDataControl(data) { Text = name });
        }

        protected virtual WfDocument CreateDocumentCore(string name) {
            return new WfStrategyDocument() { Name = name };
        }

        protected WfDocument CreateDocument(string name) {
            var data = CreateDocumentCore(name);
            data.QueryNodeVisualData += Document_QueryNodeVisualData;
            data.Saved += (d, e) => {
                UpdateRecent(((WfDocument)d).FullPath);
            };
            data.Loaded += (d, e) => {
                UpdateRecent(((WfDocument)d).FullPath);
            };
            return data;
        }

        private void Document_QueryNodeVisualData(object sender, WfNodeEventArgs e) {
            
        }

        protected virtual void AddDoumentControl(WfDocument document) {
            Document = document;
            WfDiagramControl c = new WfDiagramControl(document);
            c.Dock = DockStyle.Fill;
            Controls.Add(c);
            c.BringToFront();
            c.FitToDrawing();
            this.ribbonControl1.MergeRibbon(c.Ribbon);
        }

        protected WfDocument Document {
            get; private set;
        }

        private void bbiOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(this.xtraOpenFileDialog1.ShowDialog() == DialogResult.OK) {
                WfDocument data = CreateDocument("");
                try {
                    data.Load(this.xtraOpenFileDialog1.FileName);
                    AddDoumentControl(data);
                    if(!RecentItems.Items.Contains(this.xtraOpenFileDialog1.FileName))
                        RecentItems.Items.Add(this.xtraOpenFileDialog1.FileName);
                    UpdateRecent();
                }
                catch(Exception ee) {
                    XtraMessageBox.Show("Error loading file: " + ee.ToString());
                }
            }
        }

        private void bliRecent_ListItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e) {
            string path = this.bliRecent.Strings[e.Index];
            if(!File.Exists(path)) {
                XtraMessageBox.Show("File does not exist at path " + path);
                RecentItems.Items.Remove(path);
                UpdateRecent();
            }
            WfDocument data = CreateDocument("");
            data.Load(path);
            AddDoumentControl(data);
        }

        protected WfRunner ActiveRunner { get; set; }

        private void bbiTestInit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(Document == null) {
                XtraMessageBox.Show("No Document loaded", "Run");
                return;
            }

            WfRunner runner = new WfRunner(Document);
            ActiveRunner = runner;
            bool res = runner.Initialize();
            if(res)
                XtraMessageBox.Show("Test initialization failed!", "Initialization");
            else
                XtraMessageBox.Show("Successfully initialized!", "Initialization");
        }

        private void bbiRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(Document == null) {
                XtraMessageBox.Show("No Document loaded", "Run");
                return;
            }
            WfRunner runner = new WfRunner(Document);
            ActiveRunner = runner;
            bool res = runner.Initialize();
            if(!res) {
                XtraMessageBox.Show("Test initialization failed!", "Initialization");
                return;
            }
            runner.RunAsync().ContinueWith(t => {
                if(t.Result == false)
                    XtraMessageBox.Show("Execution failed!", "Execution");
                else
                    XtraMessageBox.Show("Executed succesfully!", "Execution");
            });
        }

        private void biRunOnce_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(Document == null) {
                XtraMessageBox.Show("No Document loaded", "Run");
                return;
            }

            WfRunner runner = new WfRunner(Document);
            ActiveRunner = runner;
            bool res = runner.Initialize();
            if(!res) {
                XtraMessageBox.Show("Test initialization failed!", "Initialization");
                return;
            }
            runner.RunOnceAsync().ContinueWith(t => {
                if(t.Result == false)
                    XtraMessageBox.Show("Execution failed!", "Execution");
                else
                    XtraMessageBox.Show("Executed succesfully!", "Execution");
            });
        }

        private void biCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(ActiveRunner != null) {
                ActiveRunner.Cancel();
                ActiveRunner = null;
            }
        }
    }
}
