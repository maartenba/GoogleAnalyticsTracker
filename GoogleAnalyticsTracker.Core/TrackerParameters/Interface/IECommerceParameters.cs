using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface IECommerceParameters
    {
        /// <summary>
        /// A unique identifier for the transaction. 
        /// This value should be the same for both the Transaction hit and Items hits associated to the particular transaction.
        /// <remarks>Required for both transaction and item hit type.</remarks>
        /// <example>OD564</example>
        /// </summary>        
        string TransactionId { get; set; }

        /// <summary>
        /// When present indicates the local currency for all transaction currency values. 
        /// Value should be a valid ISO 4217 currency code.
        /// <remarks>Optional</remarks>
        /// <example>EUR</example>
        /// </summary>        
        string? CurrencyCode { get; set; } //TODO: Implement enum based
    }
}