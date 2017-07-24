using System;
namespace VadodaraByFoot.ServiceLayer
{
    public class WebServiceURL
    {

        #region User 
        public static string Url_userReg = "http://vadodarabyfoot.com/wp-jr-api/users/register";
        public static string Url_userLogin = "http://vadodarabyfoot.com/wp-jr-api/users/login";
        public static string Url_userForgotPass = "http://vadodarabyfoot.com/wp-jr-api/users/forgotpassword";
		#endregion

		#region MenuPages 
		public static string Url_FAQ = "http://vadodarabyfoot.com/wp-jr-api/posts?type=faqs";
        public static string Url_ContactUs = "http://vadodarabyfoot.com/wp-json/wpcf7/v1/forms/73";
		public static string Url_AboutUs = "http://vadodarabyfoot.com/wp-jr-api/pages/21";
        public static string Url_Feedback = "http://vadodarabyfoot.com/wp-json/wpcf7/v1/forms/77";

        #endregion
        #region    
       
        #endregion

        #region Walk Tour
        public static string Url_walktourlst = "http://vadodarabyfoot.com/wp-jr-api/posts?type=tour";
        public static string Url_walktourdetail = "http://vadodarabyfoot.com/wp-jr-api/posts/";
        #endregion

    }
}
