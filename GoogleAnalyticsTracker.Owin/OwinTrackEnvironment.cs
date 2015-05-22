using System;
using System.Net;
using GoogleAnalyticsTracker.Core.Interface;
using Microsoft.Owin;

namespace GoogleAnalyticsTracker.Owin
{
    /// <summary>An owin tracker environment.</summary>
    public class OwinTrackerEnvironment : ITrackerEnvironment
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly IOwinContext _context;

        /// <summary>
        /// Initializes a new instance of the OwinTrackerEnvironment class.
        /// </summary>
        /// <param name="context">The context.</param>
        public OwinTrackerEnvironment(IOwinContext context)
        {
            _context = context;

            Hostname = Dns.GetHostName();
            OsPlatform = Environment.OSVersion.Platform.ToString();
            OsVersion = Environment.OSVersion.Version.ToString();
            OsVersionString = Environment.OSVersion.VersionString;
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