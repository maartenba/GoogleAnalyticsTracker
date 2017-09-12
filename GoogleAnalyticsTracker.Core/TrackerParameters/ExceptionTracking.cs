using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class ExceptionTracking : GeneralParameters, IExceptionParameters
    {
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType
        {
            get { return HitType.Exception; }
        }

        #endregion

        #region Implementation of IExceptionParameters

        /// <summary>
        /// Specifies the description of an exception.
        /// <remarks>Optional</remarks>
        /// <example>DatabaseError</example>
        /// </summary>   
        [Beacon("exd")]
        public string Description { get; set; }

        /// <summary>
        /// Specifies whether the exception was fatal.
        /// <remarks>Optional, null value means the exception is fatal.</remarks>
        /// <example>false</example>
        /// </summary> 
        [Beacon("exf")] 
        public bool? IsFatal { get; set; }

        #endregion
    }
}