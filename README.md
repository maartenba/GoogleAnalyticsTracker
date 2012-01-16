# GoogleAnalyticsTracker
GoogleAnalyticsTracker - A C# library for tracking Google Analytics

## Like this project?
[<img src="https://www.paypal.com/en_US/i/btn/btn_donate_SM.gif">](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=C8GLSG8E33NA4) via [PayPal](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=C8GLSG8E33NA4).

## What can it be used for?
GoogleAnalyticsTracker was created to have a means of tracking specific URL's directly from C#. For example, when creating an API using the ASP.NET MVC framework, GoogleAnalyticsTracker enables you to track usage of the API by calling directly into Google Analytics.

## Get it on NuGet!

    Install-Package GoogleAnalyticsTracker
	
## Example usage
Using GoogleAnalyticsTracker is very straightforward. In your code, add the following structure wherever you want to track page views:

    using (GoogleAnalyticsTracker tracker = new GoogleAnalyticsTracker("UA-XXXXXX-XX", "www.example.org"))
    {
        tracker.TrackPageView("My API - Create", "api/create");
        tracker.TrackPageView("MY API - List", "api/list");
    }

Or without a using block:

    GoogleAnalyticsTracker tracker = new GoogleAnalyticsTracker("UA-XXXXXX-XX", "www.example.org");
    tracker.TrackPageView("My API - Create", "api/create");

## License
[MS-PL License](https://github.com/maartenba/GoogleAnalyticsTracker/blob/master/LICENSE.md)

## Building the source
After cloning the repository, run build.cmd. A folder named "Build" will be created and populated with:

- Assemblies
- PDB files
- NuGet package

## Who uses GoogleAnalyticsTracker?
- [MyGet](http://www.myget.org)