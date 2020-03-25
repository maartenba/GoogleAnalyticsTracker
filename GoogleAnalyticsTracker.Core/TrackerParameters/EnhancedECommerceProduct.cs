using System;
using System.Collections.Generic;
using System.Linq;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class EnhancedECommerceProduct : IEnhancedECommerceProduct
    {
        public EnhancedECommerceProduct()
        {
            _customDimensions = new List<ICustomDimension>();
        }

        public void AddCustomDimension(int id, string value)
        {
            if (_customDimensions.Any(cd => cd.Id == id))
            {
                throw new ArgumentException($"Product already have custom dimension with Id = {id}");
            }

            _customDimensions.Add(new CustomDimension(id, value));
        }

        public List<ICustomDimension> GetCustomDimensions()
        {
            return _customDimensions;
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

        private readonly List<ICustomDimension> _customDimensions;
    }
}
