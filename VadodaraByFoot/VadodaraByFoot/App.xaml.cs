using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace VadodaraByFoot
{
    public partial class App : Application
    {
		public App()
		{
			InitializeComponent();
           // MainPage = new NavigationPage(new VadodaraByFoot.View.WalkTourModule.RouteCoversPage());
            //var StartUpPage = new NavigationPage(new LoginPage());
            //MainPage = new NavigationPage(new VadodaraByFoot.View.LoginModule.RegistrationPage() { });
           // MainPage = new NavigationPage(new VadodaraByFoot.View.LoginModule.LoginPage() { });
            // MainPage = new NavigationPage(new VadodaraByFoot.View.WalkTourModule.WalkToursListPage());
            //   MainPage = new NavigationPage(new VadodaraByFoot.View.FAQs());
            MainPage = new View.MenuModule.MenuMaster();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
