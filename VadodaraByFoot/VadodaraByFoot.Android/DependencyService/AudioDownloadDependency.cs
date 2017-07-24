using System;
using VadodaraByFoot.Interface;
[assembly: Xamarin.Forms.Dependency(typeof(VadodaraByFoot.Droid.DependencyService.AudioDownloadDependency))]
namespace VadodaraByFoot.Droid.DependencyService
{
    public class AudioDownloadDependency : IAudioDownlaod
    {
        public string _audioFileName;
        public bool Download_Completed { get; set; }
        public async void Downlaod_mp3_Url(string url, string audioFileName, Xamarin.Forms.ActivityIndicator LoadingDownloadProgress, Xamarin.Forms.Label DownloadingLabel)
		{
            _audioFileName = audioFileName;
            LoadingDownloadProgress.IsRunning = true;
            DownloadingLabel.IsVisible = true;
		//	Android.Widget.Toast.MakeText(Android.App.Application.Context, _audioFileName + " Downloading..", Android.Widget.ToastLength.Long).Show();
			var webClient = new System.Net.WebClient();
			//var url = new Uri("http://www.maninblack.org/demos/SitDownShutUp.mp3");
			//  string local = "/sdcard/Download/abc.mp3";
			byte[] bytes = null;
			
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
		
			try
			{
				bytes = await webClient.DownloadDataTaskAsync(url);
			}
			catch (System.Threading.Tasks.TaskCanceledException)
			{
             //  Android.Widget.Toast.MakeText(Android.App.Application.Context, "Task Canceled!", Android.Widget.ToastLength.Long).Show();
				return;
			}
			catch (Exception a)
			{
				//Android.Widget.Toast.MakeText(Android.App.Application.Context,a.InnerException.Message, Android.Widget.ToastLength.Long);
				Download_Completed = false;
				return;
			}
         
			Java.IO.File documentsPath = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads), "");
            string localFilename = documentsPath + "/" + audioFileName;
		
            //Save the Mp3 using writeAsync
         
            System.IO.FileStream fs =new System.IO.FileStream(localFilename, System.IO.FileMode.OpenOrCreate);
      		await fs.WriteAsync(bytes, 0, bytes.Length);
            fs.Close();
            LoadingDownloadProgress.IsRunning = false;
            DownloadingLabel.IsVisible = false;
            Download_Completed = false;

		}

		public void WebClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
		{
		//	dialog.Progress = e.ProgressPercentage;
			if (e.ProgressPercentage == 100)
			{
                Download_Completed = true;
                Android.Widget.Toast.MakeText(Android.App.Application.Context, _audioFileName + " saved to Downloads.", Android.Widget.ToastLength.Long).Show();
			}
		}

    }
}
