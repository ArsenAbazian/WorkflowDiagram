using Crypto.Core;
using Crypto.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WfBaseScript;
using WorkflowDiagram;

namespace WorkflowDiagramApp.StrategyDocument {
    public class WfTickerTradeHistory : WfVisualNodeBase {
        public override string VisualTemplateName => "TradeHistory";

        public override string Type => "Trades";

        public override string Category => "Data";

        public override void OnVisit(WfRunner runner) {
            Ticker ticker = Inputs[0].Value as Ticker;
            if(ticker == null) {
                var res = new ResizeableArray<TradeInfoItem>();
                Outputs[0].OnVisit(runner, res);
                DataContext = res;
                return;
            }
            var trades = ticker.Exchange.GetTrades(ticker, Start, End, new RunnerCancellationTokenSource(runner).Token);
            Outputs[0].OnVisit(runner, trades);
            DataContext = trades;
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Ticker", Text = "Ticker", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.In, Name = "Trades", Text = "Trades", Requirement = WfRequirementType.Optional  }
            }.ToList();
        }

        protected override bool OnInitializeCore(WfRunner runner) {
            return true;
        }

        public DateTime Start {
            get; set;
        }

        public DateTime End {
            get; set;
        }
    }
}
