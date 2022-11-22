using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkflowDiagram.Nodes.Base {
    public class WfTimerNode : WfVisualNodeBase {
        public override string VisualTemplateName => "Timer";

        public override string Type => "Timer";

        public override string Category => "Timing";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "In", Text = "In", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Out", Text = "Out", Requirement = WfRequirementType.Optional },
            }.ToList();
        }

        protected long LastTime { get; set; } = 0;
        protected Stopwatch Stopwatch { get; set; } = null;
        public bool SleepNextTime { get; set; }
        protected override bool OnInitializeCore(WfRunner runner) {
            if(Stopwatch != null)
                Stopwatch.Stop();
            Stopwatch = new Stopwatch();
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            if(!Stopwatch.IsRunning) {
                Stopwatch.Start();
                LastTime = 0;
                Outputs["Out"].Visit(runner, Inputs["In"].Value);
                return;
            }
            long passed = Stopwatch.ElapsedMilliseconds - LastTime;
            if(SleepNextTime && passed < Milliseconds) {
                Thread.Sleep((int)(Milliseconds - passed));
                passed = Milliseconds + 1;
            }
            if(passed > Milliseconds) {
                LastTime = Stopwatch.ElapsedMilliseconds;
                Outputs["Out"].Visit(runner, Inputs["In"].Value);
                return;
            }
            Outputs["Out"].SkipVisit(runner, Inputs["In"].Value);
        }

        [Browsable(false)]
        public long Milliseconds { get; set; } = 1000;

        [XmlIgnore]
        public TimeSpan Interval { get { return TimeSpan.FromMilliseconds(Milliseconds); } set { Milliseconds = (long)value.TotalMilliseconds; } }
    }
}
