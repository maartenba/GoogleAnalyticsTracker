using System.Collections.Generic;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    public class EnhancedECommerceTransaction : ECommerceTransaction, IEnhancedECommerceTransactionParameters
    {
        public EnhancedECommerceTransaction()
        {
            Products = new List<IEnhancedECommerceProduct>();
        }

        [Beacon("pa")]
        public ProductAction ProductAction { get; set; }

        [Beacon("tcc")]
        public string CouponCode { get; set; }

        [Beacon("pal")]
        public string ProductActionList { get; set; }

        [Beacon("cos")]
        public int? CheckoutStep { get; set; }

        [Beacon("col")]
        public string CheckoutStepOption { get; set; }

        public List<IEnhancedECommerceProduct> Products { get; set; }
    }
}
