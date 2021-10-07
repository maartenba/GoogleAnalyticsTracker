using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters;

[PublicAPI]
public enum ProductAction
{
    Detail,
    Click,
    Add,
    Remove,
    Checkout,
    CheckoutOption,
    Purchase,
    Refund
}