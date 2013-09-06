using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker
{
    public partial class Tracker
    {
			public Task<TrackingResult> TrackPageViewAsync(string pageTitle, string pageUrl, string hostname = null, string userAgent = null, string characterSet = null, string language = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("utmwv", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", hostname??Hostname);
            parameters.Add("utmcs", characterSet ?? CharacterSet);
            parameters.Add("utmul", language ?? Language);
            parameters.Add("utmdt", pageTitle);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmp", pageUrl);
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            var utme = _utmeGenerator.Generate();
            if (!string.IsNullOrEmpty(utme))
                parameters.Add("utme", utme);

            return RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userAgent);
        }

			public Task<TrackingResult> TrackEventAsync(string category, string action, string label, int value, string hostname = null, string userAgent = null, string characterSet = null, string language = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("utmwv", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", hostname??Hostname);
            parameters.Add("utmni", "1");
            parameters.Add("utmt", "event");

            var utme = _utmeGenerator.Generate();
            parameters.Add("utme", string.Format("5({0}*{1}*{2})({3})", category, action, label ?? "", value) + utme);

            parameters.Add("utmcs", characterSet ?? CharacterSet);
            parameters.Add("utmul", language ?? Language);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            return RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userAgent);
        }

			public Task<TrackingResult> TrackTransactionAsync(string orderId, string storeName, string total, string tax, string shipping, string city, string region, string country, string hostname = null, string userAgent = null, string characterSet = null, string language = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("utmwv", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", hostname ?? Hostname);
            parameters.Add("utmt", "transaction");
            parameters.Add("utmcs", characterSet ?? CharacterSet);
            parameters.Add("utmul", language ?? Language);
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

            return RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userAgent);
        }

			public Task<TrackingResult> TrackTransactionItemAsync(string orderId, string productId, string productName, string productVariation, string productPrice, string quantity, string hostname = null, string userAgent = null, string characterSet = null, string language = null)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("utmwv", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", hostname ?? Hostname);
            parameters.Add("utmt", "item");
            parameters.Add("utmcs", characterSet ?? CharacterSet);
            parameters.Add("utmul", language ?? Language);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            parameters.Add("utmtid", orderId);
            parameters.Add("utmipc", productId);
            parameters.Add("utmipn", productName);
            parameters.Add("utmiva", productVariation);
            parameters.Add("utmipr", productPrice);
            parameters.Add("utmiqt", quantity);

            return RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userAgent);
        }
    }
}