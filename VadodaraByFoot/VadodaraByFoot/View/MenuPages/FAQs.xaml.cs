using System;
using System.Collections.Generic;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using VadodaraByFoot.ServiceLayer;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
namespace VadodaraByFoot.View
{
    public partial class FAQs : ContentPage
    {
        
        #region Variables
        ObservableCollection<Faqlist> ListFAQ = new ObservableCollection<Faqlist>();
        #endregion
        public FAQs()
        {
            InitializeComponent();
        }
        protected override  async void OnAppearing()
        {
           await CallWebserviceFAQ();
        }
        public async Task CallWebserviceFAQ()
        {
            if (AppStatics.CheckInternetConnection())
            {
                try
                {
                    RootObjectFAQ response = await CallWebservice.GetResponse_Get<RootObjectFAQ>(WebServiceURL.Url_FAQ);
                    if (response != null)
                    {
                        if (response._resultflag == "1")
                        {
                            for (int i = 0; i < response.faqlist.Count; i++)
                            {
                                var objFAQ = new Faqlist();
                                objFAQ.TapId = i.ToString();
                                objFAQ.expand_collapse = "faq_plus.png";
                                objFAQ.ObjIsVisible = false;
                                objFAQ.title = response.faqlist[i].title;
                                objFAQ.content = response.faqlist[i].content;
                                objFAQ.TitleFontSize = AppStatics.GetFontSizeMedium();
                                objFAQ.DetailFontSize = AppStatics.GetFontSizeSmall();
                                ListFAQ.Add(objFAQ);
                            }
                            lstFAQ.ItemsSource = ListFAQ;
                            AppStatics.Loading(loading, false);
                        }
                    }
                    else
                    {
                        AppStatics.Loading(loading, false);
                        await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LWebserverNotResponding, AppResources.AppResources.LOk);
                    }
                }
                catch (Exception e)
                {
                    AppStatics.Loading(loading, false);
                    await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LSomethingWentWrong, AppResources.AppResources.LOk);
                }
            }
			else
			{
                AppStatics.Loading(loading, false);
				await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LNoInternetConnection, AppResources.AppResources.LOk);
			}
			
        }
		public void img_Tapped(object sender, EventArgs args)
		{
			try
			{
				var img = sender as Image;
				var itemNumTapped = img.ClassId;
                int itemNum = Convert.ToInt32(itemNumTapped);
                if(ListFAQ[itemNum].ObjIsVisible)
                {
                    ListFAQ[itemNum].expand_collapse = "faq_plus.png";
                    ListFAQ[itemNum].ObjIsVisible = false;
                }
                else
                {
                    ListFAQ[itemNum].expand_collapse = "faq_minus.png";
                    ListFAQ[itemNum].ObjIsVisible = true;
                }
                lstFAQ.ItemsSource = null;
                lstFAQ.ItemsSource = ListFAQ;
			}
			catch (Exception ex)
			{
			//	var fileService = DependencyService.Get<ISaveException>();
			//	fileService.SaveTextAsync(App.FileName, ex.StackTrace);
			}
		}

        public void stck_Tapped(object sender, EventArgs args)
        {
			try
			{
                var img = sender as StackLayout;
				var itemNumTapped = img.ClassId;
				int itemNum = Convert.ToInt32(itemNumTapped);
				if (ListFAQ[itemNum].ObjIsVisible)
				{
					ListFAQ[itemNum].expand_collapse = "faq_plus.png";
					ListFAQ[itemNum].ObjIsVisible = false;
				}
				else
				{
					ListFAQ[itemNum].expand_collapse = "faq_minus.png";
					ListFAQ[itemNum].ObjIsVisible = true;
				}
				lstFAQ.ItemsSource = null;
				lstFAQ.ItemsSource = ListFAQ;
			}
			catch (Exception ex)
			{
				//  var fileService = DependencyService.Get<ISaveException>();
				//  fileService.SaveTextAsync(App.FileName, ex.StackTrace);
			}
        }
		public void lstFAQ_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			((ListView)sender).SelectedItem = null; // de-select the row   
		}
		#region Toolbar item tapped events
		public void Search_Clicked(object sender, System.EventArgs e)
		{

		}
		#endregion
		protected override bool OnBackButtonPressed()
		{
			Application.Current.MainPage = new VadodaraByFoot.View.MenuModule.MenuMaster();
			return true;
		}
	}
}
