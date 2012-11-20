using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker
{
    public partial class Tracker
    {
        public Task<TrackingResult> TrackPageViewAsync(string pageTitle, string pageUrl)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("AnalyticsVersion", AnalyticsVersion);
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
                parameters.Add("utme", utme);

            return RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters);
        }

        public Task<TrackingResult> TrackEventAsync(string category, string action, string label, int value)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("AnalyticsVersion", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmni", "1");
            parameters.Add("utmt", "event");

            var utme = _utmeGenerator.Generate();
            parameters.Add("utme", string.Format("5({0}*{1}*{2})({3})", category, action, label ?? "", value) + utme);

            parameters.Add("utmcs", CharacterSet);
            parameters.Add("utmul", Language);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            return RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters);
        }

        public Task<TrackingResult> TrackTransactionAsync(string orderId, string storeName, string total, string tax, string shipping, string city, string region, string country)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("AnalyticsVersion", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmt", "event");
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

            return RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters);
        }
    }
}