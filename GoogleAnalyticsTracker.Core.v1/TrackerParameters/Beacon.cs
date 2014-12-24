using System;
using System.Collections.Generic;

namespace GoogleAnalyticsTracker.Core.v1.TrackerParameters
{
    class Beacon<TKey, TValue> : Tuple<TKey, TValue>
    {       
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Tuple`2"/> class.
        /// </summary>
        /// <param name="item1">The value of the tuple's first component.</param><param name="item2">The value of the tuple's second component.</param>
        public Beacon(TKey item1, TValue item2) : base(item1, item2) {}
     
        public override bool Equals(object other)
        {
            return Equals(other as Beacon<TKey, TValue>);
        }

        public virtual bool Equals(Beacon<TKey, TValue> other)
        {
            if (other == null) { return false; }
            if (object.ReferenceEquals(this, other)) { return true; }
            var comparer = Comparer<TKey>.Default;
            return comparer.Compare(Item1, other.Item1) == 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }                
    }
}