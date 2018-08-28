using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.Interface;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using System.Linq;
using System.Net.Http;

namespace GoogleAnalyticsTracker.Core
{
    public partial class TrackerBase : IDisposable
    {
        public const string TrackingAccountConfigurationKey = "GoogleAnalyticsTracker.TrackingAccount";

        public string TrackingAccount { get; set; }
        public IAnalyticsSession AnalyticsSession { get; set; }

        public string UserAgent { get; set; }

        public bool ThrowOnErrors { get; set; }
        public string EndpointUrl { get; set; }

        public string ProxyUrl { get; set; }
        public int ProxyPort { get; set; }
        
        /// <summary> 
        /// Use HTTP GET (not recommended) instead of POST.
        /// When switched on, the <see cref="AmendParameters(TrackerParameters.Interface.IGeneralParameters)"/>
        /// sets the <see cref="TrackerParameters.Interface.IGeneralParameters.CacheBuster"/>, too.
        /// </summary>
        public bool UseHttpGet { get; set; }

        private HttpClient _httpClient
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ProxyUrl))
                {
                    return new HttpClient();
                }
                else
                {
                    var proxy = new WebProxy(ProxyUrl, ProxyPort);

                    var httpClientHandler = new HttpClientHandler
                    {
                        Proxy = proxy,
                    };

                    return new HttpClient(httpClientHandler, true);
                }
            }
        }

        public TrackerBase(string trackingAccount, ITrackerEnvironment trackerEnvironment)
            : this(trackingAccount, new AnalyticsSession(), trackerEnvironment)
        {
        }

        public TrackerBase(string trackingAccount, IAnalyticsSession analyticsSession, ITrackerEnvironment trackerEnvironment)
        {
            TrackingAccount = trackingAccount;
            AnalyticsSession = analyticsSession;

            EndpointUrl = GoogleAnalyticsEndpoints.Default;
            UserAgent = string.Format("GoogleAnalyticsTracker/3.0 ({0}; {1}; {2})", trackerEnvironment.OsPlatform, trackerEnvironment.OsVersion, trackerEnvironment.OsVersionString);
        }

        private async Task<TrackingResult> RequestUrlAsync(string url, IDictionary<string, string> parameters, string userAgent)
        {
            // Create GET string
            var data = new StringBuilder();
            foreach (var parameter in parameters.OrderBy(p => p.Key, new BeaconComparer()))
            {
                data.Append(string.Format("{0}={1}&", parameter.Key, Uri.EscapeDataString(parameter.Value)));
            }

            // Build TrackingResult
            var returnValue = new TrackingResult
            {
                Url = url,
                Parameters = parameters
            };

            // Create request
            HttpRequestMessage request;
            try
            {
                request = UseHttpGet
                    ? CreateGetWebRequest(url, data.ToString())
                    : CreatePostWebRequest(url, data.ToString());

                if (!string.IsNullOrEmpty(userAgent))
                {

                    request.Headers.Add("User-Agent", userAgent);
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
            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.SendAsync(request);
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

        private HttpRequestMessage CreateGetWebRequest(string url, string data)
        {
            return new HttpRequestMessage(HttpMethod.Get, string.Format("{0}?{1}", url, data));
        }

        private HttpRequestMessage CreatePostWebRequest(string url, string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var dataBytes = Encoding.UTF8.GetBytes(data);
            request.Content = new ByteArrayContent(dataBytes);

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
