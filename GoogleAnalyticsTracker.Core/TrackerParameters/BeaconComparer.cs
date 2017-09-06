using System.Collections.Generic;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class BeaconComparer : IComparer<string>
    {
        private static readonly List<string> Ordered = new List<string> { "t", "cid", "tid", "v" }; // reverse order
        private static readonly List<string> LastOrdered = new List<string> { "z" }; // direct order

        public int Compare(string x, string y)
        {
            var xi = Ordered.IndexOf(x);
            var yi = Ordered.IndexOf(y);
            if (xi > yi)
                return -1;
            if (xi < yi)
                return 1;

            xi = LastOrdered.IndexOf(x);
            yi = LastOrdered.IndexOf(y);
            if (xi > yi)
                return 1;
            if (xi < yi)
                return -1;

            return string.CompareOrdinal(x, y);
        }

        public bool Equals(string x, string y)
        {
            if (x != null)
                return ((y != null) && x.Equals(y));

            if (y != null)
                return false;

            return true;
        }
    }
}