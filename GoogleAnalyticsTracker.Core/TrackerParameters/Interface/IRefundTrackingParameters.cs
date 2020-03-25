using System.Collections.Generic;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    public interface IRefundTrackingParameters
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

        /// <summary>
        /// Specifies the product list to refund.
        /// <remarks>Optional</remarks>
        /// </summary>
        List<IEnhancedECommerceProduct> Products { get; set; }
    }
}