using System.Net.Http;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using GoogleAnalyticsTracker.WebAPI2.Helpers;

namespace GoogleAnalyticsTracker.WebAPI2
{
    public static class PageViewTrackerExtensions
    {
        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, string pageTitle, string pageUrl = null)
        {
            var httpRequest = WebApiHelper.GetCurrentRequest();
            if (httpRequest == null)
            {
                // We can't do anything here
                return new TrackingResult
                {
                    Success = false
                };
            }

            return await TrackPageViewAsync(tracker, httpRequest, pageTitle, pageUrl);
        }

        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle, string pageUrl = null)
        {
            if (pageUrl == null)
            {
                pageUrl = httpRequest.RequestUri.AbsolutePath;
            }

            var pageViewParameters = new PageView
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                UserAgent = httpRequest.Headers.UserAgent.ToString(),
                DocumentHostName = httpRequest.RequestUri.Host,
                UserLanguage = httpRequest.Headers.AcceptLanguage.ToString().ToLower(),
                ReferralUrl = httpRequest.Headers.Referrer != null ? httpRequest.Headers.Referrer.ToString() : null,                
                IpOverride = WebApiHelper.GetClientIp(httpRequest)
            };

            return await tracker.TrackAsync(pageViewParameters);
        }
    }
}