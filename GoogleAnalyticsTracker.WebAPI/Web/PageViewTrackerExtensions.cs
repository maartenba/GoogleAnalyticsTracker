using System;
using System.Web;
using System.Net.Http;

// ReSharper disable CheckNamespace
namespace GoogleAnalyticsTracker{
	// ReSharper restore CheckNamespace
	public static class PageViewTrackerExtensions {
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
	}
}