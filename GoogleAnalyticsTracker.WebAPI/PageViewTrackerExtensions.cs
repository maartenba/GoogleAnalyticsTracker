using System.Net.Http;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.WebApi
{
	public static class PageViewTrackerExtensions
    {        
        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle, string pageUrl = null)
        {
            var pageViewParameters = new PageView
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                UserAgent = httpRequest.Headers.UserAgent.ToString(),
                DocumentHostName = httpRequest.RequestUri.Host,
                UserLanguage = httpRequest.Headers.AcceptLanguage.ToString().ToLower(),
                ReferralUrl = httpRequest.Headers.Referrer != null ? httpRequest.Headers.Referrer.ToString() : null,
            };

            return await tracker.TrackAsync(pageViewParameters);
        }
	}
}