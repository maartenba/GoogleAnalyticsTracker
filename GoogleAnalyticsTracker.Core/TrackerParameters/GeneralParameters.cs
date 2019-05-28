using System.Collections.Generic;

using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public abstract class GeneralParameters : IGeneralParameters
    {        
        public string UserAgent { get; set; }

        #region Implementation of IGeneralParameters

        /// <summary>
        /// The Protocol version. The current value is '1'. This will only change when there are changes made that are not backwards compatible.
        /// <remarks>Required for all hit types</remarks>
        /// </summary>        
        [Beacon("v", true)]
        public string ProtocolVersion
        {
            get { return "1"; }
        }

        /// <summary>
        /// The tracking ID / web property ID. The format is UA-XXXX-Y. All collected data is associated by this ID.
        /// <remarks>Required for all hit types</remarks>
        /// <example>UA-XXXX-Y</example>
        /// </summary>
        [Beacon("tid", true)]
        public string TrackingId { get; set; }

        /// <summary>
        /// When present, the IP address of the sender will be anonymized. 
        /// For example, the IP will be anonymized if any of the following parameters are present in the payload: &amp;aip=, &amp;aip=0, or &amp;aip=1
        /// <remarks>Optional</remarks>
        /// <example>GoogleBoolean.True</example>
        /// </summary>         
        [Beacon("aip")]
        public bool? AnonymizeIp { get; set; }

        /// <summary>
        /// Used to collect offline / latent hits. 
        /// The value represents the time delta (in milliseconds) between when the hit being reported occurred and the time the hit was sent. 
        /// The value must be greater than or equal to 0. Values greater than four hours may lead to hits not being processed.
        /// <remarks>Optional</remarks>
        /// <example>560</example>
        /// </summary>
        [Beacon("qt")]
        public long? QueueTime { get; set; }

        /// <summary>
        /// Used to send a random number in GET requests to ensure browsers and proxies don't cache hits. 
        /// It should be sent as the final parameter of the request since we've seen some 3rd party internet filtering software add additional parameters to HTTP requests incorrectly. This value is not used in reporting.
        /// <remarks>Optional</remarks>
        /// <example>289372387623</example>
        /// </summary>   
        [Beacon("z")]
        public string CacheBuster { get; set; }

        #endregion

        #region Implementation of IHitParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        [Beacon("t", true)]
        public abstract HitType HitType { get; }

        /// <summary>
        /// Specifies that a hit be considered non-interactive.
        /// <remarks>Optional</remarks>
        /// <example>GoogleBoolean.True</example>
        /// </summary>                
        [Beacon("ni")]
        public bool? NonInteractionHit { get; set; }

        #endregion

        #region Implementation of IUserParameters

        /// <summary>
        /// This anonymously identifies a particular user, device, or browser instance. 
        /// For the web, this is generally stored as a first-party cookie with a two-year expiration. For mobile apps, this is randomly generated for each particular instance of an application install. 
        /// The value of this field should be a random UUID (version 4) as described in http://www.ietf.org/rfc/rfc4122.txt
        /// <remarks>Required for all hit types</remarks>
        /// <example>35009a79-1a05-49d7-b876-2b884d0f825b</example>
        /// </summary>
        [Beacon("cid", true)]
        public string ClientId { get; set; }

        /// <summary>
        /// This is intended to be a known identifier for a user provided by the site owner/tracking library user. 
        /// It may not itself be PII (personally identifiable information). 
        /// The value should never be persisted in GA cookies or other Analytics provided storage.
        /// <remarks>Optional</remarks>
        /// <example>as8eknlll</example>
        /// </summary>   
        [Beacon("uid")]
        public string UserId { get; set; }

        #endregion

        #region Implementation of ISystemInfoParameters

        /// <summary>
        /// Specifies the screen resolution
        /// <remarks>Optional</remarks>
        /// <example>800x600</example>
        /// </summary>        
        [Beacon("sr")]
        public string ScreenResolution { get; set; }

        /// <summary>
        /// Specifies the viewable area of the browser / device.
        /// <remarks>Optional</remarks>
        /// <example>123x456</example>
        /// </summary>        
        [Beacon("vp")]
        public string ViewportSize { get; set; }

        /// <summary>
        /// Specifies the character set used to encode the page / document.
        /// <remarks>Optional</remarks>
        /// <example>UTF-8</example>
        /// </summary>        
        [Beacon("de")]
        public string DocumentEncoding { get; set; }

        /// <summary>
        /// Specifies the screen color depth.
        /// <remarks>Optional</remarks>
        /// <example>24-bits</example>
        /// </summary>        
        [Beacon("sd")]
        public string ScreenColors { get; set; }

        /// <summary>
        /// Specifies the language.
        /// <remarks>Optional</remarks>
        /// <example>en-us</example>        
        /// </summary>      
        [Beacon("ul")]
        public string UserLanguage { get; set; }

        /// <summary>
        /// Specifies whether Java was enabled.
        /// <remarks>Optional</remarks>
        /// <example>GoogleBoolean.True</example>
        /// </summary>                
        [Beacon("je")]
        public bool? JavaEnabled { get; set; }

        /// <summary>
        /// Specifies the flash version.
        /// <remarks>Optional</remarks>
        /// <example>10 1 r103</example>
        /// </summary>       
        [Beacon("fl")]
        public string FlashVersion { get; set; }

        #endregion

        #region Implementation of IContentInformationParameters

        /// <summary>
        /// Use this parameter to send the full URL (document location) of the page on which content resides. 
        /// You can use the &amp;dh and &amp;dp parameters to override the hostname and path + query portions of the document location, accordingly. 
        /// The JavaScript clients determine this parameter using the concatenation of the document.location.origin + document.location.pathname + document.location.search browser parameters. 
        /// Be sure to remove any user authentication or other private information from the URL if present.
        /// <remarks>Optional (For 'pageview' hits, either &amp;dl or both &amp;dh and &amp;dp have to be specified for the hit to be valid)</remarks>
        /// <example>http://foo.com/home?a=b</example>
        /// </summary>
        [Beacon("dl", true)]
        public string DocumentLocationUrl { get; set; }

        /// <summary>
        /// Specifies the hostname from which content was hosted.
        /// <remarks>Optional</remarks>
        /// <example>foo.com</example>
        /// </summary>
        [Beacon("dh")]
        public string DocumentHostName { get; set; }

        /// <summary>
        /// he path portion of the page URL.
        /// <remarks>Optional (Should begin with '/'. For 'pageview' hits, either &amp;dl or both &amp;dh and &amp;dp have to be specified for the hit to be valid.)</remarks>
        /// <example>/foo</example>
        /// </summary>
        [Beacon("dp", true)]
        public string DocumentPath { get; set; }

        /// <summary>
        /// The title of the page / document.
        /// <remarks>Optional</remarks>
        /// <example>Settings</example>
        /// </summary>
        [Beacon("dt")]
        public string DocumentTitle { get; set; }

        /// <summary>
        /// If not specified, this will default to the unique URL of the page by either using the &amp;dl parameter as-is or assembling it from &amp;dh and &amp;dp. 
        /// App tracking makes use of this for the 'Screen Name' of the screenview hit.
        /// <remarks>Optional</remarks>
        /// <example>High Scores</example>
        /// </summary>
        [Beacon("cd")]
        public string ScreenName { get; set; }

        /// <summary>
        /// The ID of a clicked DOM element, used to disambiguate multiple links to the same URL in In-Page Analytics reports when Enhanced Link Attribution is enabled for the property.
        /// <remarks>Optional</remarks>
        /// <example>nav_bar</example>
        /// </summary>
        [Beacon("linkid")]
        public string LinkId { get; set; }

        #endregion

        #region Implementation of ISessionParameters

        /// <summary>
        /// Used to control the session duration. 
        /// A value of 'start' forces a new session to start with this hit and 'end' forces the current session to end with this hit. 
        /// All other values are ignored.
        /// <remarks>Optional</remarks>
        /// <example>SessionControl.Start</example>
        /// </summary>
        [Beacon("sc")]
        public SessionControl? SessionControl { get; set; }

        /// <summary>
        /// The IP address of the user. This should be a valid IP address. It will always be anonymized just as though &amp;aip (anonymize IP) had been used.
        /// <remarks>Optional</remarks>
        /// <example>1.2.3.4</example>
        /// </summary>
        [Beacon("uip")]
        public string IpOverride { get; set; }

        /// <summary>
        /// The User Agent of the browser. Note that Google has libraries to identify real user agents. 
        /// Hand crafting your own agent could break at any time.
        /// <remarks>Optional</remarks>
        /// <example>Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14</example>
        /// </summary>
        [Beacon("ua")]
        public string UserAgentOverride { get; set; }

        #endregion

        #region Implementation of ITrafficSourceParameters

        /// <summary>
        /// Specifies which referral source brought traffic to a website. 
        /// This value is also used to compute the traffic source. The format of this value is a URL.
        /// <remarks>Optional</remarks>
        /// <example>http://example.com</example>
        /// </summary>
        [Beacon("dr")]
        public string DocumentReferrer { get; set; }

        /// <summary>
        /// Specifies the campaign name.
        /// <remarks>Optional</remarks>
        /// <example>(direct)</example>
        /// </summary>
        [Beacon("cn")]
        public string CampaignName { get; set; }

        /// <summary>
        /// Specifies the campaign source.
        /// <remarks>Optional</remarks>
        /// <example>(direct)</example>
        /// </summary>
        [Beacon("cs")]
        public string CampaignSource { get; set; }

        /// <summary>
        /// Specifies the campaign medium.
        /// <remarks>Optional</remarks>
        /// <example>organic</example>
        /// </summary>
        [Beacon("cm")]
        public string CampaignMedium { get; set; }

        /// <summary>
        /// Specifies the campaign keyword.
        /// <remarks>Optional</remarks>
        /// <example>Blue Shoes</example>
        /// </summary>
        [Beacon("ck")]
        public string CampaignKeyword { get; set; }

        /// <summary>
        /// Specifies the campaign content.
        /// <remarks>Optional</remarks>
        /// <example>content</example>
        /// </summary>
        [Beacon("cc")]
        public string CampaignContent { get; set; }

        /// <summary>
        /// Specifies the campaign ID.
        /// <remarks>Optional</remarks>
        /// <example>ID</example>
        /// </summary>
        [Beacon("ci")]
        public string CampaignId { get; set; }

        /// <summary>
        /// Specifies the Google AdWords Id.
        /// <remarks>Optional</remarks>
        /// <example>CL6Q-OXyqKUCFcgK2goddQuoHg</example>
        /// </summary>
        [Beacon("gclid")]
        public string GoogleAdWordsId { get; set; }

        /// <summary>
        /// Specifies the Google Display Ads Id.
        /// <remarks>Optional</remarks>
        /// <example>d_click_id</example>
        /// </summary>
        [Beacon("dclid")]
        public string GoogleDisplayAdsId { get; set; }

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

        #region Implementation of ICustomDimensionParameters
        /// <summary>
        /// Any custom dimensions are set here.
        /// </summary>
        public void SetCustomDimensions(IDictionary<int, string> customDimensions)
        {
            if (customDimensions == null || customDimensions.Count <= 0) return;
            foreach (var dimension in customDimensions)
            {
                if (dimension.Value == null) continue;
                var value = dimension.Value;

                //max length of 150 bytes for custom dimensions
                if (value.Length > 149) value = value.Substring(0, 149);

                if (dimension.Key == 1) CustomDimension1 = value;
                else if (dimension.Key == 2) CustomDimension2 = value;
                else if (dimension.Key == 3) CustomDimension3 = value;
                else if (dimension.Key == 4) CustomDimension4 = value;
                else if (dimension.Key == 5) CustomDimension5 = value;
                else if (dimension.Key == 6) CustomDimension6 = value;
                else if (dimension.Key == 7) CustomDimension7 = value;
                else if (dimension.Key == 8) CustomDimension8 = value;
                else if (dimension.Key == 9) CustomDimension9 = value;
                else if (dimension.Key == 10) CustomDimension10 = value;
                else if (dimension.Key == 11) CustomDimension11 = value;
                else if (dimension.Key == 12) CustomDimension12 = value;
                else if (dimension.Key == 13) CustomDimension13 = value;
                else if (dimension.Key == 14) CustomDimension14 = value;
                else if (dimension.Key == 15) CustomDimension15 = value;
                else if (dimension.Key == 16) CustomDimension16 = value;
                else if (dimension.Key == 17) CustomDimension17 = value;
                else if (dimension.Key == 18) CustomDimension18 = value;
                else if (dimension.Key == 19) CustomDimension19 = value;
                else if (dimension.Key == 20) CustomDimension20 = value;
            }
        }

        [Beacon("cd1")]
        public string CustomDimension1 { get; set; }
        [Beacon("cd2")]
        public string CustomDimension2 { get; set; }
        [Beacon("cd3")]
        public string CustomDimension3 { get; set; }
        [Beacon("cd4")]
        public string CustomDimension4 { get; set; }
        [Beacon("cd5")]
        public string CustomDimension5 { get; set; }
        [Beacon("cd6")]
        public string CustomDimension6 { get; set; }
        [Beacon("cd7")]
        public string CustomDimension7 { get; set; }
        [Beacon("cd8")]
        public string CustomDimension8 { get; set; }
        [Beacon("cd9")]
        public string CustomDimension9 { get; set; }
        [Beacon("cd10")]
        public string CustomDimension10 { get; set; }
        [Beacon("cd11")]
        public string CustomDimension11 { get; set; }
        [Beacon("cd12")]
        public string CustomDimension12 { get; set; }
        [Beacon("cd13")]
        public string CustomDimension13 { get; set; }
        [Beacon("cd14")]
        public string CustomDimension14 { get; set; }
        [Beacon("cd15")]
        public string CustomDimension15 { get; set; }
        [Beacon("cd16")]
        public string CustomDimension16 { get; set; }
        [Beacon("cd17")]
        public string CustomDimension17 { get; set; }
        [Beacon("cd18")]
        public string CustomDimension18 { get; set; }
        [Beacon("cd19")]
        public string CustomDimension19 { get; set; }
        [Beacon("cd20")]
        public string CustomDimension20 { get; set; }
        #endregion

        #region Implementation of ICustomMetricParameters
        /// <summary>
        /// Any custom dimensions are set here.
        /// </summary>
        public void SetCustomMetrics(IDictionary<int, long?> customMetrics)
        {
            if (customMetrics == null || customMetrics.Count <= 0) return;
            foreach (var metric in customMetrics)
            {
                if (metric.Value == null) continue;
                var value = metric.Value.Value;

                if (metric.Key == 1) CustomMetric1 = value;
                else if (metric.Key == 2) CustomMetric2 = value;
                else if (metric.Key == 3) CustomMetric3 = value;
                else if (metric.Key == 4) CustomMetric4 = value;
                else if (metric.Key == 5) CustomMetric5 = value;
                else if (metric.Key == 6) CustomMetric6 = value;
                else if (metric.Key == 7) CustomMetric7 = value;
                else if (metric.Key == 8) CustomMetric8 = value;
                else if (metric.Key == 9) CustomMetric9 = value;
                else if (metric.Key == 10) CustomMetric10 = value;
                else if (metric.Key == 11) CustomMetric11 = value;
                else if (metric.Key == 12) CustomMetric12 = value;
                else if (metric.Key == 13) CustomMetric13 = value;
                else if (metric.Key == 14) CustomMetric14 = value;
                else if (metric.Key == 15) CustomMetric15 = value;
                else if (metric.Key == 16) CustomMetric16 = value;
                else if (metric.Key == 17) CustomMetric17 = value;
                else if (metric.Key == 18) CustomMetric18 = value;
                else if (metric.Key == 19) CustomMetric19 = value;
                else if (metric.Key == 20) CustomMetric20 = value;
            }
        }
        
        [Beacon("cm1")]
        public long? CustomMetric1 { get; set; }
        [Beacon("cm2")]
        public long? CustomMetric2 { get; set; }
        [Beacon("cm3")]
        public long? CustomMetric3 { get; set; }
        [Beacon("cm4")]
        public long? CustomMetric4 { get; set; }
        [Beacon("cm5")]
        public long? CustomMetric5 { get; set; }
        [Beacon("cm6")]
        public long? CustomMetric6 { get; set; }
        [Beacon("cm7")]
        public long? CustomMetric7 { get; set; }
        [Beacon("cm8")]
        public long? CustomMetric8 { get; set; }
        [Beacon("cm9")]
        public long? CustomMetric9 { get; set; }
        [Beacon("cm10")]
        public long? CustomMetric10 { get; set; }
        [Beacon("cm11")]
        public long? CustomMetric11 { get; set; }
        [Beacon("cm12")]
        public long? CustomMetric12 { get; set; }
        [Beacon("cm13")]
        public long? CustomMetric13 { get; set; }
        [Beacon("cm14")]
        public long? CustomMetric14 { get; set; }
        [Beacon("cm15")]
        public long? CustomMetric15 { get; set; }
        [Beacon("cm16")]
        public long? CustomMetric16 { get; set; }
        [Beacon("cm17")]
        public long? CustomMetric17 { get; set; }
        [Beacon("cm18")]
        public long? CustomMetric18 { get; set; }
        [Beacon("cm19")]
        public long? CustomMetric19 { get; set; }
        [Beacon("cm20")]
        public long? CustomMetric20 { get; set; }
        #endregion
    }
}