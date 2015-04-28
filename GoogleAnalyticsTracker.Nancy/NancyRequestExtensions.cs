using System;
using System.Linq;
using Nancy;

namespace GoogleAnalyticsTracker.Nancy
{
    public static class NancyRequestExtensions
    {
        public static string GetDeserializedCookieValue(this Request request, string key)
        {
            if (request != null)
            {
                var cookies = request.Cookies.Where(c => c.Key == key).ToList();
                return cookies.Where(c => !String.IsNullOrEmpty(c.Value)).Select(c => c.Value).FirstOrDefault();
            }
            return null;
        }

        public static void SetSerializedCookieValue(this Response response, string key, string value)
        {
            if (response != null)
            {
                response.WithCookie(key, value);
            }
        }

        public static void SetSerializedCookieValue(this Response response, string key, int value)
        {
            response.SetSerializedCookieValue(key, value.ToString());
        }
    }
}
