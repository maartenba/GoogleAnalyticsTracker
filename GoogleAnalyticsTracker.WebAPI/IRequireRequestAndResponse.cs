using System.Net.Http;

namespace GoogleAnalyticsTracker.WebApi
{
    public interface IRequireRequestAndResponse
    {
        void SetRequestAndResponse(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage);
    }
}