using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface ISocialInteractionsParameters
    {
        /// <summary>
        /// Specifies the social network, for example Facebook or Google Plus.
        /// <remarks>Required for social hit type.</remarks>
        /// <example>facebook</example>
        /// </summary>        
        string SocialNetwork { get; set; }

        /// <summary>
        /// Specifies the social interaction action. For example on Google Plus when a user clicks the +1 button, the social action is 'plus'.
        /// <remarks>Required for social hit type.</remarks>
        /// <example>like</example>
        /// </summary>        
        string SocialAction { get; set; }

        /// <summary>
        /// Specifies the target of a social interaction. This value is typically a URL but can be any text.
        /// <remarks>Required for social hit type.</remarks>
        /// <example>http://foo.com</example>
        /// </summary>        
        string SocialActionTarget { get; set; }
    }
}