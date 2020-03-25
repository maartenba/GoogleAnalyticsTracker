﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core.TrackerParameters;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

namespace GoogleAnalyticsTracker.Core
{
    public partial class TrackerBase
    {
        private static IDictionary<string, string> GetParametersDictionary(object parameters)
        {
            var beaconList = new BeaconList<string, string>();

            foreach (var p in parameters.GetType().GetRuntimeProperties())
            {
                if (!(p.GetCustomAttribute(typeof(BeaconAttribute), true) is BeaconAttribute attr))
                {
                    continue;
                }

                object value;
                var underlyingType = Nullable.GetUnderlyingType(p.PropertyType);

                if ((p.PropertyType.GetTypeInfo().IsEnum || p.PropertyType.IsNullableEnum()) && attr.IsEnumByValueBased)
                {
                    value = GetValueFromEnum(p, parameters) ?? p.GetMethod.Invoke(parameters, null);
                }
                else if (p.PropertyType.GetTypeInfo().IsEnum || p.PropertyType.IsNullableEnum())
                {
                    value = GetLowerCaseValueFromEnum(p, parameters) ?? p.GetMethod.Invoke(parameters, null);
                }
                else if (p.PropertyType == typeof(bool) || (underlyingType != null && underlyingType == typeof(bool)))
                {
                    value = p.GetMethod.Invoke(parameters, null);
                    if (value != null)
                        value = (bool)value ? "1" : "0";
                }
                else
                {
                    value = p.GetMethod.Invoke(parameters, null);
                }

                if (value == null)
                {
                    continue;
                }

                beaconList.Add(attr.Name, Convert.ToString(value, CultureInfo.InvariantCulture));
            }

            if (parameters.GetType() == typeof(EnhancedECommerceTransaction))
            {
                beaconList.AddRange(GetProductsParameters((EnhancedECommerceTransaction)parameters));
            }

            return beaconList.ToDictionary(key => key.Item1, value => value.Item2);
        }

        private static BeaconList<string, string> GetProductsParameters(IEnhancedECommerceTransactionParameters transaction)
        {
            var result = new BeaconList<string, string>();
            
            if (transaction.Products == null || !transaction.Products.Any()) return result;
            
            var productIndex = 1;
            foreach (var product in transaction.Products)
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
        
        private static object GetValueFromEnum(PropertyInfo propertyInfo, object parameters)
        {
            var value = propertyInfo.GetMethod.Invoke(parameters, null);

            if (value == null) return null;

            var propertyType = propertyInfo.PropertyType.IsNullableEnum()
                ? Nullable.GetUnderlyingType(propertyInfo.PropertyType)
                : propertyInfo.PropertyType;

            if (propertyType == null) return null;

            var enumValue =
                Enum.Parse(propertyType, value.ToString());

            return enumValue.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }

        private static object GetLowerCaseValueFromEnum(PropertyInfo propertyInfo, object parameters)
        {
            var value = propertyInfo.GetMethod.Invoke(parameters, null);

            return value?.ToString().ToLowerInvariant();
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
    }
}
