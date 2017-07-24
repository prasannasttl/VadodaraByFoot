using System;
using System.IO;
using Foundation;
using UIKit;
using VadodaraByFoot.Interface;
[assembly: Xamarin.Forms.Dependency(typeof(VadodaraByFoot.iOS.DependencyService.AudioDownloadsListIos))]

namespace VadodaraByFoot.iOS.DependencyService
{
    public class AudioDownloadsListIos : IAudioDownloadsList
    {
		public void GetListOfFiles(string DirectoryPath)
        {
            var directories = System.IO.Directory.EnumerateDirectories("./");
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
           // path = "/Users/xamarin-dev-pc/Library/Developer/CoreSimulator/Devices/E2AD562A-B584-43A6-B4AA-CE8F25EA1F4B/data/Containers/Data/Application/3CCDF694-5904-4149-842B-13EC7A52D7CB/Documents";
			directories = System.IO.Directory.EnumerateDirectories(path + "/..");
            var c = directories.GetEnumerator();
            var x = System.IO.Directory.EnumerateDirectories(c.Current + "/..");

			foreach (var directory in directories)
			{
				Console.WriteLine(directory);
			//	txtView.Text += directory + Environment.NewLine;
			}
        }
    }
}
