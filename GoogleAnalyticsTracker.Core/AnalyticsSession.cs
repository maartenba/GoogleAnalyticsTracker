using System;
using System.Globalization;
using GoogleAnalyticsTracker.Core.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core
{
    [PublicAPI]
    public class AnalyticsSession : IAnalyticsSession
    {
        protected string SessionId { get; set; }
        protected int SessionCount { get; set; }

        protected virtual string GetUniqueVisitorId()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            // ReSharper disable once UseStringInterpolation
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
        
        public virtual string GenerateSessionId()
        {
            return SessionId ??= Guid.NewGuid().ToString();
        }

        public virtual string GenerateCacheBuster()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            return random.Next(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        }
    }
}