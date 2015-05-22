using System.Threading.Tasks;

namespace GoogleAnalyticsTracker
{
    public interface ITrackTransactionItems
    {
        Task<TrackingResult> TrackTransactionItemAsync(string orderId, string productId, string productName,
            string productVariation, string productPrice, string quantity);
    }
}