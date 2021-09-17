using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.Interface;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core
{
    [PublicAPI]
    public class TrackerBase : IDisposable
    {
        private static readonly HttpClient DefaultHttpClient = new();

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

        private HttpClient? _customHttpClient;

        /// <summary>
        /// Makes it possible to set a custom HTTP client. If not set, the internal, static default one will be used.
        /// </summary>
        public HttpClient HttpClient
        {
            get => _customHttpClient ?? DefaultHttpClient;
            set => _customHttpClient = value;
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
            
            // ReSharper disable once UseStringInterpolation
            UserAgent = string.Format("GoogleAnalyticsTracker/7.0 ({0}; {1}; {2})", trackerEnvironment.OsPlatform, trackerEnvironment.OsVersion, trackerEnvironment.OsVersionString);
        }

        private async Task<TrackingResult> RequestUrlAsync(string url, IDictionary<string, string> parameters, string userAgent)
        {
            // Create GET string
            var data = string.Join("&", parameters
                .OrderBy(p => p.Key, new BeaconComparer())
                // ReSharper disable once UseStringInterpolation
                .Select(p => string.Format("{0}={1}", p.Key, Uri.EscapeDataString(p.Value)))
            );

            // Build TrackingResult
            var returnValue = new TrackingResult(url, parameters, data);

            // Create request
            HttpRequestMessage request;
            try
            {
                request = UseHttpGet
                    ? CreateGetWebRequest(url, data)
                    : CreatePostWebRequest(url, data);

                if (!string.IsNullOrEmpty(userAgent))
                {

                    request.Headers.TryAddWithoutValidation("User-Agent", userAgent);
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
            HttpResponseMessage? response = null;
            try
            {
                response = await HttpClient.SendAsync(request);
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
                response?.Dispose();
            }

            return returnValue;
        }

        private static HttpRequestMessage CreateGetWebRequest(string url, string data)
            => new(HttpMethod.Get, $"{url}?{data}");

        private static HttpRequestMessage CreatePostWebRequest(string url, string data)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            var dataBytes = Encoding.UTF8.GetBytes(data);
            request.Content = new ByteArrayContent(dataBytes);

            return request;
        }
        
        private static IDictionary<string, string> GetParametersDictionary(IProvideBeaconParameters parameters)
        {
            var beaconList = new BeaconList<string, string>();

            foreach (var p in parameters.GetType().GetRuntimeProperties())
            {
                if (p.GetCustomAttribute(typeof(BeaconAttribute), true) is not BeaconAttribute attr)
                {
                    continue;
                }

                object? value;
                var underlyingType = Nullable.GetUnderlyingType(p.PropertyType);

                if ((p.PropertyType.GetTypeInfo().IsEnum || p.PropertyType.IsNullableEnum()) && attr.IsEnumByValueBased)
                {
                    value = GetValueFromEnum(p, parameters) ?? p.GetMethod?.Invoke(parameters, null);
                }
                else if (p.PropertyType.GetTypeInfo().IsEnum || p.PropertyType.IsNullableEnum())
                {
                    value = GetLowerCaseValueFromEnum(p, parameters) ?? p.GetMethod?.Invoke(parameters, null);
                }
                // ReSharper disable once ArrangeRedundantParentheses
                else if (p.PropertyType == typeof(bool) || (underlyingType != null && underlyingType == typeof(bool)))
                {
                    value = p.GetMethod?.Invoke(parameters, null);
                    if (value != null)
                        value = (bool)value ? "1" : "0";
                }
                else
                {
                    value = p.GetMethod?.Invoke(parameters, null);
                }

                if (value == null)
                {
                    continue;
                }

                beaconList.Add(attr.Name, Convert.ToString(value, CultureInfo.InvariantCulture)!);
            }

            var parametersType = parameters.GetType();
            // ReSharper disable once InvertIf
            if (typeof(IProvideProductsParameters).IsAssignableFrom(parametersType))
            {
                beaconList.AddRange(GetProductsParameters((IProvideProductsParameters)parameters));
            }

            return beaconList.ToDictionary(key => key.Item1, value => value.Item2);
        }

        private static BeaconList<string, string> GetProductsParameters(IProvideProductsParameters? parameters)
        {
            var result = new BeaconList<string, string>();
            
            if (parameters?.Products.Any() != true) return result;
            
            var productIndex = 1;
            foreach (var product in parameters.Products)
            {
                var parameterList = GetParametersDictionary(product);
                foreach (var customDimension in product.GetCustomDimensions())
                {
                    parameterList.Add(customDimension.Name, customDimension.Value);
                }

                parameterList =
                    parameterList.ToDictionary(key => $"pr{productIndex}{key.Key}", value => value.Value);
                result.AddRange(parameterList);

                productIndex++;
            }

            return result;
        }

        private static object? GetValueFromEnum(PropertyInfo propertyInfo, IProvideBeaconParameters parameters)
        {
            var value = propertyInfo.GetMethod?.Invoke(parameters, null);

            if (value == null) return null;

            var propertyType = propertyInfo.PropertyType.IsNullableEnum()
                ? Nullable.GetUnderlyingType(propertyInfo.PropertyType)
                : propertyInfo.PropertyType;

            if (propertyType == null) return null;

            var enumValue = Enum.Parse(propertyType, value.ToString()!);

            return enumValue.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }

        private static object? GetLowerCaseValueFromEnum(PropertyInfo propertyInfo, IProvideBeaconParameters parameters)
        {
            var value = propertyInfo.GetMethod?.Invoke(parameters, null);
            return value?.ToString()?.ToLowerInvariant();
        }

        /// <summary>
        /// Set base, required parameters if they are empty: TrackingId, ClientId.
        /// </summary>
        /// <param name="parameters">GA request parameters.</param>
        private void SetRequiredParameters(IGeneralParameters parameters)
        {
            if (string.IsNullOrEmpty(parameters.ProtocolVersion))
            {
                throw new ArgumentException("No ProtocolVersion", nameof(parameters));
            }

            if (string.IsNullOrEmpty(parameters.TrackingId))
            {
                parameters.TrackingId = TrackingAccount;
            }

            if (string.IsNullOrEmpty(parameters.ClientId))
            {
                parameters.ClientId = AnalyticsSession.GenerateSessionId();
            }
        }

        /// <summary>
        /// Set additional properties in parameters if they are empty: CacheBuster.
        /// The CacheBuster is set when UseHttpGet flag is set.
        /// <para>
        /// Override to change other properties.</para>
        /// </summary>
        /// <param name="parameters">GA request parameters.</param>
        protected virtual void AmendParameters(IGeneralParameters parameters)
        {
            if (UseHttpGet && string.IsNullOrEmpty(parameters.CacheBuster))
            {
                parameters.CacheBuster = AnalyticsSession.GenerateCacheBuster();
            }
        }

        /// <summary>
        /// Send parameters to GA endpoint.
        /// </summary>
        /// <param name="generalParameters">GA request parameters.</param>
        /// <returns>Result of the request.</returns>
        public async Task<TrackingResult> TrackAsync(IGeneralParameters generalParameters)
        {
            AmendParameters(generalParameters);
            // Set required must come after amend.
            SetRequiredParameters(generalParameters);

            var parameters = GetParametersDictionary(generalParameters);

            return await RequestUrlAsync(EndpointUrl, parameters, generalParameters.UserAgent ?? UserAgent);
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
