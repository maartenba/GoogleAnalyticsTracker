using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GoogleAnalyticsTracker
{
    public class Tracker
        : IDisposable
    {
        private const string BeaconUrl = "http://www.google-analytics.com/__utm.gif";
        private const string AnalyticsVersion = "4.3"; // Analytics version - AnalyticsVersion

        private string _sessionId; // Session ID - utmhid
        private string _cookieValue; // Cookie related variables - utmcc

        public string TrackingAccount { get; set; } // utmac
        public string TrackingDomain { get; set; }

        public string Hostname { get; set; }
        public string Language { get; set; }
        public string UserAgent { get; set; }
        public string CharacterSet { get; set; }

        public bool ThrowOnErrors { get; set; }

        public CookieContainer CookieContainer { get; set; }

        public Tracker(string trackingAccount, string trackingDomain)
        {
            TrackingAccount = trackingAccount;
            TrackingDomain = trackingDomain;


            Hostname = Dns.GetHostName();
            Language = "en";
            UserAgent = string.Format("Tracker/1.0 ({0}; {1}; {2})", Environment.OSVersion.Platform, Environment.OSVersion.Version, Environment.OSVersion.VersionString);
            CookieContainer = new CookieContainer();

            ThrowOnErrors = false;

            InitializeUtmHid();
            InitializeCharset();
            InitializeCookieVariable();
        }

        private void InitializeUtmHid()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            _sessionId = random.Next(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        }

        private void InitializeCharset()
        {
            CharacterSet = "UTF-8";
        }

        private void InitializeCookieVariable()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            var cookie = string.Format("{0}{1}", random.Next(100000000, 999999999), "00145214523");

            var randomvalue = random.Next(1000000000, 2147483647).ToString(CultureInfo.InvariantCulture);

            _cookieValue = string.Format("__utma=1.{0}.{1}.{2}.{2}.15;+__utmz=1.{2}.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none);", cookie, randomvalue, DateTime.UtcNow.Ticks);
        }

        private string GenerateUtmn()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            return random.Next(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        }

        public void TrackPageView(string pageTitle, string pageUrl)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("AnalyticsVersion", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmcs", CharacterSet);
            parameters.Add("utmul", Language);
            parameters.Add("utmdt", pageTitle);
            parameters.Add("utmhid", _sessionId);
            parameters.Add("utmp", pageUrl);
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", _cookieValue);

            RequestUrlAsync(BeaconUrl, parameters);
        }

        private void RequestUrlAsync(string url, Dictionary<string, string> parameters)
        {
            // Create GET string
            StringBuilder data = new StringBuilder();
            foreach (var parameter in parameters)
            {
                data.Append(string.Format("{0}={1}&", parameter.Key, HttpUtility.UrlEncode(parameter.Value)));
            }

            // Create request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("{0}?{1}", url, data));
            request.CookieContainer = CookieContainer;
            request.Referer = string.Format("http://{0}/", TrackingDomain);
            request.UserAgent = UserAgent;

            Task.Factory.FromAsync(request.BeginGetResponse, result => request.EndGetResponse(result), null)
                .ContinueWith(task =>
                                  {
                                      try
                                      {
                                          task.Result.Close();
                                          if (task.IsFaulted && task.Exception != null && ThrowOnErrors)
                                          {
                                              throw task.Exception;
                                          }
                                      }
                                      catch
                                      {
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

            if (disposing)
            {
                //TODO: Managed cleanup code here, while managed refs still valid
            }
            //TODO: Unmanaged cleanup code here

            disposed = true;
        }

        public void Dispose()
        {
            // Call the private Dispose(bool) helper and indicate 
            // that we are explicitly disposing
            this.Dispose(true);

            // Tell the garbage collector that the object doesn't require any
            // cleanup when collected since Dispose was called explicitly.
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}