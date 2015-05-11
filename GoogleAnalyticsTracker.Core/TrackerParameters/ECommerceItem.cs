using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class ECommerceItem : GeneralParameters, IECommerceParameters, IECommerceItemParameters
    {
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType
        {
            get { return HitType.Item; }
        }

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
        public string CurrencyCode { get; set; }

        #endregion

        #region Implementation of IECommerceItemParameters

        /// <summary>
        /// Specifies the item name.
        /// <remarks>Required for item hit type.</remarks>
        /// <example>Shoe</example>
        /// </summary>
        [Beacon("in", true)]
        public string ItemName { get; set; }

        /// <summary>
        /// Specifies the price for a single item / unit.
        /// <remarks>Optional</remarks>
        /// <example>3.50</example>
        /// </summary>
        [Beacon("ip")]
        public decimal ItemPrice { get; set; }

        /// <summary>
        /// Specifies the number of items purchased.
        /// <remarks>Optional</remarks>
        /// <example>4</example>
        /// </summary>
        [Beacon("iq")]
        public long ItemQuantity { get; set; }

        /// <summary>
        /// Specifies the SKU or item code.
        /// <remarks>Optional</remarks>
        /// <example>SKU47</example>
        /// </summary>
        [Beacon("ic")]
        public string ItemCode { get; set; }

        /// <summary>
        /// Specifies the category that the item belongs to.
        /// <remarks>Optional</remarks>
        /// <example>Blue</example>
        /// </summary>
        [Beacon("iv")]
        public string ItemCategory { get; set; }

        #endregion
    }
}