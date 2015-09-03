using System.Threading.Tasks;
using System.Web;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.MVC5
{
    public static class PageViewTrackerExtensions
    {        
        public static async Task<TrackingResult> TrackPageViewAsync(this Tracker tracker, HttpContextBase httpContext, string pageTitle, string pageUrl = null)
        {
            var pageViewParameters = new PageView
            {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                UserAgent = httpContext.Request.UserAgent,
                DocumentHostName = httpContext.Request.UserHostName,
                UserLanguage = httpContext.Request.UserLanguages != null ? string.Join(";",  httpContext.Request.UserLanguages).ToLower() : null,
                ReferralUrl = httpContext.Request.UrlReferrer != null ? httpContext.Request.UrlReferrer.ToString() : null,
                CacheBuster = tracker.AnalyticsSession.GenerateCacheBuster()
            };

            return await tracker.TrackAsync(pageViewParameters);
        }
    }
}