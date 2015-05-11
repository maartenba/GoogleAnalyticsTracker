using System.Net.Http;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.WebAPI2
{
    public static class UserTimingExtensions
    {
        public static async Task<TrackingResult> TrackUserTimingAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle, string pageUrl, string category, string var, long value, string label = null)
        {
            var userTimingParameters = new UserTimings
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                UserAgent = httpRequest.Headers.UserAgent.ToString(),
                DocumentHostName = httpRequest.RequestUri.Host,
                UserLanguage = httpRequest.Headers.AcceptLanguage.ToString().ToLower(),
                ReferralUrl = httpRequest.Headers.Referrer != null ? httpRequest.Headers.Referrer.ToString() : null,
                UserTimingCategory = category,
                UserTimingVariable = var,
                UserTimingTime = value,
                UserTimingLabel = label,                
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster()
            };

            return await tracker.TrackUserTimingAsync(userTimingParameters);
        }
    }
}