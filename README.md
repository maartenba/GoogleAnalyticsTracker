[![googleanalyticstracker MyGet Build Status](https://www.myget.org/BuildSource/Badge/googleanalyticstracker?identifier=9b038848-c290-4123-bc35-c8cd67b40052)](https://www.myget.org/gallery/googleanalyticstracker)

# GoogleAnalyticsTracker
GoogleAnalyticsTracker - A C# library for tracking Google Analytics.

## Like this project?
[<img src="https://www.paypal.com/en_US/i/btn/btn_donate_SM.gif">](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=C8GLSG8E33NA4) via [PayPal](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=C8GLSG8E33NA4).

## What can it be used for?
GoogleAnalyticsTracker was created to have a means of tracking specific URLs directly from C#. For example, when creating an API using the ASP.NET MVC framework, GoogleAnalyticsTracker enables you to track usage of the API by calling directly into Google Analytics.

Note that for GoogleAnalyticsTracker to work, you should configure Google Analytics as a website. This library will not work when the Google Analytics account is configured as an app.

## Get it on NuGet!

Depending on the type of application you are using, use any of the following NuGet packages:

Windows applications (WinForms, WPF, Console):

    Install-Package GoogleAnalyticsTracker.Simple

ASP.NET MVC 4 applications:

    Install-Package GoogleAnalyticsTracker.MVC4

ASP.NET MVC 5 applications:

    Install-Package GoogleAnalyticsTracker.MVC5

ASP.NET Web API v1 applications:

    Install-Package GoogleAnalyticsTracker.WebAPI

ASP.NET Web API v2 applications:

    Install-Package GoogleAnalyticsTracker.WebAPI2

OWIN applications:

    Install-Package GoogleAnalyticsTracker.Owin

Nancy applications:

    Install-Package GoogleAnalyticsTracker.Nancy

## Example usage

Using GoogleAnalyticsTracker is very straightforward. In your code, add the following structure wherever you want to track page views (note: when using `GoogleAnalyticsTracker.Simple`, the class to use is `SimpleTracker`):
```csharp
using (Tracker tracker = new Tracker("UA-XXXXXX-XX", simpleTrackerEnvironment))
{
    await tracker.TrackPageViewAsync("My API - Create", "api/create");
    await tracker.TrackPageViewAsync("MY API - List", "api/list");
}
```
Or without a using block:
```csharp
Tracker tracker = new Tracker("UA-XXXXXX-XX", simpleTrackerEnvironment);
await tracker.TrackPageViewAsync("My API - Create", "api/create");
```
A number of extension methods are available which use the provided `HttpContext` as the source for URL and user properties:
```csharp
Tracker tracker = new Tracker("UA-XXXXXX-XX", simpleTrackerEnvironment);
await tracker.TrackPageViewAsync(HttpContext, "My API - Create");
```
Finally, an `ActionFilter` for use with ASP.NET MVC is available:
```csharp
[ActionTracking("UA-XXXXXX-XX")]
public class ApiController  : Controller
{
    public JsonResult Create()
    {
        return Json(true);
    }
}
```
This filter can also be applied as a global action filter, optionally filtering the requests to log:
```csharp
public class MvcApplication : System.Web.HttpApplication
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new HandleErrorAttribute());
        filters.Add(new ActionTrackingAttribute(
            "UA-XXXXXX-XX",
            action => action.ControllerContext.ControllerDescriptor.ControllerName == "Api")
        );
    }
}
```	

An hook is also available for NancyFx:

```csharp
public class HelloModule : NancyModule
{
    private readonly ActionTrackingHookAsync actionTrackingHook = new ActionTrackingHookAsync("UA-XXXXXX-XX");

    public HelloModule()
    {
        Before += async (ctx, ct) =>
        {
            await actionTrackingHook.OnActionExecutingAsync(ctx, ct);
            return null;
        };

        Get["/ping"] = _ => Response.AsJson(new { Value = "Pong" });
        Get["Index","/"] = _ => "Hello World";
    }
}
```

## Characteristics
GoogleAnalyticsTracker does not track your users. It simply serves as an interface to Google Analytics where you should provide all tracking data that is required.
Of course. GoogleAnalyticsTracker sends some data that can be inferred from usage, such as the hostname on which it is running, but not the hostname of your client.
Sessions are also untracked: every event that is tracked counts as a new unique visitor to Google Analytics.

* If you do need to track user sessions, implement a custom `IAnalyticsSession` and pass it to the constructor of the `Tracker` object.
* If you do need to set common data for all tracking hits, subclass the `Tracker` or `TrackerBase` and override the `AmendParameters` method.

## License
[MS-PL License](https://github.com/maartenba/GoogleAnalyticsTracker/blob/master/LICENSE.md)

## Building the source
After cloning the repository, run `build.cmd`. A folder named "Build" will be created and populated with:

- Assemblies
- PDB files
- NuGet package

## Who uses GoogleAnalyticsTracker?
- [MyGet](http://www.myget.org)
