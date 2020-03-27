using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface IECommerceTransactionParameters
    {
        /// <summary>
        /// Specifies the affiliation or store name.
        /// <remarks>Optional</remarks>
        /// <example>Member</example>
        /// </summary>        
        string TransactionAffiliation { get; set; }

        /// <summary>
        /// Specifies the total revenue associated with the transaction. This value should include any shipping or tax costs.
        /// <remarks>Optional</remarks>
        /// <example>15.47</example>
        /// </summary>        
        decimal TransactionRevenue { get; set; }

        /// <summary>
        /// Specifies the total shipping cost of the transaction.
        /// <remarks>Optional</remarks>
        /// <example>3.50</example>
        /// </summary>        
        decimal TransactionShipping { get; set; }

        /// <summary>
        /// Specifies the total tax of the transaction.
        /// <remarks>Optional</remarks>
        /// <example>11.20</example>
        /// </summary>        
        decimal TransactionTax { get; set; }
    }
}