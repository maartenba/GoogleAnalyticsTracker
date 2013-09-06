using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GoogleAnalyticsTracker.Web.WebApi {
	public class ActionTrackingAttribute
			: ActionFilterAttribute {
		private Func<HttpActionDescriptor, bool> _isTrackableAction;

		public Tracker Tracker { get; set; }
		public Func<HttpActionDescriptor, bool> IsTrackableAction {
			get {
				if (_isTrackableAction != null) {
					return _isTrackableAction;
				}
				return action => true;
			}
			set { _isTrackableAction = value; }
		}

		public string ActionDescription { get; set; }
		public string ActionUrl { get; set; }

		public ActionTrackingAttribute()
			: this(null, null, null, null) {
		}

		public ActionTrackingAttribute(string trackingAccount, string trackingDomain)
			: this(trackingAccount, trackingDomain, null, null) {
		}

		public ActionTrackingAttribute(string trackingAccount)
			: this(trackingAccount, null, null, null) {
		}

		public ActionTrackingAttribute(string trackingAccount, string trackingDomain, string actionDescription, string actionUrl) {
			if (string.IsNullOrEmpty(trackingDomain) && System.Web.HttpContext.Current != null) {
				trackingDomain = System.Web.HttpContext.Current.Request.Url.Host;
			}

			Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession());
			ActionDescription = actionDescription;
			ActionUrl = actionUrl;
		}

		public ActionTrackingAttribute(Tracker tracker)
			: this(tracker, action => true) {
		}

		public ActionTrackingAttribute(Tracker tracker, Func<HttpActionDescriptor, bool> isTrackableAction) {
			Tracker = tracker;
			IsTrackableAction = isTrackableAction;
		}

		public ActionTrackingAttribute(string trackingAccount, string trackingDomain, Func<HttpActionDescriptor, bool> isTrackableAction) {
			Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession());
			IsTrackableAction = isTrackableAction;
		}

		public override void OnActionExecuting(HttpActionContext filterContext) {
			if (IsTrackableAction(filterContext.ActionDescriptor)) {
				OnTrackingAction(filterContext);
			}
		}

		public virtual string BuildCurrentActionName(HttpActionContext filterContext) {
			return ActionDescription ??
						 filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " - " +
						 filterContext.ActionDescriptor.ActionName;
		}

		public virtual string BuildCurrentActionUrl(HttpActionContext filterContext) {
			var request = filterContext.Request;

			return ActionUrl ??
						 (request.RequestUri != null ? request.RequestUri.PathAndQuery : "");
		}

		public virtual void OnTrackingAction(HttpActionContext filterContext) {
			Tracker.TrackPageView(
					filterContext.Request,
					BuildCurrentActionName(filterContext),
					BuildCurrentActionUrl(filterContext)
			);
		}
	}
}