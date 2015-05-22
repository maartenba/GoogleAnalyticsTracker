namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    public interface ISessionParameters
    {
        /// <summary>
        /// Used to control the session duration. 
        /// A value of 'start' forces a new session to start with this hit and 'end' forces the current session to end with this hit. 
        /// All other values are ignored.
        /// <remarks>Optional</remarks>
        /// <example>SessionControl.Start</example>
        /// </summary>        
        SessionControl? SessionControl { get; set; }

        /// <summary>
        /// The IP address of the user. This should be a valid IP address. It will always be anonymized just as though &amp;aip (anonymize IP) had been used.
        /// <remarks>Optional</remarks>
        /// <example>1.2.3.4</example>
        /// </summary>        
        string IpOverride { get; set; }

        /// <summary>
        /// The User Agent of the browser. Note that Google has libraries to identify real user agents. 
        /// Hand crafting your own agent could break at any time.
        /// <remarks>Optional</remarks>
        /// <example>Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14</example>
        /// </summary>        
        string UserAgentOverride { get; set; }
    }
}