using System;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GoogleAnalyticsTracker.AspNet
{
    [PublicAPI]
    public class AspNetCoreTracker : TrackerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;

        private static IAnalyticsSession SetupAnalyticsSession(
            IHttpContextAccessor contextAccessor,
            IOptions<GoogleAnalyticsTrackerOptions> optionsAccessor)
        {
            var session = new CookieBasedAnalyticsSession(contextAccessor);
            optionsAccessor.Value.CustomizeAnalyticsSession?.Invoke(session);
            return session;
        }

        private static ITrackerEnvironment SetupTrackerEnvironment(
            IOptions<GoogleAnalyticsTrackerOptions> optionsAccessor)
        {
            var trackerEnvironment = new AspNetCoreTrackerEnvironment();
            optionsAccessor.Value.CustomizeTrackerEnvironment?.Invoke(trackerEnvironment);
            return trackerEnvironment;
        }

        public AspNetCoreTracker(
            [NotNull] IHttpContextAccessor contextAccessor,
            IOptions<GoogleAnalyticsTrackerOptions> optionsAccessor)
            : base(
                optionsAccessor.Value.TrackerId,
                SetupAnalyticsSession(contextAccessor, optionsAccessor),
                SetupTrackerEnvironment(optionsAccessor))
        {
            _contextAccessor = contextAccessor;
        }

        [UsedImplicitly]
        public Task TrackEventAsync(Exception ex)
        {
            return TrackEventAsync("Errors", ex.Message, ex.HResult);
        }

        [UsedImplicitly]
        public async Task TrackEventAsync(string category, string action, int value)
        {
            // If no HTTP context available, bail out...
            if (_contextAccessor.HttpContext == null) return;
            
            var eventTrackingParameters = new EventTracking
            {
                Category = category,
                Action = action,
                Label = GetRelativeUrl(),
                Value = value,
                DocumentHostName = _contextAccessor.HttpContext.Request.Host.Value,
                UserAgent = _contextAccessor.HttpContext.Request.Headers["User-Agent"],
                UserLanguage = _contextAccessor.HttpContext.Request.Headers["Accept-Language"],
                DocumentReferrer = _contextAccessor.HttpContext.Request.Headers["Referrer"],
                IpOverride = Environment.GetEnvironmentVariable("server.RemoteIpAddress"),
                UserId = _contextAccessor.HttpContext.User.Identity.Name
            };

            await TrackAsync(eventTrackingParameters);
        }

        [UsedImplicitly]
        public Task TrackPageViewAsync()
        {
            return TrackPageViewAsync(null);
        }

        [UsedImplicitly]
        public async Task TrackPageViewAsync([CanBeNull] string customTitle)
        {
            // If no HTTP context available, bail out...
            if (_contextAccessor.HttpContext == null) return;
            
            var pageviewTrackingParameters = new PageView
            {
                DocumentTitle = customTitle ?? _contextAccessor.HttpContext.Request.Path.ToString(),
                DocumentLocationUrl = GetRelativeUrl(),
                DocumentHostName = _contextAccessor.HttpContext.Request.Host.Value,
                UserAgent = _contextAccessor.HttpContext.Request.Headers["User-Agent"],
                UserLanguage = _contextAccessor.HttpContext.Request.Headers["Accept-Language"],
                DocumentReferrer = _contextAccessor.HttpContext.Request.Headers["Referrer"],
                IpOverride = Environment.GetEnvironmentVariable("server.RemoteIpAddress"),
                UserId = _contextAccessor.HttpContext.User.Identity.Name,
            };

            await TrackAsync(pageviewTrackingParameters);
        }

        private string GetRelativeUrl()
        {
            return string.IsNullOrEmpty(_contextAccessor.HttpContext.Request.QueryString.ToString())
                ? _contextAccessor.HttpContext.Request.Path.ToString()
                : string.Format("{0}{1}", _contextAccessor.HttpContext.Request.Path, _contextAccessor.HttpContext.Request.QueryString);
        }
    }
}