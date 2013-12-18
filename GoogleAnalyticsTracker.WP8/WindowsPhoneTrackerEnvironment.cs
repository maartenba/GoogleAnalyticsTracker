using System;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.WP8
{
    public class WindowsPhoneTrackerEnvironment
        : ITrackerEnvironment
    {
        public WindowsPhoneTrackerEnvironment()
        {
            Hostname = "Windows Phone";
            OsPlatform = "Windows Phone";
            OsVersion = Environment.OSVersion.Version.ToString();
            OsVersionString = "Windows Phone";
        }

        public string Hostname { get; set; }
        public string OsPlatform { get; set; }
        public string OsVersion { get; set; }
        public string OsVersionString { get; set; }
    }
}