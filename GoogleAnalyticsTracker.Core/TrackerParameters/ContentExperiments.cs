using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    public class ContentExperiments : GeneralParameters, IContentExperimentsParameters
    {
        /// <summary>
        /// Creates a new <see cref="ContentExperiments"/>.
        /// </summary>
        /// <param name="hitType">The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.</param>
        /// <param name="experimentId">Specifies the experiment id.</param>
        /// <param name="experimentVariant">Specifies the experiment variant id.</param>
        public ContentExperiments(
            HitType hitType,
            string experimentId, 
            string experimentVariant)
        {
            HitType = hitType;
            ExperimentId = experimentId;
            ExperimentVariant = experimentVariant;
        }

        #region Overrides of GeneralParameters

        public override HitType HitType { get; }

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
