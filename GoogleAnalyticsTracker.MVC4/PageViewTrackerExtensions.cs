using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.Mvc4
{
	public static class PageViewTrackerExtensions
    {
        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpContextBase httpContext, string pageTitle) 
        {
			return await TrackPageViewAsync(tracker, httpContext, pageTitle, httpContext.Request.Url.PathAndQuery);
		}

        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpContextBase httpContext, string pageTitle, string pageUrl)
        {
            var request = httpContext.Request;

            var beaconParameters = new Dictionary<string, string>();
            beaconParameters.Add(BeaconParameter.HostName, request.Url.Host);
            if (request.UserLanguages != null)
            {
                beaconParameters.Add(BeaconParameter.Browser.Language, string.Join(";", request.UserLanguages));
            }
            if (request.UrlReferrer != null)
            {
                beaconParameters.Add(BeaconParameter.Browser.ReferralUrl, request.UrlReferrer.ToString());
            }

            return await tracker.TrackPageViewAsync(pageTitle, pageUrl,
                userAgent: request.UserAgent,
                beaconParameters: beaconParameters);
        }
	}
}