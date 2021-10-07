using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters;

[PublicAPI]
public enum HitType
{                
    Pageview,
    Event,
    Social,
    Timing,
    Screenview,
    Transaction,
    Item,
    Exception,
    NonImplemented
}