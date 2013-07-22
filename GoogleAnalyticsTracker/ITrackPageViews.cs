namespace GoogleAnalyticsTracker
{
    public interface ITrackPageViews
    {
        void TrackPageView(string pageTitle, string pageUrl);
    }
}