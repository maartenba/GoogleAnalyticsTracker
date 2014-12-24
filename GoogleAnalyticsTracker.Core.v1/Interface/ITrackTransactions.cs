using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.v1.TrackerParameters;

namespace GoogleAnalyticsTracker.Core.v1.Interface
{
    public interface ITrackECommerceTransaction
    {
        Task<TrackingResult> TrackTransactionAsync(ECommerceTransaction eCommerceTransaction);
    }
}