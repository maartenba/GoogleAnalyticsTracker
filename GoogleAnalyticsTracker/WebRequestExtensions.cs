using System;
using System.IO;
using System.Net;
using System.Threading;

namespace GoogleAnalyticsTracker
{
    internal static class WebRequestExtensions
    {
        public static WebResponse GetResponse(this WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult asyncResult = request.BeginGetResponse(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetResponse(asyncResult);
        }

        public static Stream GetRequestStream(this WebRequest request)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            IAsyncResult asyncResult = request.BeginGetRequestStream(r => autoResetEvent.Set(), null);

            // Wait until the call is finished
            autoResetEvent.WaitOne();

            return request.EndGetRequestStream(asyncResult);
        }
    }
}