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
    public class ActionTrackingHookAsync
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

        public ActionTrackingHookAsync()
			: this(null, null, null, null) 
        {
		}

		public ActionTrackingHookAsync(string trackingAccount, string trackingDomain)
			: this(trackingAccount, trackingDomain, null, null)
        {
		}

		public ActionTrackingHookAsync(string trackingAccount)
			: this(trackingAccount, null, null, null)
        {
		}

		public ActionTrackingHookAsync(string trackingAccount, string trackingDomain, string actionDescription, string actionUrl)
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession(), new NancyTrackerEnvironment());
			ActionDescription = actionDescription;
			ActionUrl = actionUrl;
		}

		public ActionTrackingHookAsync(Tracker tracker)
			: this(tracker, action => true) 
        {
		}

		public ActionTrackingHookAsync(Tracker tracker, Func<NancyContext, bool> isTrackableAction) 
        {
			Tracker = tracker;
			IsTrackableAction = isTrackableAction;
		}

        public ActionTrackingHookAsync(string trackingAccount, string trackingDomain, Func<NancyContext, bool> isTrackableAction) 
        {
            Tracker = new Tracker(trackingAccount, trackingDomain, new CookieBasedAnalyticsSession(), new NancyTrackerEnvironment());
			IsTrackableAction = isTrackableAction;
		}

        public string BuildCurrentActionName(NancyContext context)
        {
            var request = context.Request;

            return ActionDescription ?? (request.Url != null ? new Uri(request.Url).AbsolutePath : ""); ;
        }

        public virtual string BuildCurrentActionUrl(NancyContext context)
        {
            var request = context.Request;

            return ActionUrl ?? (request.Url != null ? new Uri(request.Url).PathAndQuery : "");
        }

        public async Task OnActionExecutingAsync(NancyContext context, CancellationToken cancellationToken)
        {
            if (IsTrackableAction(context))
            {
                var requireRequestAndResponse = Tracker.AnalyticsSession as IRequireRequestAndResponse;
                if (requireRequestAndResponse != null)
                {
                    requireRequestAndResponse.SetRequestAndResponse(context.Request, context.Response);
                }

                await OnTrackingAction(context);
            }
        }

        public async Task<TrackingResult> OnTrackingAction(NancyContext context)
        {
            return await Tracker.TrackPageViewAsync(context.Request, BuildCurrentActionName(context), BuildCurrentActionUrl(context));

        }
    }
}
