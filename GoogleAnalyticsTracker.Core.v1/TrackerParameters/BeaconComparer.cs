using System.Collections.Generic;

namespace GoogleAnalyticsTracker.Core.v1.TrackerParameters
{
    class BeaconComparer : IComparer<string>
    {
        static readonly List<string> Ordered = new List<string> { "t", "cid", "tid", "v" };

        public int Compare(string x, string y)
        {
            var xi = Ordered.IndexOf(x);
            var yi = Ordered.IndexOf(y);

            if (xi > yi)
                return -1;

            if (xi < yi)
                return 1;

            return 0;
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