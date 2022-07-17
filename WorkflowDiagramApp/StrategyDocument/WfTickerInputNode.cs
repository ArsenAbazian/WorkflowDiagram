using Crypto.Core;
using Crypto.Core.Strategies;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WorkflowDiagram;
using WorkflowDiagramApp.Editors;

namespace WorkflowDiagramApp.StrategyDocument {
    public class WfTickerInputNode : WfStrategyNodeBase {
        public override string Header => Exchange + ": " + Ticker;
        public override string Type => "Ticker Input";

        public override string VisualTemplateName => "Input";

        ExchangeType exchange = ExchangeType.Poloniex;
        [Category("Ticker")]
        public ExchangeType Exchange {
            get { return exchange; }
            set {
                if(Exchange == value)
                    return;
                exchange = value;
                OnPropertyChanged(nameof(Exchange));
            }
        }

        string ticker = string.Empty;
        [Category("Ticker")]
        [PropertyEditor(typeof(RepositoryItemTickerCollectionEditor))]
        public string Ticker {
            get { return ticker; }
            set {
                value = ConstrainStringValue(value);
                if(Ticker == value)
                    return;
                ticker = value;
                OnPropertyChanged(nameof(Ticker));
            }
        }

        int candlestickInterval = 30;
        [Category("Ticker")]
        [PropertyEditor(typeof(RepositoryItemCandlestickCollectionEditor))]
        public int CandlestickIntervalMin {
            get { return candlestickInterval; }
            set {
                if(CandlestickIntervalMin == value)
                    return;
                candlestickInterval = value;
                OnPropertyChanged(nameof(CandlestickIntervalMin));
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public TickerInputInfo TickerInfo { get; private set; }

        [XmlIgnore]
        [Browsable(false)]
        public Ticker TickerCore { get; private set; }

        protected override bool OnInitializeCore(WfRunner runner) {
            Exchange e = Crypto.Core.Exchange.Get(Exchange);
            if(!e.Connect()) {
                Diagnostic.Add(new WfDiagnosticInfo() { Type = WfDiagnosticSeverity.Error, Text = "Could not connect to exchange." });
                return false;
            }
            TickerCore = e.GetTicker(Ticker);
            if(TickerCore == null) {
                Diagnostic.Add(new WfDiagnosticInfo() { Type = WfDiagnosticSeverity.Error, Text = "Ticker with specified name does not exist on exchange." });
                return false;
            }
            TickerCore.StartListenTickerStream();
            return true;
        }
        public override void OnVisit(WfRunner runner) {
            DataContext = TickerCore;
            Outputs["Ticker"].OnVisit(runner, TickerCore);
            Outputs["CurrentPrice"].OnVisit(runner, TickerCore.Last);
            Outputs["HighestBid"].OnVisit(runner, TickerCore.OrderBook.HighestBid);
            Outputs["LowestAsk"].OnVisit(runner, TickerCore.OrderBook.LowestAsk);
            Outputs["Spread"].OnVisit(runner, TickerCore.OrderBook.LowestAsk - TickerCore.OrderBook.HighestBid);
        }

        protected override List<WfConnectionPoint> GetDefaultInputs() {
            return new List<WfConnectionPoint>();
        }

        protected override List<WfConnectionPoint> GetDefaultOutputs() {
            return new WfConnectionPoint[] {
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Ticker", Text = "Ticker" },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "CurrentPrice", Text = "Last" },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "HighestBid", Text = "Highest Bid" },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "LowestAsk", Text = "Lowest Ask" },
                new WfConnectionPoint() { Type = WfConnectionPointType.Out, Name = "Spread", Text = "Spread" }
            }.ToList();
        }
    }
}
