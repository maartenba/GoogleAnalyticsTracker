using System;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace GoogleAnalyticsTracker.Web.Api
{
    public class ActionTrackingAttribute : ActionFilterAttribute
    {
        private Func<HttpActionDescriptor, bool> _isTrackableAction;

        public Tracker Tracker { get; set; }
        public Func<HttpActionDescriptor, bool> IsTrackableAction
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

        public ActionTrackingAttribute(string trackingAccount, string trackingDomain, string actionDescription, string actionUrl)
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession());
            ActionDescription = actionDescription;
            ActionUrl = actionUrl;
        }

        public ActionTrackingAttribute(Tracker tracker)
            : this(tracker, action => true)
        {
        }

        public ActionTrackingAttribute(Tracker tracker, Func<HttpActionDescriptor, bool> isTrackableAction)
        {
            Tracker = tracker;
            IsTrackableAction = isTrackableAction;
        }

        public ActionTrackingAttribute(string trackingAccount, string trackingDomain, Func<HttpActionDescriptor, bool> isTrackableAction)
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession());
            IsTrackableAction = isTrackableAction;
        }
        /*
        public static void RegisterGlobalFilter(string trackingAccount, string trackingDomain)
        {
            
            GlobalFilters.Filters.Add(new ActionTrackingAttribute(trackingAccount, trackingDomain));
        }

        public static void RegisterGlobalFilter(Tracker tracker)
        {
            GlobalFilters.Filters.Add(new ActionTrackingAttribute(tracker));
        }
        */
        public override void OnActionExecuting(HttpActionContext  actionContext)
        {
            if (IsTrackableAction(actionContext.ActionDescriptor))
            {
                OnTrackingAction(actionContext);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext  actionContext)
        {
            base.OnActionExecuted(actionContext);
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

            return ActionUrl ??
                   (request.RequestUri != null ? request.RequestUri.ToString() : "");
        }

        public virtual void OnTrackingAction(HttpActionContext filterContext)
        {

            Tracker.TrackPageView(
                filterContext.Request,
                BuildCurrentActionName(filterContext),
                BuildCurrentActionUrl(filterContext)
            );
        }
    }
}
