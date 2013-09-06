using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace GoogleAnalyticsTracker.Web.WebApi {
	public static class ConfigurationHelper {
		public static void RegisterTracker(this HttpConfiguration configuration, string trackingAccount, string trackingDomain = null) {
			configuration.Filters.Add(new ActionTrackingAttribute(trackingAccount, trackingDomain));
		}

		public static void RegisterTracker(this HttpConfiguration configuration, Tracker tracker) {
			configuration.Filters.Add(new ActionTrackingAttribute(tracker));
		}
	}
}
