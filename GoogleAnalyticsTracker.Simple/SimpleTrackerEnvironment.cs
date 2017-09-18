using GoogleAnalyticsTracker.Core.Interface;
using System;

namespace GoogleAnalyticsTracker.Simple
{
    public class SimpleTrackerEnvironment : ITrackerEnvironment
    {
        /// <summary>
        /// Create a new SimpleTrackerEnvironment.
        /// </summary>
        /// <param name="osPlatform">OS platform, e.g. Environment.OSVersion.Platform.ToString()</param>
        /// <param name="osVersion">OS version, e.g. Environment.OSVersion.Version.ToString()</param>
        /// <param name="osVersionString">OS version string, e.g. Environment.OSVersion.VersionString</param>
        /// <example>
        /// var simpleTrackerEnvironment = new SimpleTrackerEnvironment(
        ///     Environment.OSVersion.Platform.ToString(),
        ///     Environment.OSVersion.Version.ToString(),
        ///     Environment.OSVersion.VersionString
        /// );
        /// </example>
        public SimpleTrackerEnvironment(string osPlatform, string osVersion, string osVersionString)
        {
            if (osPlatform == null) throw new ArgumentNullException(nameof(osPlatform));
            if (osVersion == null) throw new ArgumentNullException(nameof(osVersion));
            if (osVersionString == null) throw new ArgumentNullException(nameof(osVersionString));

            OsPlatform = osPlatform;
            OsVersion = osVersion;
            OsVersionString = osVersionString;
        }

        public string OsPlatform { get; set; }
        public string OsVersion { get; set; }
        public string OsVersionString { get; set; }
    }
}