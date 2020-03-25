using System;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core
{
    [PublicAPI]
    public static class GoogleAnalyticsEndpoints
    {
        /// <summary>Default Google Endpoint URL.</summary>
        public const string Default = "https://www.google-analytics.com/collect";

        /// <summary>Non-secure Google Endpoint URL, not recommended.</summary>
        /// <remarks>It is not recommended to use this endpoint, as it uses no secure connection.</remarks>
        [Obsolete("It is not recommended to use this endpoint, as it uses no secure connection.")]
        public const string NonSecure = "http://www.google-analytics.com/collect";

        /// <summary>Debug, validating Google Endpoint.</summary>
        public const string Debug = "https://www.google-analytics.com/debug/collect";
    }
}
