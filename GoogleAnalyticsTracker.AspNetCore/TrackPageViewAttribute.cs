using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleAnalyticsTracker.AspNet;

/// <summary>
/// Track ASP.NET MVC page view.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class TrackPageViewAttribute : Attribute, IAsyncActionFilter
{
    /// <summary>
    /// Is tracking for this action enabled? Defaults to true.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Custom document title to track.
    /// </summary>
    public string? CustomTitle { get; set; }
        
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await next();

        if (Enabled)
        {
            var tracker = context.HttpContext.RequestServices.GetRequiredService<AspNetCoreTracker>();
            await tracker.TrackPageViewAsync(CustomTitle);
        }
            
        context.HttpContext.Items[GoogleAnalyticsTrackerMiddleware.TrackPageViewHandledMarker] = true;
    }
}