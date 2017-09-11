using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using Nancy;

namespace GoogleAnalyticsTracker.Nancy
{
    public static class PageViewTrackerExtensions
    {
        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, Request httpRequest, string pageTitle)
        {
            return await TrackPageViewAsync(tracker, httpRequest, pageTitle, string.Format("{0}?{1}",httpRequest.Url.Path,httpRequest.Url.Query));
        }

        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, Request httpRequest, string pageTitle, string pageUrl, Dictionary<string, string> beaconParameters = null)
        {
            var pageViewParameters = new PageView
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                UserAgent = httpRequest.Headers.UserAgent,
                DocumentHostName = httpRequest.Url.HostName,
                UserLanguage = httpRequest.Headers.AcceptLanguage.ToString().ToLower(),
                ReferralUrl = httpRequest.Headers.Referrer,
            };

            return await tracker.TrackAsync(pageViewParameters);
        }
    }
}
