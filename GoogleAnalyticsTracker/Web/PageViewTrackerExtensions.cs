using System.Web;

// ReSharper disable CheckNamespace
namespace GoogleAnalyticsTracker
// ReSharper restore CheckNamespace
{
    public static class PageViewTrackerExtensions
    {
        public static void TrackPageView(this Tracker tracker, HttpContextBase httpContext, string pageTitle)
        {
            TrackPageView(tracker, httpContext, pageTitle, httpContext.Request.Url.ToString());
        }

        public static void TrackPageView(this Tracker tracker, HttpContextBase httpContext, string pageTitle, string pageUrl)
        {
            var request = httpContext.Request;
            tracker.Hostname = request.UserHostName;
            tracker.UserAgent = request.UserAgent;
            tracker.Language = request.UserLanguages != null ? string.Join(";", request.UserLanguages) : "";
            tracker.TrackPageView(pageTitle, pageUrl);
        }

        public static void TrackPageView(this Tracker tracker, System.Net.Http.HttpRequestMessage request , string pageTitle, string pageUrl)
        {
            tracker.Hostname = request.Headers.From;
            tracker.UserAgent = request.Headers.UserAgent != null ? string.Join(";", request.Headers.UserAgent) : "";
            tracker.Language = request.Headers.AcceptLanguage != null ? string.Join(";", request.Headers.AcceptLanguage) : "";
            tracker.TrackPageView(pageTitle, pageUrl);
        }
    }
}