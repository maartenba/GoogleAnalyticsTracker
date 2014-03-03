using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public interface ITrackEvents
    {
        Task<TrackingResult> TrackEventAsync(string category, string action, string label, int value, bool nonInteraction = false, Dictionary<string, string> beaconParameters = null, string userAgent = null);
        Task<TrackingResult> TrackEventAsync(string category, string action, Dictionary<string, string> beaconParameters = null, string userAgent = null);
    }
}