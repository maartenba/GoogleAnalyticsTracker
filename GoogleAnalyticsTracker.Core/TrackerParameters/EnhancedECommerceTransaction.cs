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

        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType => HitType.Event;

        #endregion

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
