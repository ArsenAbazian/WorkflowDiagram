using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;
using WfBaseScript.Editors;

namespace WfBaseScript {
    public class WfScriptNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Expression";

        public override string Type => "Expression";

        protected override bool OnInitializeCore(WfRunner runner) {
            if(Script == null)
                Script = CreateScript();
            if(Script == null)
                Diagnostic.Add(new WfDiagnosticInfo() { Type = WfDiagnosticSeverity.Error, Text = "Could not parse expression. Compilation errors." });
            return Script != null;
        }

        public override void OnVisit(WfRunner runner) {
            Task<ScriptState<object>> task = null;
            try {
                task = Script.RunAsync(this);
                task.Wait();
                Outputs[0].OnVisit(runner, task.Result.ReturnValue);
            }
            catch(Exception e) {
                Diagnostic.Add(new WfDiagnosticInfo() { Type = WfDiagnosticSeverity.Error, Text = "Exception occurs while visit node. " + e.ToString() });
                HasErrors = true;
                Outputs[0].OnVisit(runner, 0);
            }
        }

        protected virtual Script<object> CreateScript() {
            try {
                Script<object> res = CSharpScript.Create(Expression, ScriptOptions.Default.WithImports("System.Math"), GetType());
                return res;
            }
            catch(Exception) {
                return null;
            }
        }
        
        [XmlIgnore]
        [Browsable(false)]
        public object In0 { get { return Inputs[0].Value; } }
        [XmlIgnore]
        [Browsable(false)]
        public object In1 { get { return Inputs[1].Value; } }
        [XmlIgnore]
        [Browsable(false)]
        public object In2 { get { return Inputs[2].Value; } }
        [XmlIgnore]
        [Browsable(false)]
        public object In3 { get { return Inputs[3].Value; } }
        [XmlIgnore]
        [Browsable(false)]
        public object In4 { get { return Inputs[4].Value; } }
        [XmlIgnore]
        [Browsable(false)]
        public object In5 { get { return Inputs[5].Value; } }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfExpressionInputPoint() { Type = WfConnectionPointType.In, Name = "In0", Text = "In0", Requirement = WfRequirementType.Optional },
                new WfExpressionInputPoint() { Type = WfConnectionPointType.In, Name = "In1", Text = "In1", Requirement = WfRequirementType.Optional },
                new WfExpressionInputPoint() { Type = WfConnectionPointType.In, Name = "In2", Text = "In2", Requirement = WfRequirementType.Optional },
                new WfExpressionInputPoint() { Type = WfConnectionPointType.In, Name = "In3", Text = "In3", Requirement = WfRequirementType.Optional },
                new WfExpressionInputPoint() { Type = WfConnectionPointType.In, Name = "In4", Text = "In4", Requirement = WfRequirementType.Optional },
                new WfExpressionInputPoint() { Type = WfConnectionPointType.In, Name = "In5", Text = "In5", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Result", Text = "Result",  }
            }.ToList();
        }

        string expression = "0";
        [Category("Expression"), PropertyEditor(typeof(RepositoryItemExpressionEditor))]
        public string Expression {
            get { return expression; }
            set {
                if(Expression == value)
                    return;
                expression = value;
                OnExpressionChanged();
            }
        }

        protected virtual void OnExpressionChanged() {
            Script = null;
            OnPropertyChanged(nameof(Expression));
        }

        protected Script<object> Script { get; set; }
    }
}
