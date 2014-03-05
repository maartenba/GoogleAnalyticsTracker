using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.WebApi2 {
    public class ActionTrackingAttribute
            : AsyncActionFilterAttribute
    {
        private Func<HttpActionContext, bool> _isTrackableAction;

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

        public string ActionDescription { get; set; }
        public string ActionUrl { get; set; }

        public ActionTrackingAttribute()
            : this(null, null, null, null)
        {
        }

        public ActionTrackingAttribute(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, null, null)
        {
        }

        public ActionTrackingAttribute(string trackingAccount)
            : this(trackingAccount, null, null, null)
        {
        }

        public ActionTrackingAttribute(string trackingAccount, string trackingDomain, string actionDescription, string actionUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(trackingDomain) && System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
                {
                    trackingDomain = System.Web.HttpContext.Current.Request.Url.Host;
                }
            }
            catch
            {
                // intended
            }

            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession(), new AspNetWebApiTrackerEnvironment());
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

        public ActionTrackingAttribute(string trackingAccount, string trackingDomain, Func<HttpActionContext, bool> isTrackableAction)
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession(), new AspNetWebApiTrackerEnvironment());
            IsTrackableAction = isTrackableAction;
        }

        public async override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (IsTrackableAction(actionContext))
            {
                var requireRequestAndResponse = Tracker.AnalyticsSession as IRequireRequestAndResponse;
                if (requireRequestAndResponse != null)
                {
                    requireRequestAndResponse.SetRequestAndResponse(actionContext.Request, actionContext.Response);
                }

                await OnTrackingAction(actionContext);
            }
        }

        public virtual string BuildCurrentActionName(HttpActionContext filterContext)
        {
            return ActionDescription ??
                         filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " - " +
                         filterContext.ActionDescriptor.ActionName;
        }

        public virtual string BuildCurrentActionUrl(HttpActionContext filterContext)
        {
            var request = filterContext.Request;

            return ActionUrl ?? (request.RequestUri != null ? request.RequestUri.PathAndQuery : "");
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