using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface IUserParameters
    {
        /// <summary>
        /// This anonymously identifies a particular user, device, or browser instance. 
        /// For the web, this is generally stored as a first-party cookie with a two-year expiration. For mobile apps, this is randomly generated for each particular instance of an application install. 
        /// The value of this field should be a random UUID (version 4) as described in http://www.ietf.org/rfc/rfc4122.txt
        /// <remarks>Required for all hit types</remarks>
        /// <example>35009a79-1a05-49d7-b876-2b884d0f825b</example>
        /// </summary>        
        string? ClientId { get; set; }

        /// <summary>
        /// This is intended to be a known identifier for a user provided by the site owner/tracking library user. 
        /// It may not itself be PII (personally identifiable information). 
        /// The value should never be persisted in GA cookies or other Analytics provided storage.
        /// <remarks>Optional</remarks>
        /// <example>as8eknlll</example>
        /// </summary>        
        string? UserId { get; set; }
    }
}