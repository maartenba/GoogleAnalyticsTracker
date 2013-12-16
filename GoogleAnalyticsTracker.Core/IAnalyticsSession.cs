namespace GoogleAnalyticsTracker.Core
{
    public interface IAnalyticsSession
    {
        string GenerateSessionId();
        string GenerateCookieValue();
    }
}