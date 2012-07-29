using System.Web;

namespace GoogleAnalyticsTracker
{
    public class CookieBasedAnalyticsSession
        : AnalyticsSession, IAnalyticsSession
    {
        private const string CookieName = "_GATCOOKIE";
        private const string SessionCookieName = "_GATSESSION";

        protected HttpContextBase GetHttpContext()
        {
            if (HttpContext.Current != null)
            {
                return new HttpContextWrapper(HttpContext.Current);
            }
            return null;
        }

        public override string GenerateCookieValue()
        {
            var context = GetHttpContext();
            if (context != null)
            {
                var cookie = context.Request.Cookies.Get(CookieName);
                if (cookie != null)
                {
                    return cookie.Value;
                }
                else
                {
                    var cookieValue = base.GenerateCookieValue();
                    context.Response.Cookies.Add(new HttpCookie(CookieName, cookieValue));
                }
            }
            return base.GenerateCookieValue();
        }

        public override string GenerateSessionId()
        {
            var context = GetHttpContext();
            if (context != null)
            {
                var cookie = context.Request.Cookies.Get(SessionCookieName);
                if (cookie != null)
                {
                    return cookie.Value;
                }
                else
                {
                    var cookieValue = base.GenerateSessionId();
                    context.Response.Cookies.Add(new HttpCookie(SessionCookieName, cookieValue));
                }
            }
            return base.GenerateSessionId();
        }
    }
}