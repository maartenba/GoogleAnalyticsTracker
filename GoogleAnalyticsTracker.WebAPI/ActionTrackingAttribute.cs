using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GoogleAnalyticsTracker.WebApi {
    public class ActionTrackingAttribute
			: ActionFilterAttribute 
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

		public override void OnActionExecuting(HttpActionContext filterContext) 
        {
			if (IsTrackableAction(filterContext)) {
				OnTrackingAction(filterContext);
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

        public virtual void OnTrackingAction(HttpActionContext filterContext)
        {
            var requireRequestAndResponse = Tracker.AnalyticsSession as IRequireRequestAndResponse;
            if (requireRequestAndResponse != null)
            {
                requireRequestAndResponse.SetRequestAndResponse(filterContext.Request, filterContext.Response);
            }

            // todo: we should await the result
            Tracker.TrackPageViewAsync(
                filterContext.Request,
                BuildCurrentActionName(filterContext),
                BuildCurrentActionUrl(filterContext));
        }
    }
}