using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

[PublicAPI]
public interface IHitParameters
{
    /// <summary>
    /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
    /// <remarks>Required for all hit types</remarks>
    /// <example>HitType.Pageview</example>
    /// </summary>        
    HitType HitType { get; }

    /// <summary>
    /// Specifies that a hit be considered non-interactive.
    /// <remarks>Optional</remarks>
    /// <example>GoogleBoolean.True</example>
    /// </summary>        
    bool? NonInteractionHit { get; set; }
}