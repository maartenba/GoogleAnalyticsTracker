using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public interface ITrackEvents
    {
        Task<TrackingResult> TrackEventAsync(string category, string action, string label, int value, string hostname = null, string userAgent = null, string characterSet = null, string language = null);
        Task<TrackingResult> TrackEventAsync(string category, string action);
    }
}