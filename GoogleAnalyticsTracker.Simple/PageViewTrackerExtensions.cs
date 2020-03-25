using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Simple
{
    [PublicAPI]
    public static class PageViewTrackerExtensions
    {        
        public static async Task<TrackingResult> TrackPageViewAsync(this SimpleTracker tracker, string pageTitle, string pageUrl, IDictionary<int,string> customDimensions, IDictionary<int,long?> customMetrics = null)
        {
            var pageViewParameters = new PageView
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl
            };

            pageViewParameters.SetCustomDimensions(customDimensions);
            if (customMetrics != null) {
                pageViewParameters.SetCustomMetrics(customMetrics);
            }

            return await tracker.TrackAsync(pageViewParameters);
        }
    }
}
