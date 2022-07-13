using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;

namespace WorkflowDiagramApp.StrategyDocument {
    public class WfStrategyExpression : WfStrategyNodeBase {
        public override string VisualTemplateName => "Expression";

        public override string Type => "Expression";

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public override void OnVisit(WfRunner runner) {
            if(Script == null)
                Script = CreateScript();
            Task<ScriptState<object>> task = Script.RunAsync();
            task.Wait();
            Outputs[0].Value = task.Result.ReturnValue;
        }

        protected virtual Script<object> CreateScript() {
            Script<object> res = CSharpScript.Create(string.Format("" +
                "using System;" +
                "   try {" +
                "       {0}" +
                "       return {1};" +
                "   }" +
                "   catch(Exception) {" +
                "       return 0;" +
                "}", CreateParametersList(), Expression));
            return res;
        }

        protected string CreateParametersList() {
            StringBuilder b = new StringBuilder();
            foreach(var p in Inputs) {
                WfExpressionInputPoint ep = p as WfExpressionInputPoint;
                if(ep == null)
                    continue;
                b.Append(CreateParameter(ep));
            }
            return b.ToString();
        }

        private string CreateParameter(WfExpressionInputPoint p) {
            if(p.InputType == WfValueType.Boolean)
                return string.Format("bool {0} = {1};", p.Name, p.Value);
            if(p.InputType == WfValueType.Decimal)
                return string.Format("double {0} = {1};", p.Name, p.Value);
            return string.Format("string {0} = \"{1}\";", p.Name, p.Value);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfExpressionInputPoint() { Type = WfConnectionPointType.In, Name = "In0", Text = "In0",  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Result", Text = "Result",  }
            }.ToList();
        }

        string expression = "0";
        [Category("Expression")]
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
