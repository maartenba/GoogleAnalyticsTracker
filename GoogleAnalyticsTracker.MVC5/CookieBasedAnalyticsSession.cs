using System.Web;
using GoogleAnalyticsTracker.Core;

namespace GoogleAnalyticsTracker.MVC5
{
    public class CookieBasedAnalyticsSession : AnalyticsSession
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
            var httpContext = GetHttpContext();
            if (httpContext != null)
            {
                if (string.IsNullOrEmpty(httpContext.GetDeserializedCookieValue(StorageKeyUniqueId)))
                {
                    httpContext.SetSerializedCookieValue(StorageKeyUniqueId, base.GetUniqueVisitorId());
                }
                return httpContext.GetDeserializedCookieValue(StorageKeyUniqueId);
            }
            return base.GetUniqueVisitorId();
        }

        protected override int GetFirstVisitTime()
        {
            var httpContext = GetHttpContext();
            if (httpContext != null)
            {
                int firstVisitTime = 0;
                if (int.TryParse(httpContext.GetDeserializedCookieValue(StorageKeyFirstVisitTime), out firstVisitTime) && firstVisitTime == 0)
                {
                    firstVisitTime = base.GetFirstVisitTime();
                    httpContext.SetSerializedCookieValue(StorageKeyFirstVisitTime, firstVisitTime);
                }
                return firstVisitTime;
            }
            return base.GetFirstVisitTime();
        }

        protected override int GetPreviousVisitTime()
        {
            var httpContext = GetHttpContext();
            if (httpContext != null)
            {
                int previousVisitTime = 0;
                int.TryParse(httpContext.GetDeserializedCookieValue(StorageKeyPreviousVisitTime), out previousVisitTime);
                httpContext.SetSerializedCookieValue(StorageKeyPreviousVisitTime, GetCurrentVisitTime());

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
            var httpContext = GetHttpContext();
            if (httpContext != null)
            {
                int sessionCount = 0;
                int.TryParse(httpContext.GetDeserializedCookieValue(StorageKeySessionCount), out sessionCount);
                httpContext.SetSerializedCookieValue(StorageKeySessionCount, ++sessionCount);
                return sessionCount;
            }
            return base.GetSessionCount();
        }
    }
}