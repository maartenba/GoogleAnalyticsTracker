namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    public interface IGeneralParameters : IHitParameters, IUserParameters, ISystemInfoParameters,
                                          IContentInformationParameters,
                                          ISessionParameters, ITrafficSourcesParameters,
                                          IAppTrackingParameters, ICustomDimensionParameters
    {
        /// <summary>
        /// The Protocol version. The current value is '1'. This will only change when there are changes made that are not backwards compatible.
        /// <remarks>Required for all hit types</remarks>
        /// </summary>        
        string ProtocolVersion { get; }

        /// <summary>
        /// The tracking ID / web property ID. The format is UA-XXXX-Y. All collected data is associated by this ID.
        /// <remarks>Required for all hit types</remarks>
        /// <example>UA-XXXX-Y</example>
        /// </summary>
        string TrackingId { get; set; }

        /// <summary>
        /// When present, the IP address of the sender will be anonymized. 
        /// For example, the IP will be anonymized if any of the following parameters are present in the payload: &amp;aip=, &amp;aip=0, or &amp;aip=1
        /// <remarks>Optional</remarks>
        /// <example>GoogleBoolean.True</example>
        /// </summary>        
        GoogleBoolean? AnonymizeIp { get; set; }

        /// <summary>
        /// Used to collect offline / latent hits. 
        /// The value represents the time delta (in milliseconds) between when the hit being reported occurred and the time the hit was sent. 
        /// The value must be greater than or equal to 0. Values greater than four hours may lead to hits not being processed.
        /// <remarks>Optional</remarks>
        /// <example>560</example>
        /// </summary>
        long? QueueTime { get; set; }

        /// <summary>
        /// Used to send a random number in GET requests to ensure browsers and proxies don't cache hits. 
        /// It should be sent as the final parameter of the request since we've seen some 3rd party internet filtering software add additional parameters to HTTP requests incorrectly. This value is not used in reporting.
        /// <remarks>Optional</remarks>
        /// <example>289372387623</example>
        /// </summary>        
        string CacheBuster { get; set; }

        /// <summary>
        /// Is a formatted user agent string that is used to compute the following dimensions: browser, platform, and mobile capabilities.
        /// <remarks>Required</remarks>
        /// <example>Mozilla/5.0 (iPad; U; CPU OS 3_2_1 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Mobile/7B405</example>
        /// </summary>
        string UserAgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string ReferralUrl { get; set; }
    }
}