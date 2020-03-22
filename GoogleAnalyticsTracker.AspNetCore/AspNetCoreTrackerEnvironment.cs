using System;
using System.Net;
using GoogleAnalyticsTracker.Core.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.AspNet
{
    /// <summary>An ASP.NET Core tracker environment.</summary>
    [PublicAPI]
    public class AspNetCoreTrackerEnvironment : ITrackerEnvironment
    {
        /// <summary>
        /// Initializes a new instance of the AspNetTrackerEnvironment class.
        /// </summary>
        public AspNetCoreTrackerEnvironment()
        {
            Hostname = Dns.GetHostName();
            OsPlatform =  Environment.OSVersion.Platform.ToString();
            OsVersion = Environment.OSVersion.Version.ToString();
            OsVersionString = Environment.OSVersion.VersionString;
        }

        /// <summary>Gets or sets the hostname.</summary>
        /// <value>The hostname.</value>
        // ReSharper disable once MemberCanBePrivate.Global
        public string Hostname { get; set; }

        /// <summary>Gets or sets the operating system platform.</summary>
        /// <value>The operating system platform.</value>
        public string OsPlatform { get; set; }

        /// <summary>Gets or sets the operating system version.</summary>
        /// <value>The operating system version.</value>
        public string OsVersion { get; set; }

        /// <summary>Gets or sets the operating system version string.</summary>
        /// <value>The operating system version string.</value>
        public string OsVersionString { get; set; }
    }
}