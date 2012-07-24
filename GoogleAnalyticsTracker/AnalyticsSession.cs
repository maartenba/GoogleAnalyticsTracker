using System;
using System.Globalization;

namespace GoogleAnalyticsTracker
{
    public class AnalyticsSession
        : IAnalyticsSession
    {
        public virtual string GenerateSessionId()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            return random.Next(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        }

        public virtual string GenerateCookieValue()
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            var cookie = string.Format("{0}{1}", random.Next(100000000, 999999999), "00145214523");

            var randomvalue = random.Next(1000000000, 2147483647).ToString(CultureInfo.InvariantCulture);

            return string.Format("__utma=1.{0}.{1}.{2}.{2}.15;+__utmz=1.{2}.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none);", cookie, randomvalue, DateTime.UtcNow.Ticks);
        }
    }
}