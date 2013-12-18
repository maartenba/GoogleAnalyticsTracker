using System.Net.Http;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.WebApi
{
	public static class PageViewTrackerExtensions
    {
        public static Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle)
        {
			return TrackPageViewAsync(tracker, httpRequest, pageTitle, httpRequest.RequestUri.PathAndQuery);
		}

		public static Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle, string pageUrl) 
        {
			return tracker.TrackPageViewAsync(pageTitle, pageUrl,
				hostname: httpRequest.RequestUri.Host,
				userAgent: httpRequest.Headers.UserAgent.ToString(),
				language: httpRequest.Headers.AcceptLanguage.ToString()
			);
		}
	}
}