using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAnalyticsTracker
{
    public partial class Tracker
        : IDisposable
    {
        private const string TrackingAccountConfigurationKey = "GoogleAnalyticsTracker.TrackingAccount";
        private const string TrackingDomainConfigurationKey = "GoogleAnalyticsTracker.TrackingDomain";

        const string BeaconUrl = "http://www.google-analytics.com/__utm.gif";
        const string BeaconUrlSsl = "https://ssl.google-analytics.com/_utm.gif";
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

#if !WINDOWS_PHONE && !NETFX_CORE
        public Tracker()
            : this(new AnalyticsSession())
        {
        }

        public Tracker(IAnalyticsSession analyticsSession)
            : this(ConfigurationManager.AppSettings[TrackingAccountConfigurationKey], ConfigurationManager.AppSettings[TrackingDomainConfigurationKey], analyticsSession)
        {
        }

        public Tracker(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, new AnalyticsSession())
        {
        }
#endif

        public Tracker(string trackingAccount, string trackingDomain, IAnalyticsSession analyticsSession)
        {
            TrackingAccount = trackingAccount;
            TrackingDomain = trackingDomain;
            AnalyticsSession = analyticsSession;

#if !WINDOWS_PHONE && !NETFX_CORE
            string hostname = Dns.GetHostName();
            string osversionstring = Environment.OSVersion.VersionString;
            string osplatform = Environment.OSVersion.Platform.ToString();
            string osversion = Environment.OSVersion.Version.ToString();
#elif WINDOWS_PHONE
            string hostname = "Windows Phone";
            string osversionstring = "Windows Phone";
            string osplatform = "Windows Phone";
            string osversion = Environment.OSVersion.Version.ToString();
#else
            string hostname = "Windows";
            string osversionstring = "RT";
            string osplatform = "Windows RT";
            string osversion = "8";
#endif

            Hostname = hostname;
            Language = "en";
            UserAgent = string.Format("Tracker/1.0 ({0}; {1}; {2})", osplatform, osversion, osversionstring);
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

        public void SetCustomVariable(int position, string name, string value)
        {
            if (position < 1 || position > 5)
                throw new ArgumentOutOfRangeException(string.Format("position {0} - {1}", position, "Must be between 1 and 5"));

            CustomVariables[position - 1] = new CustomVariable(name, value);
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
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmp", pageUrl);
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            var utme = _utmeGenerator.Generate();
            if (!string.IsNullOrEmpty(utme))
                parameters.Add("utme", utme);

            RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters);
        }

        public void TrackEvent(string category, string action, string label, int value)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("AnalyticsVersion", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmni", "1");
            parameters.Add("utmt", "event");

            var utme = _utmeGenerator.Generate();
            parameters.Add("utme", string.Format("5({0}*{1}*{2})({3})", category, action, label ?? "", value) + utme);

            parameters.Add("utmcs", CharacterSet);
            parameters.Add("utmul", Language);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters);
        }

        public void TrackTransaction(string orderId, string storeName, string total, string tax, string shipping, string city, string region, string country)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("AnalyticsVersion", AnalyticsVersion);
            parameters.Add("utmn", GenerateUtmn());
            parameters.Add("utmhn", Hostname);
            parameters.Add("utmt", "event");
            parameters.Add("utmcs", CharacterSet);
            parameters.Add("utmul", Language);
            parameters.Add("utmhid", AnalyticsSession.GenerateSessionId());
            parameters.Add("utmac", TrackingAccount);
            parameters.Add("utmcc", AnalyticsSession.GenerateCookieValue());

            parameters.Add("utmtid", orderId);
            parameters.Add("utmtst", storeName);
            parameters.Add("utmtto", total);
            parameters.Add("utmttx", tax);
            parameters.Add("utmtsp", shipping);
            parameters.Add("utmtci", city);
            parameters.Add("utmtrg", region);
            parameters.Add("utmtco", country);

            RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters);
        }

        private
#if NETFX_CORE
            async
#endif      
            void RequestUrlAsync(string url, Dictionary<string, string> parameters)
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

#if !WINDOWS_PHONE && !NETFX_CORE
            request.Referer = string.Format("http://{0}/", TrackingDomain);
#endif

#if !NETFX_CORE
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
#else
            try 
            {
                var response = await request.GetResponseAsync();
            }
            catch 
            {
                if (ThrowOnErrors)
                {
                    throw;
                }
            }


#endif
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
