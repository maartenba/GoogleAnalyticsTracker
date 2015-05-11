using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Core.Interface
{
    public interface ITrackPageViews
    {
        Task<TrackingResult> TrackPageViewAsync(PageView pageView);
    }
}