using System;
using GoogleAnalyticsTracker.Core.Interface;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace GoogleAnalyticsTracker.AspNet
{
    [PublicAPI]
    public class GoogleAnalyticsTrackerOptions
    {
        /// <summary>
        /// Google Analytics ID / tracking identifier.
        /// </summary>
        public string TrackerId { get; set; }
        
        /// <summary>
        /// Tracker environment customization.
        /// </summary>
        public Func<ITrackerEnvironment, ITrackerEnvironment> CustomizeTrackerEnvironment { get; set; }

        /// <summary>
        /// Analytics session customization.
        /// </summary>
        public Func<IAnalyticsSession, IAnalyticsSession> CustomizeAnalyticsSession { get; set; }

        /// <summary>
        /// Should the request be tracked? 
        /// </summary>
        public Func<HttpContext, bool> ShouldTrackRequestInMiddleware { get; set; } = TrackRequests.Yes;
    }
}