
using System;
using System.Collections.Generic;
using System.Text;
using VadodaraByFoot.iOS.DependencyService;

[assembly: Xamarin.Forms.Dependency(typeof(PhoneDialerIos))]
namespace VadodaraByFoot.iOS.DependencyService
{
    public class PhoneDialerIos : Interface.IDialer
    {
        public bool Dial(string number)
        {
            return UIKit.UIApplication.SharedApplication.OpenUrl(
                new Foundation.NSUrl("tel:" + number.Replace(" ", "")));
        }
    }
}
