using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.Interface;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using System.Linq;

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

        /// <summary> 
        /// Use HTTP GET (not recommended) instead of POST.
        /// When switched on, the <see cref="AmendParameters(TrackerParameters.Interface.IGeneralParameters)"/>
        /// sets the <see cref="TrackerParameters.Interface.IGeneralParameters.CacheBuster"/>, too.
        /// </summary>
        public bool UseHttpGet { get; set; }

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
            HttpWebRequest request;
            try
            {
                request = UseHttpGet
                    ? CreateGetWebRequest(url, data.ToString())
                    : await CreatePostWebRequestAsync(url, data.ToString());

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
            return WebRequest.CreateHttp(string.Format("{0}?{1}", url, data));
        }

        private async Task<HttpWebRequest> CreatePostWebRequestAsync(string url, string data)
        {
            var request = WebRequest.CreateHttp(url);
            request.Method = "POST";
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (var stream = await request.GetRequestStreamAsync())
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
