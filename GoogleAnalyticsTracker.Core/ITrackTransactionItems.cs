using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public interface ITrackTransactionItems
    {
        Task<TrackingResult> TrackTransactionItemAsync(string orderId, string productId, string productName, string productVariation,
						string productPrice, string quantity, string hostname = null, string userAgent = null, string characterSet = null, string language = null, string refererUrl =null);
    }
}