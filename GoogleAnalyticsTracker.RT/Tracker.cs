using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.RT
{
    public class Tracker
        : TrackerBase
    {
        public Tracker(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, new AnalyticsSession(), new WinRtTrackerEnvironment())
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
