using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.MVC5
{
    public class ActionTrackingAttribute : ActionFilterAttribute
    {
        private Func<ActionDescriptor, bool> _isTrackableAction;

        public Tracker Tracker { get; set; }

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
            : this(null, null, null)
        {
        }

        public ActionTrackingAttribute(string trackingAccount)
            : this(trackingAccount, null, null)
        {
        }

        public ActionTrackingAttribute(string trackingAccount, string actionDescription, string actionUrl)
        {
            Tracker = new Tracker(trackingAccount, new CookieBasedAnalyticsSession(), new AspNetMvc5TrackerEnvironment());
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

        public ActionTrackingAttribute(string trackingAccount, Func<ActionDescriptor, bool> isTrackableAction)
        {
            Tracker = new Tracker(trackingAccount, new CookieBasedAnalyticsSession(), new AspNetMvc5TrackerEnvironment());
            IsTrackableAction = isTrackableAction;
        }

        public static void RegisterGlobalFilter(string trackingAccount)
        {
            GlobalFilters.Filters.Add(new ActionTrackingAttribute(trackingAccount));
        }

        public static void RegisterGlobalFilter(Tracker tracker)
        {
            GlobalFilters.Filters.Add(new ActionTrackingAttribute(tracker));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsTrackableAction(filterContext.ActionDescriptor))
            {
                AsyncHelper.RunSync(() => OnTrackingAction(filterContext));
            }
        }

        public virtual string BuildCurrentActionName(ActionExecutingContext filterContext)
        {
            return ActionDescription ??
                   string.Format("{0} - {1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                       filterContext.ActionDescriptor.ActionName);
        }

        public virtual string BuildCurrentActionUrl(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;

            return ActionUrl ?? (request.Url != null ? request.Url.PathAndQuery : string.Empty);
        }

        public virtual async Task<TrackingResult> OnTrackingAction(ActionExecutingContext filterContext)
        {
            return await Tracker.TrackPageViewAsync(
                filterContext.RequestContext.HttpContext,
                BuildCurrentActionName(filterContext),
                BuildCurrentActionUrl(filterContext));
        }
    }
}