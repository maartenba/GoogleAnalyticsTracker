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
        /// <remarks>Optional</remarks>
        /// <example>0</example>
        /// </summary>  
        GoogleBoolean IsFatal { get; set; }
    }
}