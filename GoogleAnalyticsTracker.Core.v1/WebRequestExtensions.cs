using System.IO;
using System.Net;
using System.Threading;

namespace GoogleAnalyticsTracker.Core.v1
{
    internal static class WebRequestExtensions
    {
        public static WebResponse GetResponse(this WebRequest request)
        {
            var autoResetEvent = new AutoResetEvent(false);

            var asyncResult = request.BeginGetResponse(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetResponse(asyncResult);
        }

        public static Stream GetRequestStream(this WebRequest request)
        {
            var autoResetEvent = new AutoResetEvent(false);

            var asyncResult = request.BeginGetRequestStream(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetRequestStream(asyncResult);
        }
    }
}