using System;
using System.Web.Mvc;

namespace GoogleAnalyticsTracker.Web.Mvc
{
    public class ActionTrackingAttribute
        : ActionFilterAttribute
    {
        public Tracker Tracker { get; set; }
        public Func<ActionDescriptor, bool> IsTrackableAction { get; set; }

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

        public ActionTrackingAttribute(string trackingAccount, string trackingDomain, string actionDescription,
                                       string actionUrl)
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession());
            ActionDescription = actionDescription;
            ActionUrl = actionUrl;
        }

        public ActionTrackingAttribute(Tracker tracker)
            : this(tracker, action => true)
        {
        }

        public ActionTrackingAttribute(Tracker tracker, Func<ActionDescriptor, bool> isTrackableAction)
        {
            Tracker = tracker;
            IsTrackableAction = isTrackableAction;
        }

        public ActionTrackingAttribute(string trackingAccount, string trackingDomain, Func<ActionDescriptor, bool> isTrackableAction)
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession());
            IsTrackableAction = isTrackableAction;
        }

        public static void RegisterGlobalFilter(string trackingAccount, string trackingDomain)
        {
            GlobalFilters.Filters.Add(new ActionTrackingAttribute(trackingAccount, trackingDomain));
        }

        public static void RegisterGlobalFilter(Tracker tracker)
        {
            GlobalFilters.Filters.Add(new ActionTrackingAttribute(tracker));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsTrackableAction(filterContext.ActionDescriptor))
            {
                OnTrackingAction(filterContext);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        public virtual string BuildCurrentActionName(ActionExecutingContext filterContext)
        {
            return ActionDescription ??
                   filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " - " +
                   filterContext.ActionDescriptor.ActionName;
        }

        public virtual string BuildCurrentActionUrl(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;

            return ActionUrl ??
                   (request.Url != null ? request.Url.ToString() : "");
        }

        public virtual void OnTrackingAction(ActionExecutingContext filterContext)
        {
            Tracker.TrackPageView(
                filterContext.RequestContext.HttpContext,
                BuildCurrentActionName(filterContext),
                BuildCurrentActionUrl(filterContext)
            );
        }
    }
}