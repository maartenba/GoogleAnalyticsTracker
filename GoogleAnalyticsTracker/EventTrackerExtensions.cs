namespace GoogleAnalyticsTracker
{
    public static class EventTrackerExtensions
    {
        public static void TrackEvent(this Tracker tracker, string category, string action)
        {
            tracker.TrackEvent(category, action, null, 1);
        }
    }
}