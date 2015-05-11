using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Core.Interface
{
    public interface ITrackEvents
    {
        Task<TrackingResult> TrackEventAsync(EventTracking eventTracking);        
    }
}