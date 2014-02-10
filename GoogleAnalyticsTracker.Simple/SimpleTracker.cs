using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.Simple
{
    public class SimpleTracker
        : TrackerBase
    {
        public SimpleTracker(string trackingAccount, string trackingDomain)
            : base(trackingAccount, trackingDomain, new AnalyticsSession(), new SimpleTrackerEnvironment())
        {
        }


        public SimpleTracker(string trackingAccount, string trackingDomain, ITrackerEnvironment trackerEnvironment) 
            : base(trackingAccount, trackingDomain, new AnalyticsSession(), trackerEnvironment)
        {
        }
    }
}