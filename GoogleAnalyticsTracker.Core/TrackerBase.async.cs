using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public partial class TrackerBase
    {
        public async Task<TrackingResult> TrackPageViewAsync(string pageTitle, string pageUrl, Dictionary<string, string> beaconParameters = null, string userAgent = null)
        {
            // Add defaults
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("utmwv", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmcs", CharacterSet);
            parameters.Add("utmul", Language);
            parameters.Add("utmdt", pageTitle);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmp", pageUrl);
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            var utme = _utmeGenerator.Generate();
            if (!string.IsNullOrEmpty(utme))
            {
                parameters.Add("utme", utme);
            }

            // Add beacon parameters (these always override defaults)
            if (beaconParameters != null)
            {
                foreach (var beaconParameter in beaconParameters)
                {
                    parameters[beaconParameter.Key] = beaconParameter.Value;
                }
            }

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userAgent ?? UserAgent);
        }

	    public async Task<TrackingResult> TrackEventAsync(string category, string action, string label, int value, bool nonInteraction = false, Dictionary<string, string> beaconParameters = null, string userAgent = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("utmwv", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmni", "1");
            parameters.Add("utmt", "event");
            var utme = _utmeGenerator.Generate();

            parameters.Add("utme", string.Format("5({0}*{1}*{2})({3})", category, action, label ?? "", value) + utme);
	        if (nonInteraction)
	        {
	            parameters.Add("utmi", "1");
	        }

	        parameters.Add("utmcs", CharacterSet);
            parameters.Add("utmul", Language);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            // Add beacon parameters (these always override defaults)
            if (beaconParameters != null)
            {
                foreach (var beaconParameter in beaconParameters)
                {
                    parameters[beaconParameter.Key] = beaconParameter.Value;
                }
            }

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userAgent ?? UserAgent);
        }

        public async Task<TrackingResult> TrackEventAsync(string category, string action, Dictionary<string, string> beaconParameters = null, string userAgent = null)
        {
            return await TrackEventAsync(category, action, null, 1, 
                beaconParameters: beaconParameters,
                userAgent: userAgent);
        }

        public async Task<TrackingResult> TrackTransactionAsync(string orderId, string storeName, string total, string tax, string shipping, string city, string region, string country, Dictionary<string, string> beaconParameters = null, string userAgent = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("utmwv", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmt", "transaction");
            parameters.Add("utmcs", CharacterSet);
            parameters.Add("utmul", Language);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            parameters.Add("utmtid", orderId);
            parameters.Add("utmtst", storeName);
            parameters.Add("utmtto", total);
            parameters.Add("utmttx", tax);
            parameters.Add("utmtsp", shipping);
            parameters.Add("utmtci", city);
            parameters.Add("utmtrg", region);
            parameters.Add("utmtco", country);

            // Add beacon parameters (these always override defaults)
            if (beaconParameters != null)
            {
                foreach (var beaconParameter in beaconParameters)
                {
                    parameters[beaconParameter.Key] = beaconParameter.Value;
                }
            }

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userAgent ?? UserAgent);
        }

		public async Task<TrackingResult> TrackTransactionItemAsync(string orderId, string productId, string productName, string productVariation, string productPrice, string quantity, Dictionary<string, string> beaconParameters = null, string userAgent = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("utmwv", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmt", "item");
            parameters.Add("utmcs", CharacterSet);
            parameters.Add("utmul", Language);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            parameters.Add("utmtid", orderId);
            parameters.Add("utmipc", productId);
            parameters.Add("utmipn", productName);
            parameters.Add("utmiva", productVariation);
            parameters.Add("utmipr", productPrice);
            parameters.Add("utmiqt", quantity);

            // Add beacon parameters (these always override defaults)
            if (beaconParameters != null)
            {
                foreach (var beaconParameter in beaconParameters)
                {
                    parameters[beaconParameter.Key] = beaconParameter.Value;
                }
            }

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userAgent ?? UserAgent);
        }
    }
}