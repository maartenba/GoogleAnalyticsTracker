using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface IContentInformationParameters
    {
        /// <summary>
        /// Use this parameter to send the full URL (document location) of the page on which content resides. 
        /// You can use the &amp;dh and &amp;dp parameters to override the hostname and path + query portions of the document location, accordingly. 
        /// The JavaScript clients determine this parameter using the concatenation of the document.location.origin + document.location.pathname + document.location.search browser parameters. 
        /// Be sure to remove any user authentication or other private information from the URL if present.
        /// <remarks>Optional (For 'pageview' hits, either &amp;dl or both &amp;dh and &amp;dp have to be specified for the hit to be valid)</remarks>
        /// <example>http://foo.com/home?a=b</example>
        /// </summary>        
        string DocumentLocationUrl { get; set; }

        /// <summary>
        /// Specifies the hostname from which content was hosted.
        /// <remarks>Optional</remarks>
        /// <example>foo.com</example>
        /// </summary>        
        string DocumentHostName { get; set; }

        /// <summary>
        /// he path portion of the page URL.
        /// <remarks>Optional (Should begin with '/'. For 'pageview' hits, either &amp;dl or both &amp;dh and &amp;dp have to be specified for the hit to be valid.)</remarks>
        /// <example>/foo</example>
        /// </summary>        
        string DocumentPath { get; set; }

        /// <summary>
        /// The title of the page / document.
        /// <remarks>Optional</remarks>
        /// <example>Settings</example>
        /// </summary>        
        string DocumentTitle { get; set; }

        /// <summary>
        /// If not specified, this will default to the unique URL of the page by either using the &amp;dl parameter as-is or assembling it from &amp;dh and &amp;dp. 
        /// App tracking makes use of this for the 'Screen Name' of the screenview hit.
        /// <remarks>Optional</remarks>
        /// <example>High Scores</example>
        /// </summary>        
        string ScreenName { get; set; }

        /// <summary>
        /// The ID of a clicked DOM element, used to disambiguate multiple links to the same URL in In-Page Analytics reports when Enhanced Link Attribution is enabled for the property.
        /// <remarks>Optional</remarks>
        /// <example>nav_bar</example>
        /// </summary>        
        string LinkId { get; set; }
    }
}