using System.Configuration;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.Nancy
{
    public class Tracker
            : TrackerBase
    {
        public Tracker()
            : this(new AnalyticsSession())
        {
        }

        public Tracker(IAnalyticsSession analyticsSession)
            : this(ConfigurationManager.AppSettings[TrackingAccountConfigurationKey], analyticsSession, new NancyTrackerEnvironment())
        {
        }

        public Tracker(string trackingAccount)
            : this(trackingAccount, new AnalyticsSession(), new NancyTrackerEnvironment())
        {
        }


        public Tracker(string trackingAccount, ITrackerEnvironment trackerEnvironment)
            : base(trackingAccount, trackerEnvironment)
        {
        }

        public Tracker(string trackingAccount, IAnalyticsSession analyticsSession, ITrackerEnvironment trackerEnvironment)
            : base(trackingAccount, analyticsSession, trackerEnvironment)
        {
        }
    }
}
