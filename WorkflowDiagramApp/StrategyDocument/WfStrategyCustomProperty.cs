using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowDiagram;
using WorkflowDiagramApp.Editors;

namespace WorkflowDiagramApp.StrategyDocument {
    public class WfStrategyCustomProperty : WfStrategyNodeBase {
        public override string VisualTemplateName => "AccessProperty";

        public override string Type => "Access Property";
        public override string Header { get => PropertyName; }
        string propertyName;
        public string PropertyName {
            get { return propertyName; }
            set {
                value = ConstrainStringValue(value);
                if(PropertyName == value)
                    return;
                propertyName = value;
                OnPropertyChanged(nameof(PropertyName));
            }
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public override void OnVisit(WfRunner runner) {
            Console.WriteLine("GetData: On OnVisit: " + Name);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Object In", Text = "Object In" }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Data Out", Text = "Data Out" }
            }.ToList();
        }
    }
}
