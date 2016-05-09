using System;
using System.Collections.Generic;

namespace GoogleAnalyticsTracker.Core
{
    public class TrackingResult
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string Url { get; set; }
        
        public IDictionary<string, string> Parameters { get; set; }
        public bool Success { get; set; }
        
        /// <summary>
        /// Gets or sets the exception object.
        /// </summary>
        public Exception Exception { get; set; }
    }
}
