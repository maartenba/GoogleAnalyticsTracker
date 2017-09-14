using System.Configuration;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using System.Linq;

namespace GoogleAnalyticsTracker.MVC5
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
            : this(ConfigurationManager.AppSettings[TrackingAccountConfigurationKey], analyticsSession, new AspNetMvc5TrackerEnvironment())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(string trackingAccount)
            : this(trackingAccount, new AnalyticsSession(), new AspNetMvc5TrackerEnvironment())
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }


        public Tracker(string trackingAccount, ITrackerEnvironment trackerEnvironment) 
            : base(trackingAccount, trackerEnvironment)
        {
            PopulateUserAgentPropertiesFromHttpContext();
        }

        public Tracker(string trackingAccount, IAnalyticsSession analyticsSession, ITrackerEnvironment trackerEnvironment)
            : base(trackingAccount, analyticsSession, trackerEnvironment)
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
