using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_PHONE
using Microsoft.Phone.Info;
#endif

namespace GoogleAnalyticsTracker
{
    public class Tracker
        : IDisposable
    {
        private const string TrackingAccountConfigurationKey = "GoogleAnalyticsTracker.TrackingAccount";
        private const string TrackingDomainConfigurationKey = "GoogleAnalyticsTracker.TrackingDomain";

        const string BeaconUrl = "http://www.google-analytics.com/__utm.gif";
        const string BeaconUrlSsl = "https://ssl.google-analytics.com/_utm.gif";
        const string AnalyticsVersion = "4.3"; // Analytics version - AnalyticsVersion

        private readonly UtmeGenerator _utmeGenerator;
#if WINDOWS_PHONE
        private string screenRes=null;
#endif
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

#if !WINDOWS_PHONE
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
#else
        public Tracker(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, new WindowsPhoneAnalyticsSession())
        {
        }
#endif


        public Tracker(string trackingAccount, string trackingDomain, IAnalyticsSession analyticsSession)
        {
            TrackingAccount = trackingAccount;
            TrackingDomain = trackingDomain;
            AnalyticsSession = analyticsSession;

            Language = CultureInfo.CurrentCulture.ToString();
#if !WINDOWS_PHONE
            UserAgent = string.Format("Tracker/1.0 ({0}; {1}; {2})", Environment.OSVersion.Platform, Environment.OSVersion.Version, Environment.OSVersion.VersionString);
            Hostname = Dns.GetHostName();
#else
            Hostname = "Windows Phone";
            /* This was my first try, reconstructing the same user agent that google analytics for android sdk was sending. It didn't recognized other info that
             * OS Windows Phone.
            // Version ver = new Version(System.Reflection.Assembly.GetExecutingAssembly().FullName.Split(',')[1].Split('=')[1]);
            UserAgent = string.Format("GoogleAnalytics/1.4.2 ({0}; U; {1}; {2}; {3} Build/{4})",
                                      Environment.OSVersion.Platform.ToString(),
                                      osversionstring + " " + Environment.OSVersion.Version.ToString(),
                                      CultureInfo.CurrentCulture.ToString(), DeviceStatus.DeviceManufacturer + " " + DeviceStatus.DeviceName,
                                      Environment.OSVersion.Version.Build);
             */
            
            /* This is my second and working solution: we reconstruct the exact same useragent that IE9 is sending. In that way
             * Google Analytics recognize OS, Manufacturer and device model: wonderful!
             */
            string osver;
            // Windows Phone 7.5, as we usually call Mango, in reality is 7.10... So we change that string.
            if (Environment.OSVersion.Version.Major == 7 && Environment.OSVersion.Version.Minor == 10) osver = "7.5";
            else osver = string.Format("{0}.{1}", Environment.OSVersion.Version.Major,Environment.OSVersion.Version.Minor) ;
            

            UserAgent = string.Format("Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS {0}; Trident/5.0; IEMobile/9.0; {1}; {2})",
                                      osver, Microsoft.Phone.Info.DeviceStatus.DeviceManufacturer, Microsoft.Phone.Info.DeviceStatus.DeviceName);
#endif 
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
#if WINDOWS_PHONE
        private string GenerateUtmsr()
        {
            if (screenRes==null) screenRes= string.Format("{0}x{1}", System.Windows.Application.Current.Host.Content.ActualWidth, System.Windows.Application.Current.Host.Content.ActualHeight);
            return screenRes;
        }
#endif 
        public void SetCustomVariable(int position, string name, string value)
        {
            if (position < 1 || position > 5)
                throw new ArgumentOutOfRangeException(string.Format("position {0} - {1}", position, "Must be between 1 and 5"));

            CustomVariables[position - 1] = new CustomVariable(name, value);
        }

#if WINDOWS_PHONE 
        private void AddWindowsPhoneParameters(Dictionary<string, string> parameters)
        {
            parameters.Add("utmsr", GenerateUtmsr());
            parameters.Add("utmsc", "32-bit");
        }
#endif 

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
#if WINDOWS_PHONE
            AddWindowsPhoneParameters(parameters);
#endif 
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
#if WINDOWS_PHONE
            AddWindowsPhoneParameters(parameters);
#endif 

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
#if WINDOWS_PHONE
            AddWindowsPhoneParameters(parameters);
#endif 

            RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters);
        }

        private void RequestUrlAsync(string url, Dictionary<string, string> parameters)
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

#if !WINDOWS_PHONE
            request.Referer = string.Format("http://{0}/", TrackingDomain);
#endif

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
