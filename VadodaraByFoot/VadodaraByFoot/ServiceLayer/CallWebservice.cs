using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Net;
namespace VadodaraByFoot.ServiceLayer
{
    public class CallWebservice
    {
        public static async Task<T> GetResponse_Post<T>(string URI, List<KeyValuePair<string, string>> values) where T : class
        {

            var authValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "YWRtaW46YWRtaW4kIzEyMw==");

            using (var httpClient = new HttpClient())
            {
                //	httpClient.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
                httpClient.BaseAddress = new Uri(URI);
                httpClient.DefaultRequestHeaders.Authorization = authValue;

                var request = new HttpRequestMessage(HttpMethod.Post, "");

                if (request.Headers.CacheControl == null)
                {
                    request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
                }
                request.Headers.CacheControl.NoCache = true;
                request.Content = new FormUrlEncodedContent(values);

                try
                {
                    var response = await httpClient.SendAsync(request);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrWhiteSpace(responseString))
                        {
                            return JsonConvert.DeserializeObject<T>(responseString);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }


            return null;
        }
		
        public static async Task<T> GetResponse_Get<T>(string URI) where T : class
        {
            
            using (var httpClient = new HttpClient())
            {
				var authValue = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "YWRtaW46YWRtaW4kIzEyMw==");
                //   httpClient.BaseAddress = new Uri(URI);
				httpClient.DefaultRequestHeaders.Authorization = authValue;
                var request = new HttpRequestMessage(HttpMethod.Get, "");
                if (request.Headers.CacheControl == null)
					request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
				request.Headers.CacheControl.NoCache = true;     
                var uri = new Uri(string.Format(URI, string.Empty));
                               
				try
				{
					var response = await httpClient.GetAsync(uri);
					if (response != null && response.IsSuccessStatusCode)
					{
						var responseString = await response.Content.ReadAsStringAsync();

						if (!string.IsNullOrWhiteSpace(responseString))
						{
                            return JsonConvert.DeserializeObject<T>(responseString);
						}
					}
				}
				catch (Exception ex)
				{
					return null;
				}

                return null;
				
			}


           
 
        }
    }
}

