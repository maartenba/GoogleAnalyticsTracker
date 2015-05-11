using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Core.Interface
{
    public interface ITrackUserTiming
    {
        Task<TrackingResult> TrackUserTimingAsync(UserTimings userTimings);
    }
}