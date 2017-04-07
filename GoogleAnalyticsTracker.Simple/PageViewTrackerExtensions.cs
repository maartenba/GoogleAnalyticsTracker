using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Simple
{
    public static class PageViewTrackerExtensions
    {        
        public static async Task<TrackingResult> TrackPageViewAsync(this SimpleTracker tracker, string pageTitle, string pageUrl, IDictionary<int,string> customDimensions)
        {
            var pageViewParameters = new PageView
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                DocumentHostName = tracker.Hostname,
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster()
            };

            pageViewParameters.SetCustomDimensions(customDimensions);

            return await tracker.TrackAsync(pageViewParameters);
        }
    }
}