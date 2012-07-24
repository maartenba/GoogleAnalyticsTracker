namespace GoogleAnalyticsTracker
{
    public interface IAnalyticsSession
    {
        string GenerateSessionId();
        string GenerateCookieValue();
    }
}