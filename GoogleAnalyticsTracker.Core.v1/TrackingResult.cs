using System;
using System.Collections.Generic;

namespace GoogleAnalyticsTracker.Core.v1
{
    public class TrackingResult
    {
        public string Url { get; set; }
        public IDictionary<string, string> Parameters { get; set; }
        public bool Success { get; set; }
        public Exception Exception { get; set; }
    }
}