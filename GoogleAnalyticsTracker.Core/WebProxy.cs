using System;
using System.Net;

namespace GoogleAnalyticsTracker.Core
{
    public class WebProxy : IWebProxy
    {
        private readonly Uri uri;

        public WebProxy(string url, int port)
        {
            this.uri = new Uri($"{url}:{port}");
        }

        public Uri GetProxy(Uri destination)
        {
            return uri;
        }

        public bool IsBypassed(Uri host)
        {
            return false;
        }

        public ICredentials Credentials { get; set; }
    }
}
