using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GoogleAnalyticsTracker.WebAPI2.v1
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetDeserializedCookieValue(this HttpRequestMessage request, string key)
        {
            if (request == null) return null;

            var cookies = request.Headers.GetCookies(key);
            return cookies.Where(c => !string.IsNullOrEmpty(c[key].Value)).Select(c => c[key].Value).FirstOrDefault();
        }

        public static void SetSerializedCookieValue(this HttpResponseMessage response, string key, string value)
        {
            if (response == null) return;

            var cookieValue = new CookieHeaderValue(key, value);
            response.Headers.AddCookies(new[] { cookieValue });
        }

        public static void SetSerializedCookieValue(this HttpResponseMessage response, string key, int value)
        {
            response.SetSerializedCookieValue(key, value.ToString(CultureInfo.InvariantCulture));
        }
    }
}
