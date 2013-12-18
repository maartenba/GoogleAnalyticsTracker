using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker.Core
{
    public partial class TrackerBase
        : IDisposable, ITrackEvents, ITrackPageViews, ITrackTransactions, ITrackTransactionItems
    {
        public const string TrackingAccountConfigurationKey = "GoogleAnalyticsTracker.TrackingAccount";
        public const string TrackingDomainConfigurationKey = "GoogleAnalyticsTracker.TrackingDomain";

        const string BeaconUrl = "http://www.google-analytics.com/__utm.gif";
        const string BeaconUrlSsl = "https://ssl.google-analytics.com/__utm.gif";
        const string AnalyticsVersion = "4.3"; // Analytics version - AnalyticsVersion

        private readonly UtmeGenerator _utmeGenerator;

        public string TrackingAccount { get; set; } // utmac
        public string TrackingDomain { get; set; }
        public IAnalyticsSession AnalyticsSession { get; set; }

        public string Hostname { get; set; }
        public string Language { get; set; }
        public string UserAgent { get; set; }
        public string CharacterSet { get; set; }

        internal CustomVariable[] CustomVariables { get; set; }

        public bool ThrowOnErrors { get; set; }

        public CookieContainer CookieContainer { get; set; }

        public bool UseSsl { get; set; }

        public TrackerBase(string trackingAccount, string trackingDomain, ITrackerEnvironment trackerEnvironment)
            : this(trackingAccount, trackingDomain, new AnalyticsSession(), trackerEnvironment)
        {
        }

        public TrackerBase(string trackingAccount, string trackingDomain, IAnalyticsSession analyticsSession, ITrackerEnvironment trackerEnvironment)
        {
            TrackingAccount = trackingAccount;
            TrackingDomain = trackingDomain;
            AnalyticsSession = analyticsSession;

            Hostname = trackerEnvironment.Hostname;
            Language = "en";
            UserAgent = string.Format("Tracker/1.0 ({0}; {1}; {2})", trackerEnvironment.OsPlatform, trackerEnvironment.OsVersion, trackerEnvironment.OsVersionString);

            CookieContainer = new CookieContainer();

            ThrowOnErrors = false;

            InitializeCharset();

            CustomVariables = new CustomVariable[5];

            _utmeGenerator = new UtmeGenerator(this);
        }

        private void InitializeCharset()
        {
            CharacterSet = "UTF-8";
        }

        private string GenerateUtmn()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            return random.Next(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        }

        public void SetCustomVariable(int position, string name, string value, int scope=3)
        {
            if (position < 1 || position > 5)
                throw new ArgumentOutOfRangeException(string.Format("position {0} - {1}", position, "Must be between 1 and 5"));
            if (scope < 1 || scope > 3)
                throw new ArgumentOutOfRangeException(string.Format("scope {0} - {1}", scope, "Must be between 1 and 3"));

            CustomVariables[position - 1] = new CustomVariable(name, value, scope);
        }

        private  Task<TrackingResult> RequestUrlAsync(string url, Dictionary<string, string> parameters, string userAgent = null)
        {
            // Create GET string
            StringBuilder data = new StringBuilder();
            foreach (var parameter in parameters)
            {
                data.Append(string.Format("{0}={1}&", parameter.Key, Uri.EscapeDataString(parameter.Value)));
            }

            // Create request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}?{1}", url, data));
            request.CookieContainer = CookieContainer;
            request.Headers["Referer"] = string.Format("http://{0}/", TrackingDomain);
            request.Headers["User-Agent"] = userAgent ?? UserAgent;

            return Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null)
                .ContinueWith(task =>
                {
                    try
                    {
                        var returnValue = new TrackingResult
                        {
                            Url = url,
                            Parameters = parameters,
                            Success = true
                        };
                        if (task.IsFaulted && task.Exception != null && ThrowOnErrors)
                        {
                            throw task.Exception;
                        }
                        else if (task.IsFaulted)
                        {
                            returnValue.Success = false;
                            returnValue.Exception = task.Exception;
                        }
                        return returnValue;
                    }
                    finally
                    {
                        if (!task.IsFaulted && task.Result != null)
                        {
                            var disposableResult = task.Result as IDisposable;
                            if (disposableResult != null)
                            {
                                disposableResult.Dispose();
                            }
                        }
                    }
                });
        }

        #region IDisposable Members

        private bool disposed;

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
