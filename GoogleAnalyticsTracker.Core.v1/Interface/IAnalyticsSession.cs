namespace GoogleAnalyticsTracker.Core.v1.Interface
{
    public interface IAnalyticsSession
    {
        string GenerateSessionId();
        string GenerateCookieValue();
        string GenerateCacheBuster();
    }
}