using System;
using System.Web;
using Newtonsoft.Json;

namespace GoogleAnalyticsTracker.Web
{
    public class CookieBasedAnalyticsSession
        : AnalyticsSession, IAnalyticsSession
    {
        private const string StorageKeyUniqueId = "_GAT_uqid";
        private const string StorageKeyFirstVisitTime = "_GAT_fvt";
        private const string StorageKeyPreviousVisitTime = "_GAT_pvt";
        private const string StorageKeySessionCount = "_GAT_sc";

        protected HttpContextBase GetHttpContext()
        {
            if (HttpContext.Current != null)
            {
                return new HttpContextWrapper(HttpContext.Current);
            }
            return null;
        }

        protected override string GetUniqueVisitorId()
        {
            if (string.IsNullOrEmpty(GetHttpContext().GetDeserializedCookieValue<string>(StorageKeyUniqueId)))
            {
                GetHttpContext().SetSerializedCookieValue(StorageKeyUniqueId, base.GetUniqueVisitorId());
            }
            return GetHttpContext().GetDeserializedCookieValue<string>(StorageKeyUniqueId);
        }

        protected override int GetFirstVisitTime()
        {
            if (GetHttpContext().GetDeserializedCookieValue<int>(StorageKeyFirstVisitTime) == 0)
            {
                GetHttpContext().SetSerializedCookieValue(StorageKeyFirstVisitTime, base.GetFirstVisitTime());
            }
            return GetHttpContext().GetDeserializedCookieValue<int>(StorageKeyFirstVisitTime);
        }

        protected override int GetPreviousVisitTime()
        {
            var previousVisitTime = GetHttpContext().GetDeserializedCookieValue<int>(StorageKeyPreviousVisitTime);
            GetHttpContext().SetSerializedCookieValue(StorageKeyPreviousVisitTime, GetCurrentVisitTime());

            if (previousVisitTime == 0)
            {
                previousVisitTime = GetCurrentVisitTime();
            }

            return previousVisitTime;
        }

        protected override int GetSessionCount()
        {
            var sessionCount = GetHttpContext().GetDeserializedCookieValue<int>(StorageKeySessionCount);
            GetHttpContext().SetSerializedCookieValue(StorageKeySessionCount, ++sessionCount);
            return sessionCount;
        }
    }
}