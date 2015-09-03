using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class ScreenviewTracking : GeneralParameters, IAppTrackingParameters
    {
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType
        {
            get { return HitType.Screenview; }
        }

        #endregion

        #region Implementation of IAppTrackingParameters

        /// <summary>
        /// Specifies the application name.
        /// <remarks>Optional</remarks>
        /// <example>My App</example>
        /// </summary>
        [Beacon("an")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Application identifier.
        /// <remarks>Optional</remarks>
        /// <example>com.company.app</example>
        /// </summary>
        [Beacon("aid")]
        public string ApplicationId { get; set; }

        /// <summary>
        /// Specifies the application version.
        /// <remarks>Optional</remarks>
        /// <example>1.2</example>
        /// </summary>
        [Beacon("av")]
        public string ApplicationVersion { get; set; }

        /// <summary>
        /// Application installer identifier.
        /// <remarks>Optional</remarks>
        /// <example>com.platform.vending</example>
        /// </summary>
        [Beacon("aiid")]
        public string ApplicationInstallerId { get; set; }

        #endregion
    }
}