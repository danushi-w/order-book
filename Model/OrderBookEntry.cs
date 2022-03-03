using System;
namespace OrderBookTest.Model
{
    public class OrderBookEntry
    {
        public DateTime CreationTime { get; private set; }

        public Limit Limit { get; set; }

        public Order CurrentOrder { get; set; }

        public OrderBookEntry Next { get; set; }

        public OrderBookEntry Previous { get; set; }


        public OrderBookEntry(Limit limit, Order order)
        {
            CreationTime = DateTime.UtcNow;
            Limit = limit;
            CurrentOrder = order;
        }

    }
}
