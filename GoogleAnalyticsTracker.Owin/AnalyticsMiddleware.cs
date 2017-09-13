using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace GoogleAnalyticsTracker.Owin
{
    public class AnalyticsMiddleware : OwinMiddleware
    {
        private readonly string _trackingId;

        public AnalyticsMiddleware(OwinMiddleware next, string trackingId)
            : base(next)
        {
            if (next == null)
            {
                throw new ArgumentNullException("next");
            }

            if (trackingId == null)
            {
                throw new ArgumentNullException("trackingId");
            }

            _trackingId = trackingId;
        }

        public override async Task Invoke(IOwinContext context)
        {
            using (var tracker = new Tracker(context, _trackingId)
                                     {
                                         ThrowOnErrors = true
                                     })
            {
                // Run the original request
                await Next.Invoke(context);
                await tracker.TrackPageViewAsync();
            }
        }
    }
}