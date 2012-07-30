using System;
using System.Globalization;

namespace GoogleAnalyticsTracker
{
    public class AnalyticsSession
        : IAnalyticsSession
    {
        protected string SessionId { get; set; }
        protected string Cookie { get; set; }
        protected int SessionCount { get; set; }

        protected virtual string GetUniqueVisitorId()
        {
            Random random = new Random();
            return string.Format("{0}{1}", random.Next(100000000, 999999999), "00145214523");
        }

        protected virtual int GetFirstVisitTime()
        {
            return DateTime.UtcNow.ToUnixTime();
        }

        protected virtual int GetPreviousVisitTime()
        {
            return DateTime.UtcNow.ToUnixTime();
        }

        protected virtual int GetCurrentVisitTime()
        {
            return DateTime.UtcNow.ToUnixTime();
        }

        protected virtual int GetSessionCount()
        {
            return ++SessionCount;
        }

        public virtual string GenerateCookieValue()
        {
            //__utma cookie syntax: domain-hash.unique-id.FirstVisitTime.PreviousVisitTime.CurrentVisitTime.session-counter
            if (Cookie == null)
            {
                Cookie = string.Format("__utma=1.{0}.{1}.{2}.{3}.{4};+__utmz=1.{3}.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none);",
                    GetUniqueVisitorId(), GetFirstVisitTime(), GetPreviousVisitTime(), GetCurrentVisitTime(), GetSessionCount());
            }
            return Cookie;
        }

        public virtual string GenerateSessionId()
        {
            if (SessionId == null)
            {
                Random random = new Random();
                SessionId = random.Next(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
            }
            return SessionId;
        }
    }
}