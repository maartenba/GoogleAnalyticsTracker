using System;
using System.Net;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.Mvc4
{
    public class AspNetMvc4TrackerEnvironment
        : ITrackerEnvironment
    {
        public AspNetMvc4TrackerEnvironment()
        {
            Hostname = Dns.GetHostName();
            OsPlatform = Environment.OSVersion.Platform.ToString();
            OsVersion = Environment.OSVersion.Version.ToString();
            OsVersionString = Environment.OSVersion.VersionString;

            if (IsHttpRequestAvailable())
            {
                Hostname = System.Web.HttpContext.Current.Request.Url.Host;
            }
        }

        public string Hostname { get; set; }
        public string OsPlatform { get; set; }
        public string OsVersion { get; set; }
        public string OsVersionString { get; set; }

        protected bool IsHttpRequestAvailable()
        {
            return System.Web.HttpContext.Current != null;
        }
    }
}