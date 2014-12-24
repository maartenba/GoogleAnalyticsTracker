using System.Net.Http;

namespace GoogleAnalyticsTracker.WebAPI2.v1.Interface
{
    public interface IRequireRequestAndResponse
    {
        void SetRequestAndResponse(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage);
    }
}