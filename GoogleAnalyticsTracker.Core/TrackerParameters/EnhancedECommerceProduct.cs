using System.Collections.Generic;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class EnhancedECommerceProduct : IEnhancedECommerceProduct
    {
        public EnhancedECommerceProduct()
        {
            CustomDimensions = new List<ICustomDimension>();
        }

        [Beacon("id")]
        public string Sku { get; set; }

        [Beacon("nm")]
        public string Name { get; set; }

        [Beacon("br")]
        public string Brand { get; set; }

        [Beacon("ca")]
        public string Category { get; set; }

        [Beacon("va")]
        public string Variant { get; set; }

        [Beacon("pr")]
        public decimal? Price { get; set; }

        [Beacon("qt")]
        public int? Quantity { get; set; }

        [Beacon("cc")]
        public string ProductCouponCode { get; set; }

        [Beacon("ps")]
        public int? Position { get; set; }

        public List<ICustomDimension> CustomDimensions { get; set; }
    }
}
