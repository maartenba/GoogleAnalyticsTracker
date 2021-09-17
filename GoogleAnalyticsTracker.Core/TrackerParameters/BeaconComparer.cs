using System.Collections.Generic;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    public class BeaconComparer : IComparer<string?>
    {
        private static readonly List<string> Ordered = new() { "t", "cid", "tid", "v" }; // reverse order
        private static readonly List<string> LastOrdered = new() { "z" }; // direct order

        public int Compare(string? x, string? y)
        {
            if (x == null && y == null)
                return 0;
            if (x != null && y == null)
                return -1;
            if (x == null && y != null)
                return 1;
            
            var xi = Ordered.IndexOf(x!);
            var yi = Ordered.IndexOf(y!);
            if (xi > yi)
                return -1;
            if (xi < yi)
                return 1;

            xi = LastOrdered.IndexOf(x!);
            yi = LastOrdered.IndexOf(y!);
            if (xi > yi)
                return 1;
            if (xi < yi)
                return -1;

            return string.CompareOrdinal(x, y);
        }

        public bool Equals(string? x, string? y)
        {
            if (x != null)
                return y != null && x.Equals(y);

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (y != null)
                return false;

            return true;
        }
    }
}