using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    public class EventTracking : GeneralParameters, IEventTrackingParameters
    {
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType => HitType.Event;

        #endregion

        #region Implementation of IEventTrackingParameters

        /// <summary>
        /// Specifies the event category. Must not be empty.
        /// <remarks>Required for event hit type</remarks>
        /// <example>Category</example>
        /// </summary>
        [Beacon("ec", true)]
        public string Category { get; set; }

        /// <summary>
        /// Specifies the event action. Must not be empty.
        /// <remarks>Required for event hit type</remarks>
        /// <example>Action</example>
        /// </summary>
        [Beacon("ea", true)]
        public string Action { get; set; }

        /// <summary>
        /// Specifies the event label.
        /// <remarks>Optional for event hit type</remarks>
        /// <example>Label</example>
        /// </summary>
        [Beacon("el")]
        public string Label { get; set; }

        /// <summary>
        /// Specifies the event value. Values must be non-negative.
        /// <example>55</example>
        /// </summary>
        [Beacon("ev")]
        public long? Value { get; set; }

        #endregion
    }
}