using System;
using Newtonsoft.Json;
using VadodaraByFoot.CustomControls;
using VadodaraByFoot.Interface;
using Xamarin.Forms;

namespace VadodaraByFoot
{
    public class AppStatics
    {
        public static void Loading(ActivityIndicator _progressoverlay, bool _runningStatus)
        {
            _progressoverlay.IsRunning = _runningStatus;
        }
        public static bool CheckInternetConnection()
        {
            var networkConnection = DependencyService.Get<IInternetConnectivity>();
            return networkConnection.IsInternetConnected();
        }
        public static void CheckMaxLengthEntry(object sender)
        {
            Entry entry = sender as Entry;
            string EntryText = entry.Text;
            string Maxlength = entry.ClassId;
            if (EntryText.Length > Int32.Parse(Maxlength))//If it is more than your character restriction
            {
                EntryText = EntryText.Remove(EntryText.Length - 1);// Remove Last character 
                entry.Text = EntryText; //Set the Old value
            }
        }

        public static int DeviceWidth;
        public static int DeviceHeight;
        public static int ActionBarHeight;
        public static int DeviceHeightWithoutActionBar = 0;
        public static string CurrentPage = "";

        public static string AndroidScreenResolutionType;

        #region Manage isolated data

        public static UserData LoadIsolatedData()
        {
            var data = DependencyService.Get<IIsolatedStorage>().LoadData("user.json");
            if (data != null)
            {
                UserData userdata = JsonConvert.DeserializeObject<UserData>(data);
                return userdata;
            }
            else
                return null;
        }
        public static void SaveIsolatedData(UserData userdata)
        {
            if (userdata != null)
            {
                var json = JsonConvert.SerializeObject(userdata);
                DependencyService.Get<IIsolatedStorage>().SaveData("user.json", json);
            }
        }
        public static void ClearIsolatedData()
        {
            DependencyService.Get<IIsolatedStorage>().ClearData("user.json");
           
        }
		#endregion
		#region Set FontSIze of controls
		public static int GetFontSizeSmall()
		{
			#region ios
			if (TargetPlatform.iOS == Device.OS)
			{
				if (Device.Idiom == TargetIdiom.Tablet)
				{
					if (((DeviceHeight * 2) >= 1024) && ((DeviceHeight * 2) < 2048)) return 15; //ipad, ipad2, ipad mini
					else if (((DeviceHeight * 2) >= 2048) && ((DeviceHeight * 2) < 2732)) return 15; //ipad air, mini retina
					else if ((DeviceHeight * 2) >= 2732) return 15; // ipad pro
				}
				else if (Device.Idiom == TargetIdiom.Phone)
				{
					if (((DeviceHeight * 2) >= 960) && ((DeviceHeight * 2) < 1136)) return 12; //4, 4s, ipod touch 4G
					else if (((DeviceHeight * 2) >= 1136) && ((DeviceHeight * 2) < 1334)) return 13; //5c, 5s, 5, ipoad touch 5G , SE                   
					else if (((DeviceHeight * 2) >= 1334) && ((DeviceHeight * 2) < 1920)) return 14; //6, 7, 6s
					else if (((DeviceHeight * 2) >= 1920) && ((DeviceHeight * 2) < 2248)) return 13; //6+, 6s+
					else if ((DeviceHeight * 2) >= 2248) return 14; //7+
				}
			}
			#endregion
			#region android
			else if (TargetPlatform.Android == Device.OS)
			{
                if (AndroidScreenResolutionType == "ldpi") return 10;
                if (AndroidScreenResolutionType == "mdpi") return 11;
                if (AndroidScreenResolutionType == "hdpi") return 13;
                if (AndroidScreenResolutionType == "xhdpi") return 14;
                if (AndroidScreenResolutionType == "xxhdpi") return 16;
                if (AndroidScreenResolutionType == "xxxhdpi") return 16;
				/*if (Device.Idiom == TargetIdiom.Tablet)
				{ return 13; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 14; }*/
			}
			#endregion
			return 10;
		}
		public static int GetFontSizeMedium()
		{
			#region ios
			if (TargetPlatform.iOS == Device.OS)
			{
				//if (Device.Idiom == TargetIdiom.Tablet)
				//{ return 16; }
				//else if (Device.Idiom == TargetIdiom.Phone)
				//{ return 12; }
				if (Device.Idiom == TargetIdiom.Tablet)
				{
					if (((DeviceHeight * 2) >= 1024) && ((DeviceHeight * 2) < 2048)) return 16; //ipad, ipad2, ipad mini
                    else if (((DeviceHeight * 2) >= 2048) && ((DeviceHeight * 2) < 2732)) return 16; //ipad air, mini retina
				}
				else if (Device.Idiom == TargetIdiom.Phone)
				{
					if (((DeviceHeight * 2) >= 960) && ((DeviceHeight * 2) < 1136)) return 14; //4, 4s, ipod touch 4G
					else if (((DeviceHeight * 2) >= 1136) && ((DeviceHeight * 2) < 1334)) return 14; //5c, 5s, 5, ipoad touch 5G , SE                   
					else if (((DeviceHeight * 2) >= 1334) && ((DeviceHeight * 2) < 1920)) return 15; //6, 7, 6s
					else if (((DeviceHeight * 2) >= 1920) && ((DeviceHeight * 2) < 2248)) return 14; //6+, 6s+
					else if ((DeviceHeight * 2) >= 2248) return 15; //7+
				}
			}
			#endregion
			#region android
			else if (TargetPlatform.Android == Device.OS)
			{
				if (AndroidScreenResolutionType == "ldpi") return 13;
				if (AndroidScreenResolutionType == "mdpi") return 14;
				if (AndroidScreenResolutionType == "hdpi") return 15;
				if (AndroidScreenResolutionType == "xhdpi") return 16;
				if (AndroidScreenResolutionType == "xxhdpi") return 18;
				if (AndroidScreenResolutionType == "xxxhdpi") return 18;
				/*if (Device.Idiom == TargetIdiom.Tablet)
				{ return 15; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 16; }*/
			}
			#endregion
			return 10;
		}
        public static int GetFontSizeTitle()
        {
            #region ios
            if (TargetPlatform.iOS == Device.OS)
            {
                //if (Device.Idiom == TargetIdiom.Tablet)
                //{ return 17; }
                //else if (Device.Idiom == TargetIdiom.Phone)
                //{ return 13; }
                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    if (((DeviceHeight * 2) >= 1024) && ((DeviceHeight * 2) < 2048)) return 17; //ipad, ipad2, ipad mini
                    else if (((DeviceHeight * 2) >= 2048) && ((DeviceHeight * 2) < 2732)) return 17; //ipad air, mini retina
                    else if ((DeviceHeight * 2) >= 2732) return 17; // ipad pro
                }
                else if (Device.Idiom == TargetIdiom.Phone)
                {
                    if (((DeviceHeight * 2) >= 960) && ((DeviceHeight * 2) < 1136)) return 14; //4, 4s, ipod touch 4G
                    else if (((DeviceHeight * 2) >= 1136) && ((DeviceHeight * 2) < 1334)) return 14; //5c, 5s, 5, ipoad touch 5G , SE                   
                    else if (((DeviceHeight * 2) >= 1334) && ((DeviceHeight * 2) < 1920)) return 16; //6, 7, 6s
                    else if (((DeviceHeight * 2) >= 1920) && ((DeviceHeight * 2) < 2248)) return 16; //6+, 6s+
                    else if ((DeviceHeight * 2) >= 2248) return 16; //7+
                }
            }
			#endregion
			#region android
			else if (TargetPlatform.Android == Device.OS)
			{
				if (AndroidScreenResolutionType == "ldpi") return 15;
				if (AndroidScreenResolutionType == "mdpi") return 16;
				if (AndroidScreenResolutionType == "hdpi") return 17;
				if (AndroidScreenResolutionType == "xhdpi") return 18;
				if (AndroidScreenResolutionType == "xxhdpi") return 20;
				if (AndroidScreenResolutionType == "xxxhdpi") return 20;
				/*if (Device.Idiom == TargetIdiom.Tablet)
				{ return 17; }
				else if (Device.Idiom == TargetIdiom.Phone)
				{ return 18; }*/
			}
			#endregion
			return 15;
		}
        #endregion
    }
}
