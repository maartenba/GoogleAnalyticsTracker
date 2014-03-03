using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public interface ITrackTransactionItems
    {
        Task<TrackingResult> TrackTransactionItemAsync(string orderId, string productId, string productName, string productVariation,
                        string productPrice, string quantity, Dictionary<string, string> beaconParameters = null, string userAgent = null);
    }
}