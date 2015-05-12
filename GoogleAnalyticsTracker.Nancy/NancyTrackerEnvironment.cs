using GoogleAnalyticsTracker.Core;
using System;
using System.Net;

namespace GoogleAnalyticsTracker.Nancy
{
    class NancyTrackerEnvironment: ITrackerEnvironment
    {
        public NancyTrackerEnvironment()
        {
            Hostname = Dns.GetHostName();
            OsPlatform = Environment.OSVersion.Platform.ToString();
            OsVersion = Environment.OSVersion.Version.ToString();
            OsVersionString = Environment.OSVersion.VersionString;
        }

        public string Hostname { get; set; }
        public string OsPlatform { get; set; }
        public string OsVersion { get; set; }
        public string OsVersionString { get; set; }
    }
}
