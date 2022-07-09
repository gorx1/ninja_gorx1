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
	public class WStdDev : Indicator
	{
		private WMA	wm;

		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description					= "Weighted Standard Deviation";
				Name						= "WStDev";
				IsOverlay					= false;
				IsSuspendedWhileInactive	= true;
				Period						= 14;

				AddPlot(Brushes.DarkCyan, Name);
			}
			else if (State == State.DataLoaded)
			{
				wm = WMA(Inputs[0], Period);			
			}
		}

		protected override void OnBarUpdate()
		{
			if (CurrentBar < 1)
			{
				Value[0] = 0;
			}
			else
			{
				double sum   = 0;
				int    w_sum = 0;
				int    back  = Math.Min(CurrentBar, Period - 1);
				
				for (int i = back; i >= 0; i--)
				{
					w_sum += (i + 1);
					sum   += Math.Pow(Input[back - i] - wm[0], 2) * (i + 1);
				}
				
				Value[0] = Math.Sqrt(sum / w_sum);
			}
		}

		#region Properties
		[Range(1, int.MaxValue), NinjaScriptProperty]
		[Display(ResourceType = typeof(Custom.Resource), Name = "Period", GroupName = "NinjaScriptParameters", Order = 0)]
		public int Period
		{ get; set; }
		#endregion
	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private WStdDev[] cacheWStdDev;
		public WStdDev WStdDev(int period)
		{
			return WStdDev(Input, period);
		}

		public WStdDev WStdDev(ISeries<double> input, int period)
		{
			if (cacheWStdDev != null)
				for (int idx = 0; idx < cacheWStdDev.Length; idx++)
					if (cacheWStdDev[idx] != null && cacheWStdDev[idx].Period == period && cacheWStdDev[idx].EqualsInput(input))
						return cacheWStdDev[idx];
			return CacheIndicator<WStdDev>(new WStdDev(){ Period = period }, input, ref cacheWStdDev);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.WStdDev WStdDev(int period)
		{
			return indicator.WStdDev(Input, period);
		}

		public Indicators.WStdDev WStdDev(ISeries<double> input , int period)
		{
			return indicator.WStdDev(input, period);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.WStdDev WStdDev(int period)
		{
			return indicator.WStdDev(Input, period);
		}

		public Indicators.WStdDev WStdDev(ISeries<double> input , int period)
		{
			return indicator.WStdDev(input, period);
		}
	}
}

#endregion
