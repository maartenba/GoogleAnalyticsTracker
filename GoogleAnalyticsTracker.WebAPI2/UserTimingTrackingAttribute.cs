using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.WebAPI2
{
    public class UserTimingTrackingAttribute : AsyncActionFilterAttribute
    {
        DateTime _startTime;
        Func<HttpActionContext, bool> _isTrackableAction;
        public Tracker Tracker { get; set; }

        public string Category { get; }
        public string Var { get; }
        public string Label { get; }

        public UserTimingTrackingAttribute(string trackingAccount, string category, string var, string label = null)
        {
            Tracker = new Tracker(trackingAccount, new CookieBasedAnalyticsSession(), new AspNetWebApiTrackerEnvironment());

            Category = category;
            Var = var;
            Label = label;
        }

        public Func<HttpActionContext, bool> IsTrackableAction
        {
            get
            {
                if (_isTrackableAction != null)
                    return _isTrackableAction;

                return action => true;
            }
            set { _isTrackableAction = value; }
        }

        private long TotalMilliseconds
        {
            get { return Convert.ToInt64(new TimeSpan(DateTime.Now.Ticks - _startTime.Ticks).TotalMilliseconds); }
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (IsTrackableAction(actionContext))
                _startTime = DateTime.Now;

            return Task.FromResult(new object());
        }

        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (IsTrackableAction(actionExecutedContext.ActionContext))
                await OnTrackingUserTiming(actionExecutedContext.ActionContext);
        }

        public virtual async Task<TrackingResult> OnTrackingUserTiming(HttpActionContext filterContext)
        {
            return
                await
                    Tracker.TrackUserTimingAsync(filterContext.Request, BuildCurrentActionName(filterContext),
                        BuildCurrentActionUrl(filterContext), Category, Var, TotalMilliseconds, Label);
        }
    }
}