namespace GoogleAnalyticsTracker.Owin
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GoogleAnalyticsTracker.Core;

    using Microsoft.Owin;

    public class Tracker : TrackerBase
    {
        private readonly IOwinContext context;

        public Tracker(IOwinContext context, string trackingAccount, string trackingDomain)
            : base(
                trackingAccount,
                trackingDomain,
                new CookieBasedAnalyticsSession(context),
                new OwinTrackerEnvironment(context))
        {
            this.context = context;
        }

        public async Task TrackEventAsync(Exception ex)
        {
            const string Category = "Errors";

            var action = ex.Message;
            var label = this.GetRelativeUrl();
            var value = ex.HResult;
            var beacons = this.BuildBeaconParameters();
            var userAgent = this.context.Request.Headers["User-Agent"];

            await base.TrackEventAsync(Category, action, label, value, false, beacons, userAgent);
        }

        public async Task TrackPageViewAsync()
        {
            var title = this.context.Request.Path.ToString();
            var url = this.GetRelativeUrl();
            var beacons = this.BuildBeaconParameters();
            var userAgent = this.context.Request.Headers["User-Agent"];

            await base.TrackPageViewAsync(title, url, beacons, userAgent);
        }

        private string GetRelativeUrl()
        {
            return string.IsNullOrEmpty(this.context.Request.QueryString.ToString())
                       ? this.context.Request.Path.ToString()
                       : string.Format("{0}{1}", this.context.Request.Path, this.context.Request.QueryString);
        }

        private Dictionary<string, string> BuildBeaconParameters()
        {
            var beacons = new Dictionary<string, string>
                                  {
                                      {
                                          BeaconParameter.HostName, this.context.Request.Uri.Host
                                      }
                                  };

            if (this.context.Request.Headers["Accept-Language"] != null)
            {
                beacons.Add(BeaconParameter.Browser.Language, this.context.Request.Headers["Accept-Language"]);
            }

            if (this.context.Request.Headers["Referrer"] != null)
            {
                beacons.Add(BeaconParameter.Browser.ReferralUrl, this.context.Request.Headers["Referrer"]);
            }

            if (this.context.Request.Environment["server.RemoteIpAddress"] != null)
            {
                beacons.Add("uip", this.context.Request.Environment["server.RemoteIpAddress"].ToString());
            }

            if (this.context.Authentication.User != null && this.context.Authentication.User.Identity.IsAuthenticated)
            {
                beacons.Add("uid", this.context.Authentication.User.Identity.Name);
            }

            return beacons;
        }
    }
}