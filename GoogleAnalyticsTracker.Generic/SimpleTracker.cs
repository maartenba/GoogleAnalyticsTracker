using System;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Simple
{
  public class SimpleTracker : TrackerBase
  {
    static SimpleTracker instance;
    public GeneralParameters Parameters;


    public string appName;
    public string appId;
    public string appVersion;
    public string ScreenResolution;
    public string ViewportSize;
    public string ClientId;


    public static SimpleTracker Instance {
      get {
        if (instance == null) {
#if DEBUG
          throw new Exception ("Tracker not configured");
#endif
        }
        return instance;
      }
    }

    public SimpleTracker (string trackingAccount,
                          string trackingDomain,
                          string Hostname,
                          string OsPlatform,
                          string OsVersion,
                          string OsVersionString)
        : base (trackingAccount, trackingDomain, new AnalyticsSession (), new SimpleTrackerEnvironment (Hostname, OsPlatform, OsVersion, OsVersionString))
    {
    }

    static public void Configure (string trackingAccount,
                                  string trackingDomain,
                                  string Hostname,
                                  string OsPlatform,
                                  string OsVersion,
                                  string OsVersionString)
    {
      instance = new SimpleTracker (trackingAccount,
                                    trackingDomain,
                                    Hostname,
                                    OsPlatform,
                                    OsVersion,
                                    OsVersionString);
    }


    public async Task<TrackingResult> TrackScreenviewAsync (string screenName)
    {
      var screenviewParamenters = new ScreenviewTracking {
        ApplicationName = appName,
        ApplicationId = appId,
        ApplicationVersion = appVersion,
        ScreenName = screenName,
        CacheBuster = AnalyticsSession.GenerateCacheBuster (),
        ViewportSize = ViewportSize,
        ScreenResolution = ScreenResolution,
        ClientId = ClientId
      };
      return await TrackAsync (screenviewParamenters);
    }



  }
}