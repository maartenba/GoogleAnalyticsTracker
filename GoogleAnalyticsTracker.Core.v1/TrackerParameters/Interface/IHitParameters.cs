namespace GoogleAnalyticsTracker.Core.v1.TrackerParameters.Interface
{
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
        GoogleBoolean? NonInteractionHit { get; set; }
    }
}