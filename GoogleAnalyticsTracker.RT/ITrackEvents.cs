using System.Threading.Tasks;

namespace GoogleAnalyticsTracker
{
    public interface ITrackEvents
    {
        Task<TrackingResult> TrackEventAsync(string category, string action, string label, int value);
    }
}