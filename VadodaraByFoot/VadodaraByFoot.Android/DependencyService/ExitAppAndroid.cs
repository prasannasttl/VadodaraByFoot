using System;
using VadodaraByFoot.Droid.DependencyService;
using VadodaraByFoot.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(ExitAppAndroid))]
namespace VadodaraByFoot.Droid.DependencyService
{
	public class ExitAppAndroid : IExitApp
	{
		public void CloseApp()
		{
			Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
		}
	}
}