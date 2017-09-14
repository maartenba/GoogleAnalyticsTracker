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
        public SimpleTracker(string trackingAccount, ITrackerEnvironment trackerEnvironment) 
            : base(trackingAccount, trackerEnvironment)
        {
        }

        /// <summary>
        /// Creates a new SimpleTracker. See <see cref="SimpleTracker(string, string)"/> for details.
        /// </summary>
        /// <param name="trackingAccount"></param>
        /// <param name="analyticsSession"></param>
        /// <param name="trackerEnvironment"></param>
        public SimpleTracker(string trackingAccount, IAnalyticsSession analyticsSession, ITrackerEnvironment trackerEnvironment)
            : base(trackingAccount, analyticsSession, trackerEnvironment)
        {
        }

    }
}