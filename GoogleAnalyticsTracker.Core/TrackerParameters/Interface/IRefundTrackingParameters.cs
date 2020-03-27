using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface IRefundTrackingParameters : IProvideProductsParameters
    {
        /// <summary>
        /// Specifies the transaction Id to refund. Must not be empty.
        /// <remarks>Required for refund</remarks>
        /// <example>T12345</example>
        /// </summary>
        string TransactionId { get; set; }

        /// <summary>
        /// Specifies the product action. Must not be empty.
        /// <remarks>Required for refund</remarks>
        /// </summary>
        ProductAction ProductAction { get; }
    }
}