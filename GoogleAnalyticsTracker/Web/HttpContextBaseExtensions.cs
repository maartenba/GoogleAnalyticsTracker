using System.Web;
using Newtonsoft.Json;

namespace GoogleAnalyticsTracker.Web
{
    public static class HttpContextBaseExtensions
    {
        public static T GetDeserializedCookieValue<T>(this HttpContextBase context, string key)
        {
            if (context != null)
            {
                var cookie = context.Request.Cookies.Get(key);
                if (cookie != null)
                {
                    return JsonConvert.DeserializeObject<T>(cookie.Value);
                }
            }
            return default(T);
        }

        public static void SetSerializedCookieValue(this HttpContextBase context, string key, object value)
        {
            if (context != null)
            {
                context.Response.Cookies.Add(new HttpCookie(key, JsonConvert.SerializeObject(value)));
            }
        }
    }
}