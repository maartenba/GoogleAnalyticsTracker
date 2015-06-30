using System;
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
        private static IDictionary<string, string> GetParametersDictionary(IGeneralParameters parameters)
        {
            var beaconList = new BeaconList<string, string>();

            foreach (var p in parameters.GetType().GetRuntimeProperties())
            {
                var attr = p.GetCustomAttribute(typeof(BeaconAttribute), true) as BeaconAttribute;

                if (attr == null) continue;

                object value;

                if ((p.PropertyType.GetTypeInfo().IsEnum || p.PropertyType.IsNullableEnum()) && attr.IsEnumByValueBased)
                    value = GetValueFromEnum(p, parameters) ?? p.GetMethod.Invoke(parameters, null);
                else if (p.PropertyType.GetTypeInfo().IsEnum || p.PropertyType.IsNullableEnum())
                    value = GetLowerCaseValueFromEnum(p, parameters) ?? p.GetMethod.Invoke(parameters, null);
                else
                    value = p.GetMethod.Invoke(parameters, null);

                if (value != null)
                    beaconList.Add(attr.Name, value.ToString());
            }

            beaconList.ShiftToLast("z");
            return beaconList.OrderBy(k => k.Item1, new BeaconComparer()).ToDictionary(key => key.Item1, value => value.Item2);
        }

        private static object GetValueFromEnum(PropertyInfo propertyInfo, IGeneralParameters parameters)
        {
            var value = propertyInfo.GetMethod.Invoke(parameters, null);

            if (value == null) return null;

            var enumValue =
                Enum.Parse(propertyInfo.PropertyType.IsNullableEnum()
                        ? Nullable.GetUnderlyingType(propertyInfo.PropertyType)
                        : propertyInfo.PropertyType, value.ToString());

            return enumValue.GetHashCode().ToString(CultureInfo.InvariantCulture);
        }

        private static object GetLowerCaseValueFromEnum(PropertyInfo propertyInfo, IGeneralParameters parameters)
        {
            var value = propertyInfo.GetMethod.Invoke(parameters, null);

            return value == null ? null : value.ToString().ToLowerInvariant();
        }

        private void SetRequired(IGeneralParameters parameters)
        {
            parameters.TrackingId = TrackingAccount;
            parameters.ClientId = AnalyticsSession.GenerateSessionId();
        }

        public async Task<TrackingResult> TrackPageViewAsync(PageView pageView)
        {
            SetRequired(pageView);

            var parameters = GetParametersDictionary(pageView);

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, pageView.UserAgent ?? UserAgent);
        }

        public async Task<TrackingResult> TrackEventAsync(EventTracking eventTracking)
        {
            SetRequired(eventTracking);

            var parameters = GetParametersDictionary(eventTracking);

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, eventTracking.UserAgent ?? UserAgent);
        }

        public async Task<TrackingResult> TrackTransactionAsync(ECommerceTransaction eCommerceTransaction)
        {
            SetRequired(eCommerceTransaction);

            var parameters = GetParametersDictionary(eCommerceTransaction);

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, eCommerceTransaction.UserAgent ?? UserAgent);
        }

        public async Task<TrackingResult> TrackTransactionItemAsync(ECommerceItem eCommerceItem)
        {
            SetRequired(eCommerceItem);

            var parameters = GetParametersDictionary(eCommerceItem);

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, eCommerceItem.UserAgent ?? UserAgent);
        }

        public async Task<TrackingResult> TrackUserTimingAsync(UserTimings userTimings)
        {
            SetRequired(userTimings);

            var parameters = GetParametersDictionary(userTimings);

            return await RequestUrlAsync(UseSsl ? BeaconUrlSsl : BeaconUrl, parameters, userTimings.UserAgent ?? UserAgent);
        }
    }
}