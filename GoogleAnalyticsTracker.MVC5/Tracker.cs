using System.Configuration;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.MVC5
{
    public class Tracker : TrackerBase
    {
        public Tracker()
            : this(new AnalyticsSession())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(IAnalyticsSession analyticsSession)
            : this(ConfigurationManager.AppSettings[TrackingAccountConfigurationKey], ConfigurationManager.AppSettings[TrackingDomainConfigurationKey], analyticsSession, new AspNetMvc5TrackerEnvironment())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, new AnalyticsSession(), new AspNetMvc5TrackerEnvironment())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }


        public Tracker(string trackingAccount, string trackingDomain, ITrackerEnvironment trackerEnvironment) 
            : base(trackingAccount, trackingDomain, trackerEnvironment)
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(string trackingAccount, string trackingDomain, IAnalyticsSession analyticsSession, ITrackerEnvironment trackerEnvironment)
            : base(trackingAccount, trackingDomain, analyticsSession, trackerEnvironment)
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        private void PopulateUserAgentPropertiesFromHttpContext()
        {
            if (!IsHttpRequestAvailable()) return;

            UserAgent = System.Web.HttpContext.Current.Request.UserAgent;
            Language = System.Web.HttpContext.Current.Request.UserLanguages != null
                ? string.Join(";", System.Web.HttpContext.Current.Request.UserLanguages)
                : string.Empty;
        }

        protected bool IsHttpRequestAvailable()
        {
            if (System.Web.HttpContext.Current == null)
                return false;

            try
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                return System.Web.HttpContext.Current.Request == null;
            }
            catch (System.Web.HttpException ex)
            {
                return false;
            }
        }
    }
}
