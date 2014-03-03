using System.Net;
using System.Reflection;

namespace GoogleAnalyticsTracker.Core
{
    public static class HttpWebRequestExtensions
    {
        /// <summary>
        /// Workaround for PCL's "Additional information: The '.....' header must be modified using the appropriate property or method." exception.
        /// </summary>
        /// <param name="Request">Request</param>
        /// <param name="Header">Header name</param>
        /// <param name="Value">Value for the header</param>
        public static void SetHeader(this HttpWebRequest Request, string Header, string Value)
        {
            // Retrieve the property through reflection.
            var propertyInfo = Request.GetType().GetProperty(Header.Replace("-", string.Empty));

            // Check if the property is available.
            if (propertyInfo != null)
            {
                // Set the value of the header.
                propertyInfo.SetValue(Request, Value, null);
            }
            else
            {
                // Set the value of the header.
                Request.Headers[Header] = Value;
            }
        }
    }
}