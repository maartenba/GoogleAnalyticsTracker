namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    public interface IExceptionParameters
    {
        /// <summary>
        /// Specifies the description of an exception.
        /// <remarks>Optional</remarks>
        /// <example>DatabaseError</example>
        /// </summary>   
        string Description { get; set; }

        /// <summary>
        /// Specifies whether the exception was fatal.
        /// <remarks>Optional, null value means the exception is fatal.</remarks>
        /// <example>false</example>
        /// </summary>  
        bool? IsFatal { get; set; }
    }
}