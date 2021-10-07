using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Simple;

[PublicAPI]
public static class ScreenviewTrackerExtension
{
    public static async Task<TrackingResult> TrackScreenviewAsync(this SimpleTracker tracker, string appName,
        string appId, string appVersion, string appInstallerId, string screenName, IDictionary<int, string?>? customDimensions = null, IDictionary<int,long?>? customMetrics = null)
    {
        var screenviewParameters = new ScreenviewTracking
        {
            ApplicationName = appName,
            ApplicationId = appId,
            ApplicationVersion = appVersion,
            ApplicationInstallerId = appInstallerId,
            ScreenName = screenName
        };

        screenviewParameters.SetCustomDimensions(customDimensions);
        if (customMetrics != null) {
            screenviewParameters.SetCustomMetrics(customMetrics);
        }

        return await tracker.TrackAsync(screenviewParameters);
    }
}