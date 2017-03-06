using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Simple
{
    public static class PageViewTrackerExtensions
    {        
        public static async Task<TrackingResult> TrackPageViewAsync(this SimpleTracker tracker, string pageTitle, string pageUrl)
        {
            var pageViewParameters = new PageView
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster()
            };

            return await tracker.TrackAsync(pageViewParameters);
        }
    }
}