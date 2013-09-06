namespace GoogleAnalyticsTracker
{
    public interface ITrackPageViews
    {
			void TrackPageView(string pageTitle, string pageUrl, string hostname = null, string userAgent = null, string characterSet = null, string language = null);
    }
}