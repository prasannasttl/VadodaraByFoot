using System;
using System.Collections.Generic;
using VadodaraByFoot;
using VadodaraByFoot.CustomControls;
using Xamarin.Forms;
using VadodaraByFoot.Interface;
using Newtonsoft.Json;

namespace VadodaraByFoot.View.MenuModule
{
    public partial class MenuMaster : MasterDetailPage
    {
        #region variables
        List<MenuItem> menuList;
        #endregion

        public MenuMaster()
        {
            InitializeComponent();
            MenuList();
			SetFontSizeControls();
            LoggedInStatusCheck();
        }
        public void SetFontSizeControls()
        {
            lblLogIn_Out.FontSize = AppStatics.GetFontSizeTitle();
            lblUserName.FontSize = AppStatics.GetFontSizeMedium();
            lblUserNameEmailId.FontSize = AppStatics.GetFontSizeSmall();
        }
        #region Tap Events
        public void Handle_IsPresentedChanged(object sender, System.EventArgs e)
        {
            if (IsPresented == true)
            {
                DependencyService.Get<VadodaraByFoot.Interface.IDissmissKeyboard>().DismissKeyboard();
                LoggedInStatusCheck();
            }
        }
        public void btnCloseTapped(object sender, System.EventArgs e)
        {
            IsPresented = false;
        }
        public async void OnLoginLogoutTap(object sender, System.EventArgs e)
		{
             IsPresented = false;
			UserData _userdata = AppStatics.LoadIsolatedData();

			if (_userdata == null)
                 {
					var stack = Detail.Navigation.NavigationStack;
					if (stack[stack.Count - 1].GetType() != typeof(VadodaraByFoot.View.LoginModule.LoginPage))
					{
						Page displayPage = new VadodaraByFoot.View.LoginModule.LoginPage();
						await Detail.Navigation.PushAsync(displayPage);
					}
				 }
                 else
                 {
                    AppStatics.ClearIsolatedData();
					imgUser.Source = null;
                    imgUser.HeightRequest = 0;
                    lblUserName.HeightRequest = 0;
                    lblUserNameEmailId.HeightRequest = 0;
                    lblUserName.Text = null;
                    lblUserNameEmailId.Text = null;
                    await DisplayAlert(AppResources.AppResources.LMessage,AppResources.AppResources.LLogoutSucessful ,AppResources.AppResources.LOk);
                 }
            		
		}
        public void Menu_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            IsPresented = false;
            var objselected = e.SelectedItem as MenuItem;
            objselected.IsSelected = 1;
            int index = (lstMenu.ItemsSource as List<MenuItem>).IndexOf(e.SelectedItem as MenuItem);

            ShowMenuItemSelectedInList(index);
			if (objselected != null)
			{
                if (objselected.Title != VadodaraByFoot.AppStatics.CurrentPage)
                {
                    VadodaraByFoot.AppStatics.CurrentPage = objselected.Title;
                    Detail = new NavigationPage((Page)Activator.CreateInstance(objselected.TargetType));
                }
			}
        }
		
        #endregion

