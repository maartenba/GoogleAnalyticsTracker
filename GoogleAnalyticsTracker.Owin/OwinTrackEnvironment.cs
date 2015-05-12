namespace GoogleAnalyticsTracker.Owin
{
    using System;
    using System.Net;

    using GoogleAnalyticsTracker.Core;

    using Microsoft.Owin;

    /// <summary>An owin tracker environment.</summary>
    public class OwinTrackerEnvironment : ITrackerEnvironment
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     Knowit.UltraReg.WebApi.Middleware.AnalyticsManager.OwinTrackerEnvironment class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OwinTrackerEnvironment(IOwinContext context)
        {
            this.Hostname = Dns.GetHostName();
            this.OsPlatform = Environment.OSVersion.Platform.ToString();
            this.OsVersion = Environment.OSVersion.Version.ToString();
            this.OsVersionString = Environment.OSVersion.VersionString;
        }

        /// <summary>Gets or sets the hostname.</summary>
        /// <value>The hostname.</value>
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