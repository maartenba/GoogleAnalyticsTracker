using System.Net.Http;
using System.Web;

namespace GoogleAnalyticsTracker.WebAPI2.Helpers
{
    internal static class WebApiHelper
    {
        public static HttpRequestMessage GetCurrentRequest()
        {
            var httpRequestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            return httpRequestMessage;
        }

        public static string GetClientIp(HttpRequestMessage request = null)
        {
            if (request == null)
            {
                request = GetCurrentRequest();
            }

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                var userHostAddress =
                    ((HttpContextWrapper) request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                return userHostAddress;
            }

            if (HttpContext.Current != null)
            {
                var userHostAddress = HttpContext.Current.Request.UserHostAddress;
                return userHostAddress;
            }

            return null;
        }
    }
}