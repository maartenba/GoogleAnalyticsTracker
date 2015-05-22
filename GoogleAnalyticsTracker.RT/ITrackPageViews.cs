using System.Threading.Tasks;

namespace GoogleAnalyticsTracker
{
    public interface ITrackPageViews
    {
        Task<TrackingResult> TrackPageViewAsync(string pageTitle, string pageUrl);
    }
}