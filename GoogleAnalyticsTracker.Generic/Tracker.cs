using System;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Generic
{
  public class Tracker : TrackerBase
  {
    static Tracker instance;

    public string appName;
    public string appId;
    public string appVersion;
    public string ScreenResolution;
    public string ViewportSize;
    public string ClientId;
    public string UserId;

    public string MergedIds {
      get {
        if (string.IsNullOrEmpty(UserId))
        {
          return ClientId;
        }
        return $"{UserId}:{ClientId}";
      }
    }

    public static Tracker Instance {
      get {
        if (instance == null) {
#if DEBUG
          throw new Exception ("Tracker not configured");
#endif
        }
        return instance;
      }
    }

    public Tracker (string trackingAccount,
                          string trackingDomain,
                          string Hostname,
                          string OsPlatform,
                          string OsVersion,
                          string OsVersionString)
        : base (trackingAccount, trackingDomain, new AnalyticsSession (), new TrackerEnvironment (Hostname, OsPlatform, OsVersion, OsVersionString))
    {
    }

    static public void Configure (string trackingAccount,
                                  string trackingDomain,
                                  string Hostname,
                                  string OsPlatform,
                                  string OsVersion,
                                  string OsVersionString)
    {
      instance = new Tracker (trackingAccount,
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
        ClientId = MergedIds,
        UserId = UserId
      };
      return await TrackAsync (screenviewParamenters);
    }



  }
}