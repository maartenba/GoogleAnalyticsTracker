using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface ITrafficSourcesParameters
    {
        /// <summary>
        /// Specifies which referral source brought traffic to a website. 
        /// This value is also used to compute the traffic source. The format of this value is a URL.
        /// <remarks>Optional</remarks>
        /// <example>http://example.com</example>
        /// </summary>       
        string DocumentReferrer { get; set; }

        /// <summary>
        /// Specifies the campaign name.
        /// <remarks>Optional</remarks>
        /// <example>(direct)</example>
        /// </summary>        
        string CampaignName { get; set; }

        /// <summary>
        /// Specifies the campaign source.
        /// <remarks>Optional</remarks>
        /// <example>(direct)</example>
        /// </summary>        
        string CampaignSource { get; set; }

        /// <summary>
        /// Specifies the campaign medium.
        /// <remarks>Optional</remarks>
        /// <example>organic</example>
        /// </summary>        
        string CampaignMedium { get; set; }

        /// <summary>
        /// Specifies the campaign keyword.
        /// <remarks>Optional</remarks>
        /// <example>Blue Shoes</example>
        /// </summary>        
        string CampaignKeyword { get; set; }

        /// <summary>
        /// Specifies the campaign content.
        /// <remarks>Optional</remarks>
        /// <example>content</example>
        /// </summary>       
        string CampaignContent { get; set; }

        /// <summary>
        /// Specifies the campaign ID.
        /// <remarks>Optional</remarks>
        /// <example>ID</example>
        /// </summary>        
        string CampaignId { get; set; }

        /// <summary>
        /// Specifies the Google AdWords Id.
        /// <remarks>Optional</remarks>
        /// <example>CL6Q-OXyqKUCFcgK2goddQuoHg</example>
        /// </summary>        
        string GoogleAdWordsId { get; set; }

        /// <summary>
        /// Specifies the Google Display Ads Id.
        /// <remarks>Optional</remarks>
        /// <example>d_click_id</example>
        /// </summary>        
        string GoogleDisplayAdsId { get; set; }
    }
}