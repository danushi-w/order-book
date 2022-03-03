using System;
using System.Collections.Generic;

namespace OrderBookTest.Model
{
    public class OrderBook
    {
        private readonly Instrument instrument;

        private readonly SortedSet<Limit> bids = new SortedSet<Limit>();

        private readonly SortedSet<Limit> asks = new SortedSet<Limit>();

        private readonly Dictionary<long, OrderBookEntry> orderCache = new Dictionary<long, OrderBookEntry>();

        // public SortedSet<Limit> Bids { get; private set; }

        // public SortedSet<Limit> Asks { get; private set; }


        public OrderBook(Instrument instrument)
        {
            this.instrument = instrument;
        }

        public void AddOrder(Order order)
        {
            var limit = new Limit(order.Price);

            AddOrder(order, limit, order.IsBuy ? bids : asks, orderCache);
        }

        public void AddOrder(Order order, Limit limit, SortedSet<Limit> set, Dictionary<long, OrderBookEntry> orderCache)
        {
            OrderBookEntry orderBookEntry = new OrderBookEntry(limit, order);

            // If set already contains Limit level, add OrderBookEntry to existing limit
            if (set.TryGetValue(limit, out Limit existingLimit))
            {
                // If Limit level contains no OrderBookEntries, add to Head and set Tail
                if (existingLimit.Head == null)
                {
                    existingLimit.Head = orderBookEntry;
                    existingLimit.Tail = orderBookEntry;
                }
                else
                {
                    existingLimit.Tail.Next = orderBookEntry;
                    orderBookEntry.Previous = existingLimit.Tail;
                    existingLimit.Tail = orderBookEntry;
                }
            }
            // Otherwise, add new Limit level to set
            else
            {
                set.Add(limit);
                limit.Head = orderBookEntry;
                limit.Tail = orderBookEntry;
            }

            orderCache.Add(order.OrderId, orderBookEntry);
        }

        public void UpdateOrder(Order order)
        {
            if (orderCache.ContainsKey(order.OrderId))
            {
                // Remove and add OrderBookEntry as item loses its priority
                RemoveOrder(order.OrderId);
                AddOrder(order);
            }
        }

        public void RemoveOrder(long orderId)
        {
            // If order exists in cache
            if (orderCache.TryGetValue(orderId, out OrderBookEntry orderBookEntry))
            {
                // OrderBookEntry is the only OrderBookEntry on this limit level
                if (orderBookEntry.Previous == null && orderBookEntry.Next == null)
                {
                    orderBookEntry.Limit.Head = null;
                    orderBookEntry.Limit.Tail = null;
                }
                // OrderBookEntry is at the Head of the Limit Level
                else if (orderBookEntry.Previous == null)
                {
                    orderBookEntry.Next.Previous = null;
                    orderBookEntry.Limit.Head = orderBookEntry.Next;
                }
                // OrderBookEntry is at the Tail of the Limit Level
                else if (orderBookEntry.Next == null)
                {
                    orderBookEntry.Previous.Next = null;
                    orderBookEntry.Limit.Tail = orderBookEntry.Previous;
                }
                // OrderBookEntry is in the middle of the Limit Level
                else
                {
                    orderBookEntry.Previous.Next = orderBookEntry.Next;
                    orderBookEntry.Next.Previous = orderBookEntry.Previous;
                }


                // Remove OrderBookEntry from order cache
                orderCache.Remove(orderId);
            }
        }
    }
}
