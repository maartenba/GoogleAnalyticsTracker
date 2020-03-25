using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.Interface
{
    [PublicAPI]
    public interface IAnalyticsSession
    {
        string GenerateSessionId();
        string GenerateCacheBuster();
    }
}