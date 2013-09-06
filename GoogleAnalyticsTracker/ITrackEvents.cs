namespace GoogleAnalyticsTracker
{
    public interface ITrackEvents
    {
			void TrackEvent(string category, string action, string label, int value, string hostname = null, string userAgent = null, string characterSet = null, string language = null);
    }
}