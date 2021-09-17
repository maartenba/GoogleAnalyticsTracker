[![googleanalyticstracker MyGet Build Status](https://www.myget.org/BuildSource/Badge/googleanalyticstracker?identifier=3e8e456d-0e4d-4e35-8112-1363461dfc6b)](https://www.myget.org/)

# GoogleAnalyticsTracker

GoogleAnalyticsTracker - A C# library for tracking Google Analytics.

## What can it be used for?

GoogleAnalyticsTracker was created to have a means of tracking specific URLs directly from C#. For example, when creating an API using the ASP.NET MVC framework, GoogleAnalyticsTracker enables you to track usage of the API by calling directly into Google Analytics.

Note that for GoogleAnalyticsTracker to work, you should configure Google Analytics as a website. This library will not work when the Google Analytics account is configured as an app.

## Get it on NuGet!

Depending on the type of application you are using, use any of the following NuGet packages:

Any application type where framework integration is not needed (Console, WPF, WinForms, ...):

    GoogleAnalyticsTracker.Simple

ASP.NET Core:

    GoogleAnalyticsTracker.AspNetCore

## Example usage - GoogleAnalyticsTracker.Simple

Using GoogleAnalyticsTracker is very straightforward. In your code, add the following structure wherever you want to track page views (note: when using `GoogleAnalyticsTracker.Simple`, the class to use is `SimpleTracker`):

```csharp
using (var tracker = new SimpleTracker("UA-XXXXXX-XX", simpleTrackerEnvironment))
{
    await tracker.TrackPageViewAsync("My API - Create", "api/create");
    await tracker.TrackPageViewAsync("MY API - List", "api/list");
}
```

Or without a using block:

```csharp
var tracker = new SimpleTracker("UA-XXXXXX-XX", simpleTrackerEnvironment);
await tracker.TrackPageViewAsync("My API - Create", "api/create");
```

## Example usage - GoogleAnalyticsTracker.AspNetCore

Install the `GoogleAnalyticsTracker.AspNetCore` package, and register GoogleAnalyticsTracker in your startup class:


```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // ...

        services.AddGoogleAnalyticsTracker(options =>
        {
            options.TrackerId = "UA-XXXXXX-XX";
            options.ShouldTrackRequestInMiddleware = TrackRequests.Yes;
        });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseGoogleAnalyticsTracker();
        
        // ...
    }
}
```

This will automatically track all requests made to your application.

Ideally, you will want to control tracking requests. To do so:

* Set the `options.ShouldTrackRequestInMiddleware = TrackRequests.No;` option to disable automatic tracking.
* Add the `[TrackPageView]` or `[TrackEvent]` attributes to your actions.

`[TrackPageView]` will track a page view in Google Analytics, whereas `[TrackEvent]` tracks an event.

In case custom tracking is needed, inject an `AspNetCoreTracker` into your controller and use the tracking methods directly.

Note that the options also provide a couple of events that can be overridden to customize GoogleAnalyticsTracker behaviour:

* `CustomizeTrackerEnvironment` lets you customize the tracker environment or inject a custom one.
* `CustomizeAnalyticsSession` lets you customize the analytics session or inject a custom one.

## Characteristics

GoogleAnalyticsTracker does not track your users. It simply serves as an interface to Google Analytics where you should provide all tracking data that is required.
Of course. GoogleAnalyticsTracker sends some data that can be inferred from usage, such as the hostname on which it is running, but not the hostname of your client.
Sessions are also untracked: every event that is tracked counts as a new unique visitor to Google Analytics.

* If you do need to track user sessions, implement a custom `IAnalyticsSession` and pass it to the constructor of the tracker.
* If you do need to set common data for all tracking hits, subclass the `Tracker` or `TrackerBase` and override the `AmendParameters` method.

## License

[MS-PL License](https://github.com/maartenba/GoogleAnalyticsTracker/blob/master/LICENSE.md)

## Who uses GoogleAnalyticsTracker?

* [MyGet](https://www.myget.org/)
* [JetBrains](https://www.jetbrains.com/)
