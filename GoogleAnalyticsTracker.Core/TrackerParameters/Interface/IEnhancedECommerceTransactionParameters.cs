using System.Collections.Generic;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    public interface IEnhancedECommerceTransactionParameters : IECommerceTransactionParameters
    {
        /// <summary>
        /// Specifies the role of the products included in a hit.
        /// If a product action is not specified, all product definitions included with the hit will be ignored.
        /// <remarks>Optional</remarks>
        /// <example>purchase</example>
        /// </summary>        
        ProductAction ProductAction { get; set; }

        /// <summary>
        /// Specifies the transaction coupon redeemed with the transaction.
        /// <remarks>Optional. Can be sent when Product Action is set to 'purchase' or 'refund'.</remarks>
        /// <example>SUMMER08</example>
        /// </summary> 
        string CouponCode { get; set; }

        /// <summary>
        /// Specifies the list or collection from which a product action occurred.
        /// <remarks>Optional. Can be sent when Product Action is set to 'detail' or 'click'.</remarks>
        /// <example>Search Results</example>
        /// </summary> 
        string ProductActionList { get; set; }

        /// <summary>
        /// Specifies the list or collection from which a product action occurred.
        /// <remarks>Optional. Can be sent when Product Action is set to 'detail' or 'click'.</remarks>
        /// <example>2</example>
        /// </summary>
        int? CheckoutStep { get; set; }

        /// <summary>
        /// Additional information about a checkout step.
        /// <remarks>Optional. Can be sent when Product Action is set to 'checkout'.</remarks>
        /// <example>Visa</example>
        /// </summary>
        string CheckoutStepOption { get; set; }

        /// <summary>
        /// List of products to send in scope of this transaction.
        /// <remarks>Optional</remarks>
        /// </summary>
        List<IEnhancedECommerceProduct> Products { get; set; }
    }
}
