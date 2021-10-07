using System.Net;
using System.Reflection;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core;

[PublicAPI]
public static class HttpWebRequestExtensions
{
    /// <summary>
    /// Workaround for PCL "Additional information: The '.....' header must be modified using the appropriate property or method." exception.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="header">Header name</param>
    /// <param name="value">Value for the header</param>
    public static void SetHeader(this HttpWebRequest request, string header, string value)
    {
        // Retrieve the property through reflection.
        var propertyInfo = request.GetType().GetRuntimeProperty(header.Replace("-", string.Empty));

        // Check if the property is available.
        if (propertyInfo != null)            
            // Set the value of the header.
            propertyInfo.SetValue(request, value, null);            
        else            
            // Set the value of the header.
            request.Headers[header] = value;            
    }
}