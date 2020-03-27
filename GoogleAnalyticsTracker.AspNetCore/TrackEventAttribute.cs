using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace GoogleAnalyticsTracker.AspNet
{
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
        [CanBeNull]
        public string Category { get; set; }
        
        /// <summary>
        /// Action for the event. Defaults to current action name.
        /// </summary>
        [CanBeNull]
        public string Action { get; set; }
        
        /// <summary>
        /// Value for the event.
        /// </summary>
        public int Value { get; set; } = 1;
        
        /// <summary>
        /// Description for this action.
        /// </summary>
        public string ActionDescription { get; set; }
        
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            if (Enabled)
            {
                var tracker = context.HttpContext.RequestServices.GetRequiredService<AspNetCoreTracker>();
                await tracker.TrackEventAsync(
                    Category ?? context.Controller.GetType().Name.Replace("Controller", string.Empty),
                    Action ?? (context.ActionDescriptor as ControllerActionDescriptor)?.ActionName ?? context.ActionDescriptor.DisplayName,
                    Value);
            }
            
            context.HttpContext.Items[GoogleAnalyticsTrackerMiddleware.TrackPageViewHandledMarker] = true;
        }
    }
}