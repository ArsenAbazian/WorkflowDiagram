using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base.Core.Develop {
    public class WfRandomSelector : WfVisualNodeBase, IWfValueCollectionOwner {
        public WfRandomSelector() {
            Values = new WfValueCollection(this);
        }

        public WfValueCollection Values { get; }
        void IWfValueCollectionOwner.OnCollectionChanged() {
            
        }

        public override string VisualTemplateName => "RandomSelector";

        public override string Type => "Random Selector";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "ValueIn", Text = "Value In", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "ValueOut", Text = "Value Out", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            Random = new Random(DateTime.Now.Millisecond);
            return true;
        }

        protected Random Random { get; private set; }
        protected override void OnVisitCore(WfRunner runner) {
            double val = Inputs["ValueIn"].Value == null ? Random.NextDouble() : Convert.ToDouble(Inputs["ValueIn"].Value);
            double delta = 1.0 / Values.Count;
            int index = (int)(val / delta);

            if(Values.Count == 0)
                Outputs["ValueOut"].Visit(runner, null);
            else if(index >= Values.Count)
                Outputs["ValueOut"].Visit(runner, Values.Last().Value);
            else if(index < 0)
                Outputs["ValueOut"].Visit(runner, Values[0].Value);
            else
                Outputs["ValueOut"].Visit(runner, Values[index].Value);
        }
    }

    public interface IWfValueCollectionOwner {
        void OnCollectionChanged();
    }

    public class WfValueCollection : ObservableCollection<WfValueInfo> {
        public WfValueCollection(IWfValueCollectionOwner owner) {
            Owner = owner;
        }

        public IWfValueCollectionOwner Owner { get; private set; }
    }

    public class WfValueInfo {
        public WfValueInfo() {
            Type = WfValueType.Integer;
            Value = 0;
        }

        [Browsable(false)]
        public WfValueType Type { get; set; }
        object _value;
        [Category("Value"),
            WinPropertyEditor("WorkflowDiagram.UI.Win.Editors", "WorkflowDiagram.UI.Win.Editors.RepositoryItemObjectValueEditor"),
            BlazorPropertyEditor("WorkflowDiagram.UI.Blazor", "WorkflowDiagram.UI.Blazor.NodeEditors.ObjectValueEditor")]
        public object Value {
            get { return _value; }
            set {
                if(object.Equals(Value, value))
                    return;
                _value = value;
                OnValueChanged();
            }
        }

        protected void UpdateValueType() {
            if(Value == null)
                Type = WfValueType.Decimal;
            else if(Value is int)
                Type = WfValueType.Integer;
            else if(Value is long)
                Type = WfValueType.Integer64;
            else if(Value is double || Value is float)
                Type = WfValueType.Decimal;
            else if(Value is string)
                Type = WfValueType.String;
            else if(Value is DateTime)
                Type = WfValueType.DateTime;
        }

        protected virtual void OnValueChanged() {
            UpdateValueType();
        }

        public override string ToString() {
            return String.Format("[{0}] {1}", Type, Convert.ToString(Value));
        }
    }
}
