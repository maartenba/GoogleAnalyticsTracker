using System;

namespace GoogleAnalyticsTracker.Core
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new(1970, 1, 1);

        public static int ToUnixTime(this DateTime current)
            => (int)current.Subtract(Epoch).TotalSeconds;
    }
}