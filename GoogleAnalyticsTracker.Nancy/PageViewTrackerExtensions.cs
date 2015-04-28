using System.Collections.Generic;
using System.Threading.Tasks;
using Nancy;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.Nancy
{
    public static class PageViewTrackerExtensions
    {
        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, Request httpRequest, string pageTitle)
        {
            return await TrackPageViewAsync(tracker, httpRequest, pageTitle, string.Format("{0}?{1}",httpRequest.Url.Path,httpRequest.Url.Query));
        }

        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, Request httpRequest, string pageTitle, string pageUrl, Dictionary<string, string> beaconParameters = null)
        {
            var internalBeaconParameters = new Dictionary<string, string>();
            internalBeaconParameters.Add(BeaconParameter.HostName, httpRequest.Url.HostName);
            internalBeaconParameters.Add(BeaconParameter.Browser.Language, httpRequest.Headers.AcceptLanguage.ToString());
            if (httpRequest.Headers.Referrer != null)
            {
                internalBeaconParameters.Add(BeaconParameter.Browser.ReferralUrl, httpRequest.Headers.Referrer.ToString());
            }

            if (beaconParameters != null)
            {
                foreach (var beaconParameter in beaconParameters)
                {
                    internalBeaconParameters[beaconParameter.Key] = beaconParameter.Value;
                }
            }

            return await tracker.TrackPageViewAsync(pageTitle, pageUrl,
                userAgent: httpRequest .Headers.UserAgent.ToString(),
                beaconParameters: internalBeaconParameters
            );
        }
    }
}
