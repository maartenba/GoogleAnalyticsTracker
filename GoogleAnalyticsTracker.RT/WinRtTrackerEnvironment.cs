using System;
using System.Net;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker
{
    public class WinRtTrackerEnvironment
        : ITrackerEnvironment
    {
        public WinRtTrackerEnvironment()
        {
            Hostname = "Windows";
            OsPlatform = "Windows RT";
            OsVersion = "8";
            OsVersionString = "RT";
        }

        public string Hostname { get; set; }
        public string OsPlatform { get; set; }
        public string OsVersion { get; set; }
        public string OsVersionString { get; set; }
    }
}