using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowDiagram.Nodes.Base {
    public class WfDateTimeNode : WfVisualNodeBase {
        public override string VisualTemplateName => "CurrentDate";
        public override string Type => "CurrentDate";
        public override string Category => "Data";

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "DateTime", Text = "DateTime", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Date", Text = "Date", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Year", Text = "Year", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Month", Text = "Month", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Day", Text = "Day", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Hour24", Text = "Hour 24", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "HourAMPM", Text = "Hour AM/PM", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "AMPM", Text = "AM/PM", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Minute", Text = "Minute", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Second", Text = "Second", Requirement = WfRequirementType.Optional },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Millisecond", Text = "Millisecond", Requirement = WfRequirementType.Optional }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        protected override void OnVisitCore(WfRunner runner) {
            DateTime dt = DateTime.Now;
            Outputs["DateTime"].Visit(runner, dt);
            Outputs["Date"].Visit(runner, dt.Date);
            Outputs["Year"].Visit(runner, dt.Year);
            Outputs["Month"].Visit(runner, dt.Month);
            Outputs["Day"].Visit(runner, dt.Day);
            Outputs["Hour24"].Visit(runner, dt.Hour);
            Outputs["HourAMPM"].Visit(runner, GetAmPmHour(dt.Hour));
            Outputs["AMPM"].Visit(runner, GetAmPm(dt.Hour));
            Outputs["Minute"].Visit(runner, dt.Minute);
            Outputs["Second"].Visit(runner, dt.Second);
            Outputs["Millisecond"].Visit(runner, dt.Millisecond);
        }

        private string GetAmPm(int hour) {
            if(hour < 12)
                return "AM";
            return "PM";
        }

        private int GetAmPmHour(int hour) {
            if(hour == 12)
                return 12;
            return hour % 12;
        }

        public WfDateTimeType DateTimeType { get; set; } = WfDateTimeType.Local;
    }

    public enum WfDateTimeType { Local, Utc }
}