        #region Menu list binding
        public void MenuList()
        {
            lstMenu.ItemsSource = null;
            menuList = new List<MenuItem>();
            menuList.Add(new MenuItem() { Icon = "menu_drawer_home_icon.png", Title = AppResources.AppResources.MHome, IsSelected = 1 , TargetType = typeof(WalkTourModule.WalkToursListPage), TitleFontSize = AppStatics.GetFontSizeTitle() });
            menuList.Add(new MenuItem() { Icon = "menu_drawer_about_icon.png", Title = AppResources.AppResources.MAboutUs, IsSelected = 0 , TargetType = typeof(MenuPages.About_us) , TitleFontSize = AppStatics.GetFontSizeTitle() });
            menuList.Add(new MenuItem() { Icon = "menu_drawer_contact_icon.png", Title = AppResources.AppResources.MContactUs, IsSelected = 0 , TargetType = typeof(MenuPages.Contact_Us), TitleFontSize = AppStatics.GetFontSizeTitle() });
            menuList.Add(new MenuItem() { Icon = "menu_drawer_feedback_icon.png", Title = AppResources.AppResources.MFeedback, IsSelected = 0, TargetType = typeof(MenuPages.FeedBack), TitleFontSize = AppStatics.GetFontSizeTitle() });
          //  menuList.Add(new MenuItem() { Icon = "menu_drawer_share_icon.png", Title = AppResources.AppResources.MShare, IsSelected = 0 , TargetType = typeof(FAQs), TitleFontSize = AppStatics.GetFontSizeTitle() });
            menuList.Add(new MenuItem() { Icon = "menu_drawer_faqs_icon.png", Title = AppResources.AppResources.MFAQs, IsSelected = 0 , TargetType = typeof(FAQs), TitleFontSize = AppStatics.GetFontSizeTitle() });
            lstMenu.ItemsSource = menuList;
            VadodaraByFoot.AppStatics.CurrentPage = menuList[0].Title;
        }

       
        public void LoggedInStatusCheck()
        {
            UserData _userdata = AppStatics.LoadIsolatedData();

				if(_userdata == null)
                {
                    imgUser.HeightRequest = 0;
					lblUserName.HeightRequest = 0;
					lblUserNameEmailId.HeightRequest = 0;
                    lblLogIn_Out.Text = AppResources.AppResources.MLogin;
                }
                else
                {
                    if (string.IsNullOrEmpty(lblUserNameEmailId.Text))
                    {
                        imgUser.Source = _userdata.avatar;
                        lblUserName.Text = _userdata.first_name + " " + _userdata.last_name;
                        lblUserNameEmailId.Text = _userdata.email;
                        imgUser.HeightRequest = 90;
                        if (string.IsNullOrEmpty(_userdata.first_name) && string.IsNullOrEmpty(_userdata.last_name))
                           lblUserName.HeightRequest = 0;
                        else
                           lblUserName.HeightRequest = -1;
						lblUserNameEmailId.HeightRequest = -1;
                        lblLogIn_Out.Text = AppResources.AppResources.MLogout;
                    }
                }    
			}

        public void ShowMenuItemSelectedInList(int IsSelectedIndex)
        {
			lstMenu.ItemsSource = null;
			menuList = new List<MenuItem>();
            menuList.Add(new MenuItem() { Icon = "menu_drawer_home_icon.png", Title = AppResources.AppResources.MHome, IsSelected = 0, TargetType = typeof(WalkTourModule.WalkToursListPage), TitleFontSize= AppStatics.GetFontSizeTitle() });
			menuList.Add(new MenuItem() { Icon = "menu_drawer_about_icon.png", Title = AppResources.AppResources.MAboutUs, IsSelected = 0, TargetType = typeof(MenuPages.About_us), TitleFontSize = AppStatics.GetFontSizeTitle() });
			menuList.Add(new MenuItem() { Icon = "menu_drawer_contact_icon.png", Title = AppResources.AppResources.MContactUs, IsSelected = 0, TargetType = typeof(MenuPages.Contact_Us), TitleFontSize = AppStatics.GetFontSizeTitle() });
			menuList.Add(new MenuItem() { Icon = "menu_drawer_feedback_icon.png", Title = AppResources.AppResources.MFeedback, IsSelected = 0, TargetType = typeof(MenuPages.FeedBack), TitleFontSize = AppStatics.GetFontSizeTitle() });
		//	menuList.Add(new MenuItem() { Icon = "menu_drawer_share_icon.png", Title = AppResources.AppResources.MShare, IsSelected = 0, TargetType = typeof(FAQs), TitleFontSize = AppStatics.GetFontSizeTitle() });
			menuList.Add(new MenuItem() { Icon = "menu_drawer_faqs_icon.png", Title = AppResources.AppResources.MFAQs, IsSelected = 0, TargetType = typeof(FAQs), TitleFontSize = AppStatics.GetFontSizeTitle() });
            menuList[IsSelectedIndex].IsSelected = 1;
            lstMenu.ItemsSource = menuList;
          
        }
        #endregion
    }

   
}
