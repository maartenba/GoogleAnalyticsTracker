using System.Threading.Tasks;
using System.Web;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.MVC4
{
	public static class PageViewTrackerExtensions
    {
        public static Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpContextBase httpContext, string pageTitle) 
        {
			return TrackPageViewAsync(tracker, httpContext, pageTitle, httpContext.Request.Url.PathAndQuery);
		}

		public static Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpContextBase httpContext, string pageTitle, string pageUrl) 
        {
			var request = httpContext.Request;
			return tracker.TrackPageViewAsync(pageTitle, pageUrl,
				hostname: request.Url.Host,
				userAgent: request.UserAgent,
				language: request.UserLanguages != null ? string.Join(";", request.UserLanguages) : "");
		}
	}
}