using GoogleAnalyticsTracker.Core.v1.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.v1.TrackerParameters
{
    public class SocialInterationsParameters : GeneralParameters, ISocialInterationsParameters
    {
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType
        {
            get { return HitType.Social; }
        }

        #endregion

        #region Implementation of ISocialInterationsParameters

        /// <summary>
        /// Specifies the social network, for example Facebook or Google Plus.
        /// <remarks>Required for social hit type.</remarks>
        /// <example>facebook</example>
        /// </summary>
        [Beacon("sn", true)]
        public string SocialNetwork { get; set; }

        /// <summary>
        /// Specifies the social interaction action. For example on Google Plus when a user clicks the +1 button, the social action is 'plus'.
        /// <remarks>Required for social hit type.</remarks>
        /// <example>like</example>
        /// </summary>
        [Beacon("sa", true)]
        public string SocialAction { get; set; }

        /// <summary>
        /// Specifies the target of a social interaction. This value is typically a URL but can be any text.
        /// <remarks>Required for social hit type.</remarks>
        /// <example>http://foo.com</example>
        /// </summary>
        [Beacon("st", true)]
        public string SocialActionTarget { get; set; }

        #endregion
    }
}