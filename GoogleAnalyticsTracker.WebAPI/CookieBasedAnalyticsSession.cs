using System;
using System.Net.Http;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.WebApi
{
    public class CookieBasedAnalyticsSession
        : AnalyticsSession, IAnalyticsSession, IRequireRequestAndResponse
    {
        private const string StorageKeyUniqueId = "_GAT_uqid";
        private const string StorageKeyFirstVisitTime = "_GAT_fvt";
        private const string StorageKeyPreviousVisitTime = "_GAT_pvt";
        private const string StorageKeySessionCount = "_GAT_sc";

        private HttpRequestMessage _requestMessage;
        private HttpResponseMessage _responseMessage;

        private HttpRequestMessage GetHttpRequestMessage()
        {
            return _requestMessage;
        }

        private HttpResponseMessage GetHttpResponseMessage()
        {
            return _responseMessage;
        }

        public void SetRequestAndResponse(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            _requestMessage = requestMessage;
            _responseMessage = responseMessage;
        }

        protected override string GetUniqueVisitorId()
        {
            if (string.IsNullOrEmpty(GetHttpRequestMessage().GetDeserializedCookieValue(StorageKeyUniqueId)))
            {
                GetHttpResponseMessage().SetSerializedCookieValue(StorageKeyUniqueId, base.GetUniqueVisitorId());
            }
            return GetHttpRequestMessage().GetDeserializedCookieValue(StorageKeyUniqueId);
        }

        protected override int GetFirstVisitTime()
        {
            int firstVisitTime = 0;
            if (int.TryParse(GetHttpRequestMessage().GetDeserializedCookieValue(StorageKeyFirstVisitTime), out firstVisitTime) && firstVisitTime == 0)
            {
                firstVisitTime = base.GetFirstVisitTime();
                GetHttpResponseMessage().SetSerializedCookieValue(StorageKeyFirstVisitTime, firstVisitTime);
            }
            return firstVisitTime;
        }

        protected override int GetPreviousVisitTime()
        {
            int previousVisitTime = 0;
            int.TryParse(GetHttpRequestMessage().GetDeserializedCookieValue(StorageKeyPreviousVisitTime), out previousVisitTime);
            GetHttpResponseMessage().SetSerializedCookieValue(StorageKeyPreviousVisitTime, GetCurrentVisitTime());

            if (previousVisitTime == 0)
            {
                previousVisitTime = GetCurrentVisitTime();
            }

            return previousVisitTime;
        }

        protected override int GetSessionCount()
        {
            int sessionCount = 0;
            int.TryParse(GetHttpRequestMessage().GetDeserializedCookieValue(StorageKeySessionCount), out sessionCount);
            GetHttpResponseMessage().SetSerializedCookieValue(StorageKeySessionCount, ++sessionCount);
            return sessionCount;
        }
    }
}