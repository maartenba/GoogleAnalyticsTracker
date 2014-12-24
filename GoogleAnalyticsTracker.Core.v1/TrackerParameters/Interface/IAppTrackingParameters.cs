namespace GoogleAnalyticsTracker.Core.v1.TrackerParameters.Interface
{
    public interface IAppTrackingParameters
    {
        /// <summary>
        /// Specifies the application name.
        /// <remarks>Optional</remarks>
        /// <example>My App</example>
        /// </summary>       
        string ApplicationName { get; set; }

        /// <summary>
        /// Application identifier.
        /// <remarks>Optional</remarks>
        /// <example>com.company.app</example>
        /// </summary>        
        string ApplicationId { get; set; }

        /// <summary>
        /// Specifies the application version.
        /// <remarks>Optional</remarks>
        /// <example>1.2</example>
        /// </summary>        
        string ApplicationVersion { get; set; }

        /// <summary>
        /// Application installer identifier.
        /// <remarks>Optional</remarks>
        /// <example>com.platform.vending</example>
        /// </summary>        
        string ApplicationInstallerId { get; set; }
    }
}