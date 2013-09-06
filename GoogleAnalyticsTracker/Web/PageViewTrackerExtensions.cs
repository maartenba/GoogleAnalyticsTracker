using System;
using System.Web;
using System.Net.Http;

// ReSharper disable CheckNamespace
namespace GoogleAnalyticsTracker{
	// ReSharper restore CheckNamespace
	public static class PageViewTrackerExtensions {

		#region WebAPI extensions
		public static void TrackPageView(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle) {
			TrackPageView(tracker, httpRequest, pageTitle, httpRequest.RequestUri.PathAndQuery);
		}

		public static void TrackPageView(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle, string pageUrl) {
			tracker.TrackPageView(pageTitle, pageUrl,
				hostname: httpRequest.RequestUri.Host,
				userAgent: httpRequest.Headers.UserAgent.ToString(),
				language: httpRequest.Headers.AcceptLanguage.ToString()
			);
		}

		public static void TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle) {
			TrackPageViewAsync(tracker, httpRequest, pageTitle, httpRequest.RequestUri.PathAndQuery);
		}

		public static void TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle, string pageUrl) {
			tracker.TrackPageViewAsync(pageTitle, pageUrl,
				hostname: httpRequest.RequestUri.Host,
				userAgent: httpRequest.Headers.UserAgent.ToString(),
				language: httpRequest.Headers.AcceptLanguage.ToString()
			);
		}
		#endregion

		#region ASP.NET Classic extensions
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
		#endregion
	}
}