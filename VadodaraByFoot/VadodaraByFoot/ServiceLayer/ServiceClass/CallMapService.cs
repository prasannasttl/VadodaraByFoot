using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static VadodaraByFoot.ServiceLayer.ServiceClass.MapResponse;

namespace VadodaraByFoot.ServiceLayer.ServiceClass
{
	public static class GooglePoints
	{
		/// <summary>
		/// Decode google style polyline coordinates.
		/// </summary>
		/// <param name="encodedPoints"></param>
		/// <returns></returns>
		public static List<LatiLong> Decode(string encodedPoints)
		{
			if (string.IsNullOrEmpty(encodedPoints))
				throw new ArgumentNullException("encodedPoints");

			List<LatiLong> obj = new List<LatiLong>();
			char[] polylineChars = encodedPoints.ToCharArray();
			int index = 0;

			int currentLat = 0;
			int currentLng = 0;
			int next5bits;
			int sum;
			int shifter;

			while (index < polylineChars.Length)
			{
				LatiLong Item = new LatiLong();
				// calculate next latitude
				sum = 0;
				shifter = 0;
				do
				{
					next5bits = (int)polylineChars[index++] - 63;
					sum |= (next5bits & 31) << shifter;
					shifter += 5;
				} while (next5bits >= 32 && index < polylineChars.Length);

				if (index >= polylineChars.Length)
					break;

				currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

				//calculate next longitude
				sum = 0;
				shifter = 0;
				do
				{
					next5bits = (int)polylineChars[index++] - 63;
					sum |= (next5bits & 31) << shifter;
					shifter += 5;
				} while (next5bits >= 32 && index < polylineChars.Length);

				if (index >= polylineChars.Length && next5bits >= 32)
					break;

				currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

				Item.lat = Convert.ToDouble(currentLat) / 1E5;
				Item.lng = Convert.ToDouble(currentLng) / 1E5;

				obj.Add(Item);
				return obj;
			}
			return null;
		}
	}

	public class GetResponseFromWebService
	{

		public static async Task<T> GetResponse<T>(string URI) where T : class
		{
			if (NetworkInterface.GetIsNetworkAvailable() == true)
			{
				HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
				httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Token", "dc642a7f5bd64912");

				var request = new HttpRequestMessage(HttpMethod.Get, URI);

				if (request.Headers.CacheControl == null)
				{
					request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
				}

				request.Headers.CacheControl.NoCache = true;

				//  request.Content = new FormUrlEncodedContent(values);

				try
				{
					var response1 = await httpClient.SendAsync(request);
					if (response1 != null && response1.IsSuccessStatusCode)
					{
						var responseString = await response1.Content.ReadAsStringAsync();

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
			else
			{

			}

			return null;
		}

		public static async Task<T> GetResponsePostData<T>(string URI, List<KeyValuePair<string, string>> values) where T : class
		{
			if (NetworkInterface.GetIsNetworkAvailable() == true)
			{
				HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
				httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Token", "dc642a7f5bd64912");

				var request = new HttpRequestMessage(HttpMethod.Post, URI);

				if (request.Headers.CacheControl == null)
				{
					request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
				}

				request.Headers.CacheControl.NoCache = true;

				request.Content = new FormUrlEncodedContent(values);

				try
				{
					var response1 = await httpClient.SendAsync(request);
					if (response1 != null && response1.IsSuccessStatusCode)
					{
						var responseString = await response1.Content.ReadAsStringAsync();

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
			else
			{

			}

			return null;
		}
		public static async Task<T> GetResponsePostData<T>(string URI, string values) where T : class
		{
			if (NetworkInterface.GetIsNetworkAvailable() == true)
			{
				HttpClient httpClient = new HttpClient();
				httpClient.DefaultRequestHeaders.IfModifiedSince = DateTimeOffset.Now;
				httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Token", "dc642a7f5bd64912");


				var request = new HttpRequestMessage(HttpMethod.Post, URI);

				if (request.Headers.CacheControl == null)
				{
					request.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue();
				}

				request.Headers.CacheControl.NoCache = true;
				request.Content = new StringContent(values, UTF8Encoding.UTF8);
				try
				{
					var response1 = await httpClient.SendAsync(request);
					if (response1 != null && response1.IsSuccessStatusCode)
					{
						var responseString = await response1.Content.ReadAsStringAsync();

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
			else
			{

			}

			return null;
		}

	}

}
