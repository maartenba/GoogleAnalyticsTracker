using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.Simple
{
    public class SimpleTracker : TrackerBase
    {
        /// <summary>
        /// Creates a new SimpleTracker.
        /// </summary>
        /// <param name="trackingAccount">Google Analytics tracking account</param>
        /// <param name="trackingDomain">Google Analytics tracking domain</param>
        /// <param name="trackerEnvironment">Tracking environment</param>
        /// <example>
        /// var tracker = new SimpleTracker("UA-XXXXX", "example.com", 
        ///     new SimpleTrackerEnvironment(
        ///         Dns.GetHostName(),
        ///         Environment.OSVersion.Platform.ToString(),
        ///         Environment.OSVersion.Version.ToString(),
        ///         Environment.OSVersion.VersionString
        /// ));
        /// </example>
        public SimpleTracker(string trackingAccount, string trackingDomain, ITrackerEnvironment trackerEnvironment) 
            : base(trackingAccount, trackingDomain, new AnalyticsSession(), trackerEnvironment)
        {
        }
    }
}