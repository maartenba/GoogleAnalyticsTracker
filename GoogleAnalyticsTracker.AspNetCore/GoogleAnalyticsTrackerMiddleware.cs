using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace GoogleAnalyticsTracker.AspNet;

[PublicAPI]
public class GoogleAnalyticsTrackerMiddleware
{
    public const string TrackPageViewHandledMarker = nameof(GoogleAnalyticsTrackerMiddleware) + "." + nameof(TrackPageViewHandledMarker);
        
    private readonly RequestDelegate _next;

    public GoogleAnalyticsTrackerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context, 
        AspNetCoreTracker tracker, 
        IOptions<GoogleAnalyticsTrackerOptions> optionsAccessor)
    {
        await _next(context);

        if (optionsAccessor.Value.ShouldTrackRequestInMiddleware == null || 
            optionsAccessor.Value.ShouldTrackRequestInMiddleware(context))
        {
            await tracker.TrackPageViewAsync();
            context.Items[TrackPageViewHandledMarker] = true;
        }
    }
}