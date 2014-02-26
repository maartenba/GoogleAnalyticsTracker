using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public interface ITrackPageViews
    {
        Task<TrackingResult> TrackPageViewAsync(string pageTitle, string pageUrl, string hostname = null, string userAgent = null, string characterSet = null, string language = null, string refererUrl =null);
    }
}