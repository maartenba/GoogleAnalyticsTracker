using System.Net.Http;

namespace GoogleAnalyticsTracker.WebApi2
{
    public interface IRequireRequestAndResponse
    {
        void SetRequestAndResponse(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage);
    }
}