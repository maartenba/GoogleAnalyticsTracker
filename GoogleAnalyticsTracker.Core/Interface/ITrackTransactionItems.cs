using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Core.Interface
{
    public interface ITrackECommerceItem
    {
        Task<TrackingResult> TrackTransactionItemAsync(ECommerceItem eCommerceItem);
    }
}