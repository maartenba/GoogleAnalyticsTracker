using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Simple
{
    public static class ScreenviewTrackerExtension
    {
        public static async Task<TrackingResult> TrackScreenviewAsync(this SimpleTracker tracker, string appName,
            string appId, string appVersion, string appInstallerId, string screenName)
        {
            var screenviewParamenters = new ScreenviewTracking
            {
                ApplicationName = appName,
                ApplicationId = appId,
                ApplicationVersion = appVersion,
                ApplicationInstallerId = appInstallerId,
                ScreenName = screenName,
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster()
            };

            return await tracker.TrackAsync(screenviewParamenters);
        }
    }
}
