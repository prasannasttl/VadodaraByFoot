using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Foundation;
using UIKit;
using Xamarin.Forms;
using VadodaraByFoot.Interface;
using VadodaraByFoot.iOS.DependencyService;

[assembly: Dependency(typeof(IsolatedStorageIos))]
namespace VadodaraByFoot.iOS.DependencyService
{
    public class IsolatedStorageIos :IIsolatedStorage
    {
		public void SaveData(string filename, string text)
		{
			var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var filePath = Path.Combine(documentsPath, filename);
			System.IO.File.WriteAllText(filePath, text);
		}
		public string LoadData(string filename)
		{
			var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var filePath = System.IO.Path.Combine(documentsPath, filename);
			if (System.IO.File.Exists(filePath))
			{
				return System.IO.File.ReadAllText(filePath);
			}
			return null;
		}
		public bool ClearData(string filename)
		{
			var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			var filePath = System.IO.Path.Combine(documentsPath, filename);
			System.IO.File.Delete(filePath);
			return (System.IO.File.Exists(filePath)) ? false : true;
		}
    }
}
