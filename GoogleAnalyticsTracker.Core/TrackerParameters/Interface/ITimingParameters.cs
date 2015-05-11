namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    public interface ITimingParameters
    {
        /// <summary>
        /// Specifies the user timing category.
        /// <remarks>Optional</remarks>
        /// <example>category</example>
        /// </summary>       
        string UserTimingCategory { get; set; }

        /// <summary>
        /// Specifies the user timing variable.
        /// <remarks>Optional</remarks>
        /// <example>lookup</example>
        /// </summary>        
        string UserTimingVariable { get; set; }

        /// <summary>
        /// Specifies the user timing value. The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>123</example>
        /// </summary>        
        long UserTimingTime { get; set; }

        /// <summary>
        /// Specifies the user timing label.
        /// <remarks>Optional</remarks>
        /// <example>label</example>
        /// </summary>        
        string UserTimingLabel { get; set; }

        /// <summary>
        /// Specifies the time it took for a page to load. The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>3554</example>
        /// </summary>        
        long? PageLoadTime { get; set; }

        /// <summary>
        /// Specifies the time it took to do a DNS lookup.The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>43</example>
        /// </summary>        
        long? DnsTime { get; set; }

        /// <summary>
        /// Specifies the time it took for the page to be downloaded. The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>500</example>
        /// </summary>        
        long? PageDownloadTime { get; set; }

        /// <summary>
        /// Specifies the time it took for any redirects to happen. The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>500</example>
        /// </summary>        
        long? RedirectResponseTime { get; set; }

        /// <summary>
        /// Specifies the time it took for a TCP connection to be made. The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>500</example>
        /// </summary>        
        long? TcpConnectTime { get; set; }

        /// <summary>
        /// Specifies the time it took for the server to respond after the connect time. The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>500</example>
        /// </summary>        
        long? ServerResponseTime { get; set; }

        /// <summary>
        /// Specifies the time it took for Document.readyState to be 'interactive'. The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>500</example>
        /// </summary>        
        long? DomInteractiveTime { get; set; }

        /// <summary>
        /// Specifies the time it took for the DOMContentLoaded Event to fire. The value is in milliseconds.
        /// <remarks>Optional</remarks>
        /// <example>500</example>
        /// </summary>        
        long? ContentLoadTime { get; set; }
    }
}