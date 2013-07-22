namespace GoogleAnalyticsTracker
{
    public interface ITrackEvents
    {
        void TrackEvent(string category, string action, string label, int value);
    }
}