using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace GoogleAnalyticsTracker.AspNet;

[PublicAPI]
public static class TrackRequests
{
    /// <summary>Track all requests.</summary>
    public static readonly Func<HttpContext, bool> Yes = _ => true;

    /// <summary>Track all requests that have not yet been tracked. Use this option when decorating controllers and actions with <see cref="TrackPageViewAttribute"/>.</summary>
    public static readonly Func<HttpContext, bool> OnlyWhenNotYetTracked = context =>
        context.Items.ContainsKey(GoogleAnalyticsTrackerMiddleware.TrackPageViewHandledMarker);

    /// <summary>Ignore all requests.</summary>
    public static readonly Func<HttpContext, bool> No = _ => false;
}