using System;

namespace GoogleAnalyticsTracker
{
    public static class DateTimeExtensions
    {
        static readonly DateTime Epoch = new DateTime(1970, 1, 1);

        public static int ToUnixTime(this DateTime current)
        {
            return (int)current.Subtract(Epoch).TotalSeconds;
        }
    }
}