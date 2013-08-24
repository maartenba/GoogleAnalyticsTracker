using System;
using System.Web;

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
            if (string.IsNullOrEmpty(GetHttpContext().GetDeserializedCookieValue(StorageKeyUniqueId)))
            {
                GetHttpContext().SetSerializedCookieValue(StorageKeyUniqueId, base.GetUniqueVisitorId());
            }
            return GetHttpContext().GetDeserializedCookieValue(StorageKeyUniqueId);
        }

        protected override int GetFirstVisitTime()
        {
            int firstVisitTime = 0;
            if (int.TryParse(GetHttpContext().GetDeserializedCookieValue(StorageKeyFirstVisitTime), out firstVisitTime) && firstVisitTime == 0)
            {
                firstVisitTime = base.GetFirstVisitTime();
                GetHttpContext().SetSerializedCookieValue(StorageKeyFirstVisitTime, firstVisitTime);
            }
            return firstVisitTime;
        }

        protected override int GetPreviousVisitTime()
        {
            int previousVisitTime = 0;
            int.TryParse(GetHttpContext().GetDeserializedCookieValue(StorageKeyPreviousVisitTime), out previousVisitTime);
            GetHttpContext().SetSerializedCookieValue(StorageKeyPreviousVisitTime, GetCurrentVisitTime());

            if (previousVisitTime == 0)
            {
                previousVisitTime = GetCurrentVisitTime();
            }

            return previousVisitTime;
        }

        protected override int GetSessionCount()
        {
            int sessionCount = 0;
            int.TryParse(GetHttpContext().GetDeserializedCookieValue(StorageKeySessionCount), out sessionCount);
            GetHttpContext().SetSerializedCookieValue(StorageKeySessionCount, ++sessionCount);
            return sessionCount;
        }
    }
}