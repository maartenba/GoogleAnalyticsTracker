using System;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using Microsoft.Owin;

namespace GoogleAnalyticsTracker.Owin
{
    public class Tracker : TrackerBase
    {
        private readonly IOwinContext _context;

        public Tracker(IOwinContext context, string trackingAccount, string trackingDomain)
            : base(
                trackingAccount,
                trackingDomain,
                new CookieBasedAnalyticsSession(context),
                new OwinTrackerEnvironment(context))
        {
            _context = context;
        }

        public async Task TrackEventAsync(Exception ex)
        {
            var eventTrackingParameters = new EventTracking
            {
                Category = "Errors",
                Action = ex.Message,
                Label = GetRelativeUrl(),
                Value = ex.HResult,
                DocumentHostName = _context.Request.Uri.Host,
                UserAgent = _context.Request.Headers["User-Agent"],
                UserLanguage = _context.Request.Headers["Accept-Language"],
                DocumentReferrer = _context.Request.Headers["Referrer"],
                IpOverride = _context.Request.Environment["server.RemoteIpAddress"] != null ? _context.Request.Environment["server.RemoteIpAddress"].ToString() : null,
                UserId = _context.Authentication.User.Identity.Name
            };

            await base.TrackEventAsync(eventTrackingParameters);
        }

        public async Task TrackPageViewAsync()
        {
            var pageviewTrackingParameters = new PageView
            {
                DocumentTitle = _context.Request.Path.ToString(),
                DocumentLocationUrl = GetRelativeUrl(),
                DocumentHostName = _context.Request.Uri.Host,
                UserAgent = _context.Request.Headers["User-Agent"],
                UserLanguage = _context.Request.Headers["Accept-Language"],
                DocumentReferrer = _context.Request.Headers["Referrer"],
                IpOverride = _context.Request.Environment["server.RemoteIpAddress"] != null ? _context.Request.Environment["server.RemoteIpAddress"].ToString() : null,
                UserId = _context.Authentication.User.Identity.Name
            };

            await base.TrackPageViewAsync(pageviewTrackingParameters);
        }

        private string GetRelativeUrl()
        {
            return string.IsNullOrEmpty(_context.Request.QueryString.ToString())
                       ? _context.Request.Path.ToString()
                       : string.Format("{0}{1}", _context.Request.Path, _context.Request.QueryString);
        }
    }
}