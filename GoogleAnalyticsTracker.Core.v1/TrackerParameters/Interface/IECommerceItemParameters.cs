namespace GoogleAnalyticsTracker.Core.v1.TrackerParameters.Interface
{
    public interface IECommerceItemParameters
    {
        /// <summary>
        /// Specifies the item name.
        /// <remarks>Required for item hit type.</remarks>
        /// <example>Shoe</example>
        /// </summary>        
        string ItemName { get; set; }

        /// <summary>
        /// Specifies the price for a single item / unit.
        /// <remarks>Optional</remarks>
        /// <example>3.50</example>
        /// </summary>        
        decimal ItemPrice { get; set; }

        /// <summary>
        /// Specifies the number of items purchased.
        /// <remarks>Optional</remarks>
        /// <example>4</example>
        /// </summary>        
        long ItemQuantity { get; set; }

        /// <summary>
        /// Specifies the SKU or item code.
        /// <remarks>Optional</remarks>
        /// <example>SKU47</example>
        /// </summary>        
        string ItemCode { get; set; }

        /// <summary>
        /// Specifies the category that the item belongs to.
        /// <remarks>Optional</remarks>
        /// <example>Blue</example>
        /// </summary>        
        string ItemCategory { get; set; }
    }
}