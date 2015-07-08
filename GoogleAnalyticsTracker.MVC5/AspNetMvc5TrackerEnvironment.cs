using System;
using System.Net;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.MVC5
{
    public class AspNetMvc5TrackerEnvironment
        : ITrackerEnvironment
    {
        public AspNetMvc5TrackerEnvironment()
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
            if (System.Web.HttpContext.Current == null)
                return false;

            try
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                return System.Web.HttpContext.Current.Request == null;
            }
            catch (System.Web.HttpException ex)
            {
                return false;
            }
        }
    }
}