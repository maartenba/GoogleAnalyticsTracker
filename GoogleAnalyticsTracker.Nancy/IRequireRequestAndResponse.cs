using Nancy;

namespace GoogleAnalyticsTracker.Nancy
{
    interface IRequireRequestAndResponse
    {
        void SetRequestAndResponse(Request requestMessage, Response responseMessage);
    }
}
