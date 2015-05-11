namespace GoogleAnalyticsTracker.Core.Interface
{
    public interface IAnalyticsSession
    {
        string GenerateSessionId();
        string GenerateCookieValue();
        string GenerateCacheBuster();
    }
}