using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon;
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
using XmlSerialization;

namespace WorkflowDiagramApp {
    public partial class MainForm : RibbonForm {
        public MainForm() {
            InitializeComponent();

            RecentItems = (RecentItemsList)SerializationHelper.Current.FromFile(RecentFileName, typeof(RecentItemsList));
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
            SerializationHelper.Current.Save(RecentItems, typeof(RecentItemsList), RecentFileName);
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
            return new WfDocument() { Name = name };
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

        protected WfDocumentControl ActiveDocumentControl { 
            get {
                return (WfDocumentControl)this.tabbedView1.ActiveDocument?.Control;
            } 
        }
        protected virtual void AddDoumentControl(WfDocument document) {
            this.tabbedView1.AddDocument(new WfDocumentControl(document) { Text = document.Name });
        }

        protected WfDocument ActiveDocument {
            get {
                return ActiveDocumentControl?.Document;
            }
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
        protected virtual WfRunner CreateRunner() {
            if(ActiveDocumentControl != null && ActiveDocumentControl.DocumentOwner != null)
                return ActiveDocumentControl.DocumentOwner.CreateRunner(ActiveDocument);
            return new WfRunner(ActiveDocument);
        }

        private void bbiTestInit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(ActiveDocument == null) {
                XtraMessageBox.Show("No Document loaded", "Run");
                return;
            }

            WfRunner runner = CreateRunner();
            ActiveRunner = runner;
            bool res = runner.Initialize();
            if(res)
                XtraMessageBox.Show("Test initialization failed!", "Initialization");
            else
                XtraMessageBox.Show("Successfully initialized!", "Initialization");
        }

        private void bbiRun_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(ActiveDocument == null) {
                XtraMessageBox.Show("No Document loaded", "Run");
                return;
            }
            WfRunner runner = CreateRunner();
            ActiveRunner = runner;
            bool res = runner.Initialize();
            if(!res) {
                ActiveDocumentControl.AnimationEnabled = false;
                XtraMessageBox.Show("Test initialization failed!", "Initialization");
                return;
            }
            ActiveDocumentControl.AnimationEnabled = true;
            runner.RunAsync().ContinueWith(t => {
                BeginInvoke(new Action<bool>(tr => {
                    if(ActiveDocumentControl != null)
                        ActiveDocumentControl.AnimationEnabled = false;
                    if(tr == false)
                        XtraMessageBox.Show("Execution failed!", "Execution");
                    else
                        XtraMessageBox.Show("Executed succesfully!", "Execution");
                }), t.Result);
            });
        }

        private void biRunOnce_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(ActiveDocument == null) {
                XtraMessageBox.Show("No Document loaded", "Run");
                return;
            }

            WfRunner runner = CreateRunner();
            ActiveRunner = runner;
            bool res = runner.Initialize();
            if(!res) {
                ActiveDocumentControl.AnimationEnabled = false;
                XtraMessageBox.Show("Test initialization failed!", "Initialization");
                return;
            }
            ActiveDocumentControl.AnimationEnabled = true;
            runner.RunOnceAsync().ContinueWith(t => {
                IProgress<bool> progress = new Progress<bool>(tr => {
                    if(ActiveDocumentControl != null)
                        ActiveDocumentControl.AnimationEnabled = false;
                    if(tr == false)
                        XtraMessageBox.Show("Execution failed!", "Execution");
                    else
                        XtraMessageBox.Show("Executed succesfully!", "Execution");
                });
                progress.Report(t.Result);
                //BeginInvoke(new Action<bool>(tr => {
                    
                //}), t.Result);
            });
        }

        private void biCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(ActiveRunner != null) {
                ActiveRunner.Cancel();
                ActiveRunner = null;
                if(ActiveDocumentControl != null)
                    ActiveDocumentControl.AnimationEnabled = false;
            }
        }

        private void documentManager1_DocumentActivate(object sender, DevExpress.XtraBars.Docking2010.Views.DocumentEventArgs e) {
            if(e.Document == null || e.Document.IsFloating)
                return;
            WfDocumentControl c = (WfDocumentControl)e.Document.Control;
            this.ribbonControl1.MergeRibbon(c.Ribbon);
        }
    }
}
