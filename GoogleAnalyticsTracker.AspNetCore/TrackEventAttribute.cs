using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleAnalyticsTracker.AspNet;

/// <summary>
/// Track ASP.NET MVC vent.
/// </summary>
[PublicAPI]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class TrackEventAttribute : Attribute, IAsyncActionFilter
{
    /// <summary>
    /// Is tracking for this action enabled? Defaults to true.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Category for the event. Defaults to current controller.
    /// </summary>
    public string? Category { get; set; }
        
    /// <summary>
    /// Action for the event. Defaults to current action name.
    /// </summary>
    public string? Action { get; set; }
        
    /// <summary>
    /// Value for the event.
    /// </summary>
    public int Value { get; set; } = 1;
        
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await next();

        if (Enabled)
        {
            var tracker = context.HttpContext.RequestServices.GetRequiredService<AspNetCoreTracker>();
            await tracker.TrackEventAsync(
                Category ?? context.Controller.GetType().Name.Replace("Controller", string.Empty),
                Action ?? (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName ?? context.ActionDescriptor.DisplayName ?? context.ActionDescriptor.Id,
                Value);
        }
            
        context.HttpContext.Items[GoogleAnalyticsTrackerMiddleware.TrackPageViewHandledMarker] = true;
    }
}