using Android.Content;
using VadodaraByFoot.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using VadodaraByFoot.Droid.DependencyService;

[assembly: Dependency(typeof(IsolatedStorageAndroid))]
namespace VadodaraByFoot.Droid.DependencyService
{
    public class IsolatedStorageAndroid :IIsolatedStorage
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
