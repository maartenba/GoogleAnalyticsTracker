using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public interface ITrackTransactions
    {
        Task<TrackingResult> TrackTransactionAsync(string orderId, string storeName, string total, string tax, string shipping, string city,
                        string region, string country, Dictionary<string, string> beaconParameters = null, string userAgent = null);
    }
}