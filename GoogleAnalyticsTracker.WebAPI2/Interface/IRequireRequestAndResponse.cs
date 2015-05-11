using System.Net.Http;

namespace GoogleAnalyticsTracker.WebAPI2.Interface
{
    public interface IRequireRequestAndResponse
    {
        void SetRequestAndResponse(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage);
    }
}