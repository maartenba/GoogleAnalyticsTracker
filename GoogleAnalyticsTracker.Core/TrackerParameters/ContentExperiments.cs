using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class ContentExperiments : GeneralParameters, IContentExperimentsParameters
    {
        private readonly HitType _hitType;

        public ContentExperiments(HitType hitType)
        {
            _hitType = hitType;
        }

        #region Overrides of GeneralParameters

        public override HitType HitType
        {
            get { return _hitType; }
        }

        #endregion

        #region Implementation of IContentExperimentsParameters

        /// <summary>
        /// Specifies the experiment id.
        /// <remarks>Required for experiment tracking</remarks>
        /// <example>K7Q-9lpLSd21prp9vIhdoA</example>
        /// </summary>     
        [Beacon("xid", true)]
        public string ExperimentId { get; set; }

        /// <summary>
        /// Specifies the experiment variant id.
        /// <remarks>Required for content experiment tracking</remarks>
        /// <example>1</example>
        /// </summary>    
        [Beacon("xvar", true)]
        public string ExperimentVariant { get; set; }

        #endregion
    }
}
