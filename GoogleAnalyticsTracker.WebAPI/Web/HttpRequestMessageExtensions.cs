using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace GoogleAnalyticsTracker.Web {
	public static class HttpRequestMessageExtensions {
		public static string GetDeserializedCookieValue(this HttpRequestMessage request, string key) {
			if (request != null) {
				var cookies = request.Headers.GetCookies(key);
				return cookies.Where(c=>!String.IsNullOrEmpty(c[key].Value)).Select(c=>c[key].Value).FirstOrDefault();
			}
			return null;
		}

		public static void SetSerializedCookieValue(this HttpResponseMessage response, string key, string value) {
			if (response != null) {
				var cookieValue = new CookieHeaderValue(key, value);
				response.Headers.AddCookies(new CookieHeaderValue[] {cookieValue});
			}
		}

		public static void SetSerializedCookieValue(this HttpResponseMessage response, string key, int value) {
			response.SetSerializedCookieValue(key, value.ToString());
		}
	}
}
