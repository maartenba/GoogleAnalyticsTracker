namespace GoogleAnalyticsTracker.Core
{
    public interface ITrackerEnvironment
    {
        string Hostname { get; set; }
        string OsPlatform { get; set; }
        string OsVersion { get; set; }
        string OsVersionString { get; set; }
    }
}