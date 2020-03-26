using System.Collections.Generic;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    /// <summary>
    /// Marker interface used by GoogleAnalyticsTracker to suggest
    /// beacon parameters can be extracted from a given object.
    /// </summary>
    [PublicAPI]
    public interface IProvideProductsParameters : IProvideBeaconParameters
    {
        /// <summary>
        /// List of products to send in scope of this transaction.
        /// <remarks>Optional</remarks>
        /// </summary>
        List<IEnhancedECommerceProduct> Products { get; set; }
    }
}