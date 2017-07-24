using System;
namespace VadodaraByFoot.Interface
{
    public interface IAudioDownlaod
    {
        void Downlaod_mp3_Url(string url, string audioFileName, Xamarin.Forms.ActivityIndicator LoadingDownloadProgress, Xamarin.Forms.Label DownloadingLabel);
        bool Download_Completed { get; set; }
    }
}
