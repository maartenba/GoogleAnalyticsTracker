using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.WP8
{
    public class Tracker
        : TrackerBase
    {
        public Tracker(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, new WindowsPhoneAnalyticsSession(), new WindowsPhoneTrackerEnvironment())
        {
        }


        public Tracker(string trackingAccount, string trackingDomain, ITrackerEnvironment trackerEnvironment) 
            : base(trackingAccount, trackingDomain, trackerEnvironment)
        {
        }

        public Tracker(string trackingAccount, string trackingDomain, IAnalyticsSession analyticsSession, ITrackerEnvironment trackerEnvironment)
            : base(trackingAccount, trackingDomain, analyticsSession, trackerEnvironment)
        {
        }
    }
}
