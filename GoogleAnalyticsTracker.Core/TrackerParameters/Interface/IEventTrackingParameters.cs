namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    public interface IEventTrackingParameters
    {
        /// <summary>
        /// Specifies the event category. Must not be empty.
        /// <remarks>Optional</remarks>
        /// <example>Category</example>
        /// </summary>        
        string Category { get; set; }

        /// <summary>
        /// Specifies the event action. Must not be empty.
        /// <remarks>Optional</remarks>
        /// <example>Action</example>
        /// </summary>        
        string Action { get; set; }

        /// <summary>
        /// Specifies the event label.
        /// <remarks>Optional</remarks>
        /// <example>Label</example>
        /// </summary>        
        string Label { get; set; }

        /// <summary>
        /// Specifies the event value. Values must be non-negative.
        /// <remarks>Optional</remarks>
        /// <example>55</example>
        /// </summary>        
        long Value { get; set; }
    }
}