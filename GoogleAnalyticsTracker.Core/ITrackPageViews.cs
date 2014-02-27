using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public interface ITrackPageViews
    {
        Task<TrackingResult> TrackPageViewAsync(string pageTitle, string pageUrl, Dictionary<string, string> beaconParameters = null, string userAgent = null);
    }
}