using System;
using System.IO;
using Foundation;
using UIKit;
using VadodaraByFoot.Interface;
[assembly: Xamarin.Forms.Dependency(typeof(VadodaraByFoot.iOS.DependencyService.AudioDownloadDependency))]
namespace VadodaraByFoot.iOS.DependencyService
{
    public class AudioDownloadDependency : IAudioDownlaod
    {
        #region Audio Download Methods
        public bool Download_Completed { get; set; }
        public void Downlaod_mp3_Url(string url, string audioFileName, Xamarin.Forms.ActivityIndicator LoadingDownloadProgress, Xamarin.Forms.Label DownloadingLabel)
        {
			LoadingDownloadProgress.IsRunning = true;
			DownloadingLabel.IsVisible = true;

          //  LongAlert(audioFileName + " Downloading..");
            var webClient = new System.Net.WebClient();
			
            webClient.DownloadDataCompleted += (s, e) =>
            {
                var text = e.Result; // get the downloaded text
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                string localPath = System.IO.Path.Combine(documentsPath, audioFileName);
                Console.WriteLine(localPath);
                File.WriteAllBytes(localPath, text); // writes to local storage   
                LongAlert(audioFileName + " Downloaded to path" );
                LoadingDownloadProgress.IsRunning = false;
                DownloadingLabel.IsVisible = false;
            };

            try
			{
				var urla = new Uri(url); // give this an actual URI to an MP3
				webClient.DownloadDataAsync(urla);
			}
			catch (System.Threading.Tasks.TaskCanceledException)
			{
             //   LongAlert("Task Canceled!");
                LoadingDownloadProgress.IsRunning = false;
                DownloadingLabel.IsVisible = false;
				return;
			}
			catch (Exception a)
			{
				//LongAlert(a.InnerException.Message);
                LoadingDownloadProgress.IsRunning = false;
                DownloadingLabel.IsVisible = false;
				Download_Completed = false;
				return;
			}


			Download_Completed = false;
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
        }
        public void WebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                Download_Completed = true;
            }
        }
		#endregion
		#region Toast Message Download Status Code

		const double LONG_DELAY = 3.5;
		const double SHORT_DELAY = 2;

		NSTimer alertDelay;
		UIAlertController alert;

		public void LongAlert(string message)
		{
			ShowAlert(message, LONG_DELAY);
		}
		public void ShortAlert(string message)
		{
			ShowAlert(message, SHORT_DELAY);
		}

		void ShowAlert(string message, double seconds)
		{
			alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
			{
				dismissMessage();
			});
			alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
			UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
		}
		void dismissMessage()
		{
			if (alert != null)
			{
				alert.DismissViewController(true, null);
			}
			if (alertDelay != null)
			{
				alertDelay.Dispose();
			}
		}
        #endregion
    }
}
