using System.Threading.Tasks;

namespace GoogleAnalyticsTracker
{
    public interface ITrackTransactions
    {
        Task<TrackingResult> TrackTransactionAsync(string orderId, string storeName, string total, string tax,
            string shipping, string city, string region, string country);
    }
}