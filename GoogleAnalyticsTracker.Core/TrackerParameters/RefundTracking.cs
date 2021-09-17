using System.Collections.Generic;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    public class RefundTracking : EventTracking, IRefundTrackingParameters
    {
        public RefundTracking(string transactionId) 
        {
            TransactionId = transactionId;
            Category = "Ecommerce";
            Action = "Refund";
            Products = new List<IEnhancedECommerceProduct>();
            ProductAction = ProductAction.Refund;
        }
        
        #region Overrides of GeneralParameters
        
        public override HitType HitType => HitType.Event;

        #endregion

        #region Implementation of IRefundTrackingParameters
        
        [Beacon("ti", true)]
        public string TransactionId { get; set; }

        [Beacon("pa", true)]
        public ProductAction ProductAction { get; set; }

        public List<IEnhancedECommerceProduct> Products { get; set; }

        #endregion
    }
}