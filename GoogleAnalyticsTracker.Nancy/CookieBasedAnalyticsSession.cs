using GoogleAnalyticsTracker.Core;
using Nancy;

namespace GoogleAnalyticsTracker.Nancy
{
    public class CookieBasedAnalyticsSession : AnalyticsSession, IRequireRequestAndResponse
    {
        private const string StorageKeyUniqueId = "_GAT_uqid";
        private const string StorageKeyFirstVisitTime = "_GAT_fvt";
        private const string StorageKeyPreviousVisitTime = "_GAT_pvt";
        private const string StorageKeySessionCount = "_GAT_sc";

        private Request _requestMessage;
        private Response _responseMessage;

        private Request GetHttpRequestMessage()
        {
            return _requestMessage;
        }

        private Response GetHttpResponseMessage()
        {
            return _responseMessage;
        }

        public void SetRequestAndResponse(Request requestMessage, Response responseMessage)
        {
            _requestMessage = requestMessage;
            _responseMessage = responseMessage;
        }

        protected override string GetUniqueVisitorId()
        {
            var requestMessage = GetHttpRequestMessage();
            var responseMessage = GetHttpResponseMessage();
            if (requestMessage != null && responseMessage != null)
            {
                if (string.IsNullOrEmpty(requestMessage.GetDeserializedCookieValue(StorageKeyUniqueId)))
                {
                    responseMessage.SetSerializedCookieValue(StorageKeyUniqueId, base.GetUniqueVisitorId());
                }
                return requestMessage.GetDeserializedCookieValue(StorageKeyUniqueId);
            }
            return base.GetUniqueVisitorId();
        }

        protected override int GetFirstVisitTime()
        {
            var requestMessage = GetHttpRequestMessage();
            var responseMessage = GetHttpResponseMessage();
            if (requestMessage != null && responseMessage != null)
            {
                int firstVisitTime;
                if (int.TryParse(requestMessage.GetDeserializedCookieValue(StorageKeyFirstVisitTime), out firstVisitTime) && firstVisitTime == 0)
                {
                    firstVisitTime = base.GetFirstVisitTime();
                    responseMessage.SetSerializedCookieValue(StorageKeyFirstVisitTime, firstVisitTime);
                }
                return firstVisitTime;
            }
            return base.GetFirstVisitTime();
        }

        protected override int GetPreviousVisitTime()
        {
            var requestMessage = GetHttpRequestMessage();
            var responseMessage = GetHttpResponseMessage();
            if (requestMessage != null && responseMessage != null)
            {
                int previousVisitTime;
                int.TryParse(requestMessage.GetDeserializedCookieValue(StorageKeyPreviousVisitTime), out previousVisitTime);
                responseMessage.SetSerializedCookieValue(StorageKeyPreviousVisitTime, GetCurrentVisitTime());

                if (previousVisitTime == 0)
                {
                    previousVisitTime = GetCurrentVisitTime();
                }

                return previousVisitTime;
            }
            return base.GetPreviousVisitTime();
        }

        protected override int GetSessionCount()
        {
            var requestMessage = GetHttpRequestMessage();
            var responseMessage = GetHttpResponseMessage();
            if (requestMessage != null && responseMessage != null)
            {
                int sessionCount;
                int.TryParse(requestMessage.GetDeserializedCookieValue(StorageKeySessionCount), out sessionCount);
                responseMessage.SetSerializedCookieValue(StorageKeySessionCount, ++sessionCount);
                return sessionCount;
            }
            return base.GetSessionCount();
        }
    }
}
