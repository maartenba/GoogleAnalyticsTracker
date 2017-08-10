using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.Interface;

namespace GoogleAnalyticsTracker.Core
{
    public partial class TrackerBase : IDisposable
    {
        public const string TrackingAccountConfigurationKey = "GoogleAnalyticsTracker.TrackingAccount";
        public const string TrackingDomainConfigurationKey = "GoogleAnalyticsTracker.TrackingDomain";

        public string TrackingAccount { get; set; }
        public string TrackingDomain { get; set; }
        public IAnalyticsSession AnalyticsSession { get; set; }

        public string Hostname { get; set; }
        public string Language { get; set; }
        public string UserAgent { get; set; }
        public string CharacterSet { get; set; }        

        public bool ThrowOnErrors { get; set; }        
        public string EndpointUrl { get; set; }

        /// <summary> Use HTTP GET (not recommended) instead of POST.</summary>
        public bool UseHttpGet { get; set; }

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
            EndpointUrl = GoogleAnalyticsEndpoints.Default;
            UserAgent = string.Format("GoogleAnalyticsTracker/3.0 ({0}; {1}; {2})", trackerEnvironment.OsPlatform, trackerEnvironment.OsVersion, trackerEnvironment.OsVersionString);

            InitializeCharset();                  
        }

        private void InitializeCharset()
        {
            CharacterSet = "UTF-8";
        }

        private async Task<TrackingResult> RequestUrlAsync(string url, IDictionary<string, string> parameters, string userAgent)
        {
            // Create GET string
            var data = new StringBuilder();
            foreach (var parameter in parameters)
            {
                data.Append(string.Format("{0}={1}&", parameter.Key, Uri.EscapeDataString(parameter.Value)));
            }

            // Build TrackingResult
            var returnValue = new TrackingResult
            {
                Url = url,
                Parameters = parameters
            };

            // Determine referer URL
            string referer = null;
            if (parameters.ContainsKey("ReferralUrl"))
            {
                referer = parameters["ReferralUrl"];
            }
            else if (!string.IsNullOrEmpty(TrackingDomain))
            {
                referer = string.Format("http://{0}/", TrackingDomain);
            }

            // Create request
            HttpWebRequest request;
            try
            {
                if (UseHttpGet)
                {
                    request = CreateGetWebRequest(url, data.ToString());
                }
                else
                {
                    request = CreatePostWebRequest(url, data.ToString());
                }

                if (!string.IsNullOrEmpty(referer))
                {
                    request.SetHeader("Referer", referer);
                }
                if (!string.IsNullOrEmpty(userAgent))
                {
                    request.SetHeader("User-Agent", userAgent);
                }
            }
            catch (Exception ex)
            {
                if (ThrowOnErrors)
                {
                    throw;
                }

                returnValue.Success = false;
                returnValue.Exception = ex;

                return returnValue;
            }

            // Perform request
            WebResponse response = null;
            try
            {
                response = await Task.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null);
                returnValue.Success = true;
            }
            catch (Exception ex)
            {
                if (ThrowOnErrors)
                {
                    throw;
                }
                else
                {
                    returnValue.Success = false;
                    returnValue.Exception = ex;
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
            }

            return returnValue;
        }

        private HttpWebRequest CreateGetWebRequest(string url, string data)
        {
            return (HttpWebRequest)WebRequest.CreateHttp(string.Format("{0}?{1}", url, data));
        }

        private HttpWebRequest CreatePostWebRequest(string url, string data)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (var stream = request.GetRequestStreamAsync().Result)
            {
                stream.Write(dataBytes, 0, dataBytes.Length);
                stream.Flush();
                stream.Dispose();
            }
            return request;
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
