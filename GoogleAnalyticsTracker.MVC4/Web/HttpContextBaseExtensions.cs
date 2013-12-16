using System.Web;

namespace GoogleAnalyticsTracker.Web
{
    public static class HttpContextBaseExtensions
    {
        public static string GetDeserializedCookieValue(this HttpContextBase context, string key)
        {
            if (context != null)
            {
                var cookie = context.Request.Cookies.Get(key);
                if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
                {
                    return cookie.Value;
                }
            }
            return null;
        }

        public static void SetSerializedCookieValue(this HttpContextBase context, string key, string value)
        {
            if (context != null)
            {
                context.Response.Cookies.Add(new HttpCookie(key, value));
            }
        }

        public static void SetSerializedCookieValue(this HttpContextBase context, string key, int value)
        {
            if (context != null)
            {
                context.Response.Cookies.Add(new HttpCookie(key, value.ToString()));
            }
        }
    }
}