using System;
using GoogleAnalyticsTracker.AspNet;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    [PublicAPI]
    public static class GoogleAnalyticsTrackerExtensions
    {
        public static IServiceCollection AddGoogleAnalyticsTracker(
            this IServiceCollection services, Action<GoogleAnalyticsTrackerOptions> configureOptions)
        {
            services.AddOptions();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.TryAddScoped<GoogleAnalyticsTrackerMiddleware>();
            services.TryAddScoped<AspNetCoreTracker>();
            
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }
            
            return services;
        }
        
        public static IApplicationBuilder UseGoogleAnalyticsTracker(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GoogleAnalyticsTrackerMiddleware>();
        }
    }
}