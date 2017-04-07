using System;
using System.Net;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.Generic
{
  public class TrackerEnvironment : ITrackerEnvironment
  {
    public TrackerEnvironment (string Hostname,string OsPlatform, string OsVersion, string OsVersionString)
    {
      this.Hostname = Hostname;//Dns.GetHostName();
      this.OsPlatform = OsPlatform;//Environment.OSVersion.Platform.ToString ();
      this.OsVersion = OsVersion;//Environment.OSVersion.Version.ToString ();
      this.OsVersionString = OsVersionString;//Environment.OSVersion.VersionString;
    }

    public string Hostname { get; set; }
    public string OsPlatform { get; set; }
    public string OsVersion { get; set; }
    public string OsVersionString { get; set; }
  }
}