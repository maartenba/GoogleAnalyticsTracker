using System;
using Owin;

namespace GoogleAnalyticsTracker.Owin
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds Google Analytics tracking to the OWIN pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="trackingId">The tracking identifier.</param>
        /// <param name="baseUri">The base URI.</param>
        /// <returns>The application builder.</returns>
        public static IAppBuilder UseGoogleAnalyticsTracker(this IAppBuilder app, string trackingId, string baseUri)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (string.IsNullOrEmpty(trackingId))
            {
                throw new ArgumentNullException("trackingId");
            }

            if (string.IsNullOrEmpty(baseUri))
            {
                throw new ArgumentNullException("baseUri");
            }

            app.Use<AnalyticsMiddleware>(trackingId, baseUri);

            return app;
        }
    }
}