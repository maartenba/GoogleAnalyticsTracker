using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    public class ECommerceTransaction : GeneralParameters, IECommerceParameters, IECommerceTransactionParameters
    {
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType => HitType.Transaction;

        #endregion

        #region Implementation of IECommerceParameters

        /// <summary>
        /// A unique identifier for the transaction. 
        /// This value should be the same for both the Transaction hit and Items hits associated to the particular transaction.
        /// <remarks>Required for both transaction and item hit type.</remarks>
        /// <example>OD564</example>
        /// </summary>
        [Beacon("ti", true)]
        public string TransactionId { get; set; }

        /// <summary>
        /// When present indicates the local currency for all transaction currency values. 
        /// Value should be a valid ISO 4217 currency code.
        /// <remarks>Optional</remarks>
        /// <example>EUR</example>
        /// </summary>
        [Beacon("cu")]
        public string CurrencyCode { get; set; } //TODO: Implement enum based

        #endregion

        #region Implementation of IECommerceTransactionParameters

        /// <summary>
        /// Specifies the affiliation or store name.
        /// <remarks>Optional</remarks>
        /// <example>Member</example>
        /// </summary>
        [Beacon("ta")]
        public string TransactionAffiliation { get; set; }

        /// <summary>
        /// Specifies the total revenue associated with the transaction. This value should include any shipping or tax costs.
        /// <remarks>Optional</remarks>
        /// <example>15.47</example>
        /// </summary>
        [Beacon("tr")]
        public decimal TransactionRevenue { get; set; }

        /// <summary>
        /// Specifies the total shipping cost of the transaction.
        /// <remarks>Optional</remarks>
        /// <example>3.50</example>
        /// </summary>
        [Beacon("ts")]
        public decimal TransactionShipping { get; set; }

        /// <summary>
        /// Specifies the total tax of the transaction.
        /// <remarks>Optional</remarks>
        /// <example>11.20</example>
        /// </summary>
        [Beacon("tt")]
        public decimal TransactionTax { get; set; }

        #endregion
    }
}