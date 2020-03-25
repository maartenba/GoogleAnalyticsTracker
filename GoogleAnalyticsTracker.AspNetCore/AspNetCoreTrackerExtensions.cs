using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace GoogleAnalyticsTracker.AspNet
{
    [PublicAPI]
    public static class AspNetCoreTrackerExtensions
    {
        public static async Task<TrackingResult> TrackUserTimingAsync(this AspNetCoreTracker tracker, HttpContext httpContext, string pageTitle, string pageUrl, string category, string var, long value, string label = null)
        {
            var userTimingParameters = new UserTimings
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                UserAgent = httpContext.Request.Headers[HeaderNames.UserAgent].ToString(),
                DocumentHostName = httpContext.Request.Host.Value,
                UserLanguage = httpContext.Request.Headers[HeaderNames.AcceptLanguage].ToString().ToLower(),
                UserTimingCategory = category,
                UserTimingVariable = var,
                UserTimingTime = value,
                UserTimingLabel = label          
            };

            return await tracker.TrackAsync(userTimingParameters);
        }
        
        public static async Task<TrackingResult> TrackPageViewAsync(this AspNetCoreTracker tracker, HttpContext httpContext, string pageTitle, string pageUrl = null)
        {
            pageUrl ??= httpContext.Request.Path;

            var pageViewParameters = new PageView
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                UserAgent = httpContext.Request.Headers[HeaderNames.UserAgent].ToString(),
                DocumentHostName = httpContext.Request.Host.Value,
                UserLanguage = httpContext.Request.Headers[HeaderNames.AcceptLanguage].ToString().ToLower(),
                IpOverride = httpContext.Connection.RemoteIpAddress.ToString()
            };

            return await tracker.TrackAsync(pageViewParameters);
        }
    }
}