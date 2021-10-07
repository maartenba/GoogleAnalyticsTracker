using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.Interface;

[PublicAPI]
public interface ITrackerEnvironment
{
    string OsPlatform { get; set; }
    string OsVersion { get; set; }
    string OsVersionString { get; set; }
}