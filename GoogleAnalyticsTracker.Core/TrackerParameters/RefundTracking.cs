using System.Collections.Generic;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    public class RefundTracking : EventTracking, IRefundTrackingParameters
    {
        public RefundTracking()
        {
            NonInteractionHit = true;
            Category = "Ecommerce";
            Action = "Refund";
            Products = new List<IEnhancedECommerceProduct>();
        }
        
        #region Overrides of GeneralParameters
        
        public override HitType HitType => HitType.Event;

        #endregion

        #region Implementation of IRefundTrackingParameters
        
        [Beacon("ti", true)]
        public string TransactionId { get; set; }

        [Beacon("pa", true)]
        public ProductAction ProductAction => ProductAction.Refund;

        public List<IEnhancedECommerceProduct> Products { get; set; }

        #endregion
    }
}