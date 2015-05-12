namespace GoogleAnalyticsTracker.Owin
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Owin;

    public class AnalyticsMiddleware : OwinMiddleware
    {
        private readonly string trackingId;

        private readonly string baseUri;

        public AnalyticsMiddleware(OwinMiddleware next, string trackingId, string baseUri)
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

            this.trackingId = trackingId;
            this.baseUri = baseUri;
        }

        public override async Task Invoke(IOwinContext context)
        {
            using (var tracker = new Tracker(context, this.trackingId, this.baseUri)
                                     {
                                         ThrowOnErrors = true,
                                         UseSsl = true
                                     })
            {
                // Run the original request
                await this.Next.Invoke(context);

                await tracker.TrackPageViewAsync();
            }
        }
    }
}