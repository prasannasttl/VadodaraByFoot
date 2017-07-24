﻿using System;
using Xamarin.Forms;

using System.Timers;
using VadodaraByFoot.Interface;
using VadodaraByFoot.Droid.DependencyService;
[assembly: Dependency(typeof(AdvancedTimerImplementationAndroid))]
namespace VadodaraByFoot.Droid.DependencyService
{
	/// <summary>
	/// AdvancedTimer Implementation
	/// </summary>
	public class AdvancedTimerImplementationAndroid : IAdvancedTimer
	{
		Timer timer;
		int interval;
		bool autoReset;

		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public static void Init() { }

		/// <summary>
		/// Used for initializing timer and options
		/// </summary>
		public void InitTimer(int interval, EventHandler e, bool autoReset)
		{
			//if (this.timer == null)
			//{
				this.timer = new Timer(interval);
	 			this.timer.Elapsed += new ElapsedEventHandler(e);
			//}

			this.interval = interval;
			this.autoReset = autoReset;

			this.timer.AutoReset = autoReset;
		}

		/// <summary>
		/// Used for starting timer
		/// </summary>
		public void StartTimer()
		{
			if (this.timer != null)
			{
				if (!this.timer.Enabled)
				{
					this.timer.Start();
				}
			}
			else
			{
				throw new NullReferenceException("Timer not initialized. You should call initTimer function first!");
			}
		}

		/// <summary>
		/// Used for stopping timer
		/// </summary>
		public void StopTimer()
		{
			if (this.timer != null)
			{
				if (this.timer.Enabled)
				{
					this.timer.Stop();
				}
			}
			else
			{
				throw new NullReferenceException("Timer not initialized. You should call initTimer function first!");
			}
		}

		/// <summary>
		/// Used for checking timer status
		/// </summary>
		public bool IsTimerEnabled
		{
			get
			{
				return this.timer.Enabled;
			}
		}

		/// <summary>
		/// Used for checking timer interval
		/// </summary>
		public int Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				this.interval = value;
				this.timer.Interval = value;
			}
		}

	}
}
