using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class ContentExperiments : GeneralParameters, IContentExperimentsParameters
    {
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType
        {
            get { return HitType.Pageview; }
        }

        #endregion

        #region Implementation of IContentExperimentsParameters

        /// <summary>
        /// Specifies the experiment id.
        /// <remarks>Required for experiment tracking.</remarks>
        /// <example>K7Q-9lpLSd21prp9vIhdoA</example>
        /// </summary>     
        [Beacon("xid")]
        public string ExperimentId { get; set; }

        /// <summary>
        /// Specifies the experiment variant id.
        /// <remarks>Required for content experiment tracking.</remarks>
        /// <example>1</example>
        /// </summary>    
        [Beacon("xvar")]
        public string ExperimentVariant { get; set; }

        #endregion
    }
}
