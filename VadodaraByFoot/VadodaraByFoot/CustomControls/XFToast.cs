using System;
using VadodaraByFoot.Interface;
using Xamarin.Forms;

namespace VadodaraByFoot.CustomControls
{
    public class XFToast
    {
		public static void ShortMessage(string message)
		{
			DependencyService.Get<IMessage>().ShortAlert(message);
		}

		public static void LongMessage(string message)
		{
			DependencyService.Get<IMessage>().LongAlert(message);
		}
    }
}
