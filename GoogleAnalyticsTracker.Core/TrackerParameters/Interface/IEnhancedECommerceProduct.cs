using System.Collections.Generic;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    public interface IEnhancedECommerceProduct
    {
        /// <summary>
        /// The SKU or identifier of the product.
        /// <remarks>Optional</remarks>
        /// <example>P12345</example>
        /// </summary>
        string Sku { get; set; }

        /// <summary>
        /// The name of the product.
        /// <remarks>Optional</remarks>
        /// <example>Android T-Shirt</example>
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The brand associated with the product.
        /// <remarks>Optional</remarks>
        /// <example>Google</example>
        /// </summary>
        string Brand { get; set; }

        /// <summary>
        /// The category to which the product belongs.
        /// <remarks>Optional. Can be hierarchical with the "/" delimiter up to 5 levels.</remarks>
        /// <example>Apparel/Mens/T-Shirts</example>
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// The variant of the product.
        /// <remarks>Optional</remarks>
        /// <example>Black</example>
        /// </summary>
        string Variant { get; set; }

        /// <summary>
        /// The unit price of a product.
        /// <remarks>Optional</remarks>
        /// <example>29.20</example>
        /// </summary>
        decimal? Price { get; set; }

        /// <summary>
        /// The quantity of a product.
        /// <remarks>Optional</remarks>
        /// <example>2</example>
        /// </summary>
        int? Quantity { get; set; }

        /// <summary>
        /// The coupon code associated with a product.
        /// <remarks>Optional</remarks>
        /// <example>SUMMER_SALE13</example>
        /// </summary>
        string ProductCouponCode { get; set; }

        /// <summary>
        /// The product's position in a list or collection.
        /// <remarks>Optional</remarks>
        /// <example>2</example>
        /// </summary>
        int? Position { get; set; }

        /// <summary>
        /// Add a custom dimension value with check if this dimension value already set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        void AddCustomDimension(int id, string value);

        /// <summary>
        /// Get all custom dimensions of product
        /// </summary>
        /// <returns></returns>
        List<ICustomDimension> GetCustomDimensions();
    }
}
