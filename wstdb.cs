//
// Copyright (C) 2021, NinjaTrader LLC <www.ninjatrader.com>.
// NinjaTrader reserves the right to modify or overwrite this NinjaScript component with each release.
//
#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

// This namespace holds indicators in this folder and is required. Do not change it.
namespace NinjaTrader.NinjaScript.Indicators
{
	/// <summary>
	/// Bollinger Bands are plotted at standard deviation levels above and below a moving average.
	/// Since standard deviation is a measure of volatility, the bands are self-adjusting:
	/// widening during volatile markets and contracting during calmer periods.
	/// </summary>
	public class WSTDB : Indicator
	{
		private WMA		wma;
		private WStdDev	WstdDev;

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description				 = "Weighted standard deviation bands";
				Name					 = "WSTDB";
				IsOverlay				 = true;
				IsSuspendedWhileInactive = true;
				NumStdDev				 = 2;
				Period					 = 14;

				AddPlot(Brushes.Goldenrod, "Upper" );
				AddPlot(Brushes.Goldenrod, "Middle");
				AddPlot(Brushes.Goldenrod, "Lower" );
			}
			else if (State == State.DataLoaded)
			{
				wma		= WMA(Period);
				WstdDev	= WStdDev(Period);
			}
		}

		protected override void OnBarUpdate()
		{
			double wma0		= wma[0];
			double WstdDev0	= WstdDev[0];

			Upper [0] = wma0 + NumStdDev * WstdDev0;
			Middle[0] = wma0;
			Lower [0] = wma0 - NumStdDev * WstdDev0;
		}

		#region Properties
		[Browsable(false)]
		[XmlIgnore()]
		public Series<double> Lower
		{
			get { return Values[2]; }
		}

		[Browsable(false)]
		[XmlIgnore()]
		public Series<double> Middle
		{
			get { return Values[1]; }
		}

		[Range(0, int.MaxValue), NinjaScriptProperty]
		[Display(ResourceType = typeof(Custom.Resource), Name = "NumStdDev", GroupName = "NinjaScriptParameters", Order = 0)]
		public double NumStdDev
		{ get; set; }

		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(ResourceType = typeof(Custom.Resource), Name = "Period", GroupName = "NinjaScriptParameters", Order = 1)]
		public int Period
		{ get; set; }

		[Browsable(false)]
		[XmlIgnore()]
		public Series<double> Upper
		{
			get { return Values[0]; }
		}
		#endregion
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private WSTDB[] cacheWSTDB;
		public WSTDB WSTDB(double numStdDev, int period)
		{
			return WSTDB(Input, numStdDev, period);
		}

		public WSTDB WSTDB(ISeries<double> input, double numStdDev, int period)
		{
			if (cacheWSTDB != null)
				for (int idx = 0; idx < cacheWSTDB.Length; idx++)
					if (cacheWSTDB[idx] != null && cacheWSTDB[idx].NumStdDev == numStdDev && cacheWSTDB[idx].Period == period && cacheWSTDB[idx].EqualsInput(input))
						return cacheWSTDB[idx];
			return CacheIndicator<WSTDB>(new WSTDB(){ NumStdDev = numStdDev, Period = period }, input, ref cacheWSTDB);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.WSTDB WSTDB(double numStdDev, int period)
		{
			return indicator.WSTDB(Input, numStdDev, period);
		}

		public Indicators.WSTDB WSTDB(ISeries<double> input , double numStdDev, int period)
		{
			return indicator.WSTDB(input, numStdDev, period);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.WSTDB WSTDB(double numStdDev, int period)
		{
			return indicator.WSTDB(Input, numStdDev, period);
		}

		public Indicators.WSTDB WSTDB(ISeries<double> input , double numStdDev, int period)
		{
			return indicator.WSTDB(input, numStdDev, period);
		}
	}
}

#endregion
