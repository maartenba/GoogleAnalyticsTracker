using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.WebApi2
{
	public static class PageViewTrackerExtensions
    {
        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle)
        {
			return await TrackPageViewAsync(tracker, httpRequest, pageTitle, httpRequest.RequestUri.PathAndQuery);
		}

        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle, string pageUrl, Dictionary<string, string> beaconParameters = null)
        {
            var internalBeaconParameters = new Dictionary<string, string>();
            internalBeaconParameters.Add(BeaconParameter.HostName, httpRequest.RequestUri.Host);
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
                userAgent: httpRequest.Headers.UserAgent.ToString(),
                beaconParameters: internalBeaconParameters
            );
        }
	}
}