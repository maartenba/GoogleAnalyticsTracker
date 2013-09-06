using System;
using System.Web;

// ReSharper disable CheckNamespace
namespace GoogleAnalyticsTracker{
	// ReSharper restore CheckNamespace
	public static class PageViewTrackerExtensions {
		public static void TrackPageView(this Tracker tracker, HttpContextBase httpContext, string pageTitle) {
			TrackPageView(tracker, httpContext, pageTitle, httpContext.Request.Url.PathAndQuery);
		}

		public static void TrackPageView(this Tracker tracker, HttpContextBase httpContext, string pageTitle, string pageUrl) {
			var request = httpContext.Request;
			tracker.TrackPageView(pageTitle, pageUrl,
				hostname: request.Url.Host,
				userAgent: request.UserAgent,
				language: request.UserLanguages != null ? string.Join(";", request.UserLanguages) : ""
			);
		}

		public static void TrackPageViewAsync(this Tracker tracker, HttpContextBase httpContext, string pageTitle) {
			TrackPageViewAsync(tracker, httpContext, pageTitle, httpContext.Request.Url.PathAndQuery);
		}

		public static void TrackPageViewAsync(this Tracker tracker, HttpContextBase httpContext, string pageTitle, string pageUrl) {
			var request = httpContext.Request;
			tracker.TrackPageViewAsync(pageTitle, pageUrl,
				hostname: request.Url.Host,
				userAgent: request.UserAgent,
				language: request.UserLanguages != null ? string.Join(";", request.UserLanguages) : ""
			);
		}
	}
}