using System;
using System.Collections.Generic;

namespace OrderBookTest.Model
{
    public class BidLimitComparer : IComparer<Limit>
    {
        public static IComparer<Limit> Comparer { get; } = new BidLimitComparer();
        // Bid limits should be sorted in descending order
        public int Compare(Limit x, Limit y)
        {
            if (x.Price == y.Price)
                return 0;
            else if (x.Price < y.Price)
                return 1;
            else
                return -1;
        }
    }

    public class AskLimitComparer : IComparer<Limit>
    {
        public static IComparer<Limit> Comparer { get; } = new AskLimitComparer();

        // Ask limits should be sorted in ascending order
        public int Compare(Limit x, Limit y)
        {
            if (x.Price == y.Price)
                return 0;
            else if (x.Price > y.Price)
                return 1;
            else
                return -1;
        }
    }
}
