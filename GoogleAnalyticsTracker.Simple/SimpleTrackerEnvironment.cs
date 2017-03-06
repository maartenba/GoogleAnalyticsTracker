using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.Simple
{
    public class SimpleTrackerEnvironment : ITrackerEnvironment
    {
        public SimpleTrackerEnvironment(string Hostname, string OsPlatform, string OsVersion, string OsVersionString)
        {
            this.Hostname = Hostname;
            this.OsPlatform = OsPlatform;
            this.OsVersion = OsVersion;
            this.OsVersionString = OsVersionString;
        }

        public string Hostname { get; set; }
        public string OsPlatform { get; set; }
        public string OsVersion { get; set; }
        public string OsVersionString { get; set; }
    }
}