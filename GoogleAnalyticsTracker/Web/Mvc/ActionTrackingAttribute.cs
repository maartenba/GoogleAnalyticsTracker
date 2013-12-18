using System;
using System.Web.Mvc;

namespace GoogleAnalyticsTracker.Web.Mvc
{
    public class ActionTrackingAttribute
        : ActionFilterAttribute
    {
        private Func<ActionDescriptor, bool> _isTrackableAction;

        public Tracker Tracker { get; set; }

				public bool UseAsync { get; set; }

        public Func<ActionDescriptor, bool> IsTrackableAction
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

            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession());
            ActionDescription = actionDescription;
            ActionUrl = actionUrl;
						UseAsync = false;
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
                   (request.Url != null ? request.Url.PathAndQuery : "");
        }

        public virtual void OnTrackingAction(ActionExecutingContext filterContext)
        {
          if(UseAsync)
					Tracker.TrackPageViewAsync(
                filterContext.RequestContext.HttpContext,
                BuildCurrentActionName(filterContext),
                BuildCurrentActionUrl(filterContext)
            );
					else
						Tracker.TrackPageView(
									filterContext.RequestContext.HttpContext,
									BuildCurrentActionName(filterContext),
									BuildCurrentActionUrl(filterContext)
							);
				}
    }
}