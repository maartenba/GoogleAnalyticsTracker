using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.WebApi
{
    public class ActionTrackingAttribute : AsyncActionFilterAttribute
    {
        Func<HttpActionContext, bool> _isTrackableAction;
        public Tracker Tracker { get; set; }

        public Func<HttpActionContext, bool> IsTrackableAction
        {
            get
            {
                if (_isTrackableAction != null)
                {
                    return _isTrackableAction;
                }
                return action => true;
            }
            set { _isTrackableAction = value; }
        }
        
        public ActionTrackingAttribute()
            : this(null, null, null)
        {
        }

        public ActionTrackingAttribute(string trackingAccount)
            : this(trackingAccount, null, null)
        {
        }

        public ActionTrackingAttribute(string trackingAccount, string actionDescription, string actionUrl)
        {
            Tracker = new Tracker(trackingAccount, new CookieBasedAnalyticsSession(), new AspNetWebApiTrackerEnvironment());
            ActionDescription = actionDescription;
            ActionUrl = actionUrl;
        }

        public ActionTrackingAttribute(Tracker tracker)
            : this(tracker, action => true)
        {
        }

        public ActionTrackingAttribute(Tracker tracker, Func<HttpActionContext, bool> isTrackableAction)
        {
            Tracker = tracker;
            IsTrackableAction = isTrackableAction;
        }

        public ActionTrackingAttribute(string trackingAccount, Func<HttpActionContext, bool> isTrackableAction)
        {
            Tracker = new Tracker(trackingAccount, new CookieBasedAnalyticsSession(), new AspNetWebApiTrackerEnvironment());
            IsTrackableAction = isTrackableAction;
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (IsTrackableAction(actionContext))
            {
                var requireRequestAndResponse = Tracker.AnalyticsSession as IRequireRequestAndResponse;
                if (requireRequestAndResponse != null)               
                    requireRequestAndResponse.SetRequestAndResponse(actionContext.Request, actionContext.Response);             

                await OnTrackingAction(actionContext);
            }
        }

        public virtual async Task<TrackingResult> OnTrackingAction(HttpActionContext filterContext)
        {
            return await Tracker.TrackPageViewAsync(
                filterContext.Request,
                BuildCurrentActionName(filterContext),
                BuildCurrentActionUrl(filterContext));
        }
    }
}