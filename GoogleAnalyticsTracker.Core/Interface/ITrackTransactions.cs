using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Core.Interface
{
    public interface ITrackECommerceTransaction
    {
        Task<TrackingResult> TrackTransactionAsync(ECommerceTransaction eCommerceTransaction);
    }
}