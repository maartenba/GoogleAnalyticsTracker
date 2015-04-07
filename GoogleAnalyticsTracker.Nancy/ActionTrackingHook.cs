using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using System.Threading;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.Nancy
{
    public class ActionTrackingHook
    {
        private Func<NancyContext, bool> _isTrackableAction;

        public Tracker Tracker { get; set; }

        public Func<NancyContext, bool> IsTrackableAction
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

        public ActionTrackingHook()
			: this(null, null, null, null) 
        {
		}

		public ActionTrackingHook(string trackingAccount, string trackingDomain)
			: this(trackingAccount, trackingDomain, null, null)
        {
		}

		public ActionTrackingHook(string trackingAccount)
			: this(trackingAccount, null, null, null)
        {
		}

		public ActionTrackingHook(string trackingAccount, string trackingDomain, string actionDescription, string actionUrl)
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession(), new NancyTrackerEnvironment());
			ActionDescription = actionDescription;
			ActionUrl = actionUrl;
		}

		public ActionTrackingHook(Tracker tracker)
			: this(tracker, action => true) 
        {
		}

		public ActionTrackingHook(Tracker tracker, Func<NancyContext, bool> isTrackableAction) 
        {
			Tracker = tracker;
			IsTrackableAction = isTrackableAction;
		}

        public ActionTrackingHook(string trackingAccount, string trackingDomain, Func<NancyContext, bool> isTrackableAction) 
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession(), new NancyTrackerEnvironment());
			IsTrackableAction = isTrackableAction;
		}

        public virtual string BuildCurrentActionName(NancyContext context)
        {
            return ActionDescription;
        }

        public virtual string BuildCurrentActionUrl(NancyContext context)
        {
            var request = context.Request;

            return ActionUrl ?? (request.Url != null ? new Uri(request.Url).PathAndQuery : "");
        }

        public virtual void OnActionExecutingAsync(NancyContext context, CancellationToken cancellationToken)
        {
            if (IsTrackableAction(context))
            {
                var requireRequestAndResponse = Tracker.AnalyticsSession as IRequireRequestAndResponse;
                if (requireRequestAndResponse != null)
                {
                    requireRequestAndResponse.SetRequestAndResponse(context.Request, context.Response);
                }

                OnTrackingAction(context);
            }
        }

        public virtual Task<TrackingResult> OnTrackingAction(NancyContext context)
        {
            return Tracker.TrackPageViewAsync(context.Request, BuildCurrentActionName(context), BuildCurrentActionUrl(context));

        }
    }
}
