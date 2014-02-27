using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.WebApi
{
	public static class PageViewTrackerExtensions
    {
        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle)
        {
			return await TrackPageViewAsync(tracker, httpRequest, pageTitle, httpRequest.RequestUri.PathAndQuery);
		}

		public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpRequestMessage httpRequest, string pageTitle, string pageUrl)
		{
		    var beaconParameters = new Dictionary<string, string>();
            beaconParameters.Add(BeaconParameter.HostName, httpRequest.RequestUri.Host);
            beaconParameters.Add(BeaconParameter.Browser.Language, httpRequest.Headers.AcceptLanguage.ToString());
		    if (httpRequest.Headers.Referrer != null)
		    {
		        beaconParameters.Add(BeaconParameter.Browser.ReferralUrl, httpRequest.Headers.Referrer.ToString());
		    }

		    return await tracker.TrackPageViewAsync(pageTitle, pageUrl,
                userAgent: httpRequest.Headers.UserAgent.ToString(),
                beaconParameters: beaconParameters
			);
		}
	}
}