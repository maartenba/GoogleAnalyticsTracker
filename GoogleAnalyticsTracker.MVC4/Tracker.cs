using System.Configuration;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;
using System.Linq;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Mvc4
{
    public class Tracker : TrackerBase
    {
        public string UserLanguage { get; set; }

        public Tracker()
            : this(new AnalyticsSession())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(IAnalyticsSession analyticsSession)
            : this(ConfigurationManager.AppSettings[TrackingAccountConfigurationKey], ConfigurationManager.AppSettings[TrackingDomainConfigurationKey], analyticsSession, new AspNetMvc4TrackerEnvironment())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, new AnalyticsSession(), new AspNetMvc4TrackerEnvironment())
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

        protected override void AmendParameters(IGeneralParameters parameters)
        {
            base.AmendParameters(parameters);
            if (string.IsNullOrEmpty(parameters.UserLanguage))
            {
                parameters.UserLanguage = UserLanguage;
            }
        }

        private void PopulateUserAgentPropertiesFromHttpContext()
        {
            if (!IsHttpRequestAvailable()) return;

            UserAgent = System.Web.HttpContext.Current.Request.UserAgent;
            UserLanguage = System.Web.HttpContext.Current.Request.UserLanguages?.FirstOrDefault();
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
            catch (System.Web.HttpException)
            {
                return false;
            }
        }
    }
}
