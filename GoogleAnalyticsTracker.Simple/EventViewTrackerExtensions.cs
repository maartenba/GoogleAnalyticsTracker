using System.Collections.Generic;
using System.Threading.Tasks;

using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Simple
{
    [PublicAPI]
    public static class EventTrackerExtensions
    {
        public static async Task<TrackingResult> TrackEventAsync(this SimpleTracker tracker, string category, string action, string label, IDictionary<int, string> customDimensions, IDictionary<int,long?> customMetrics = null, long value = 1)
        {
            var eventTrackingParameters = new EventTracking
            {
                Category = category,
                Action = action,
                Label = label,
                Value = value
            };

            eventTrackingParameters.SetCustomDimensions(customDimensions);
            if (customMetrics != null) {
                eventTrackingParameters.SetCustomMetrics(customMetrics);
            }

            return await tracker.TrackAsync(eventTrackingParameters);
        }
    }
}
