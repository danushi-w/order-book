using System;
using System.Collections.Generic;

namespace OrderBookTest.Model
{
    public class Limit
    {
        public decimal Price { get; private set; }

        public OrderBookEntry Head { get; set; }

        public OrderBookEntry Tail { get; set; }

        public bool IsEmpty
        {
            get { return Head == null && Tail == null; }
        }

        public Limit(decimal price)
        {
            Price = price;
        }

        public int GetLimitOrderCount()
        {
            int orderCount = 0;

            var pointer = Head;

            while (pointer != null)
            {
                if (pointer.CurrentOrder.Quantity > 0)
                    orderCount++;
                pointer = pointer.Next;
            }

            return orderCount;
        }

        public int GetLimitOrderQuantity()
        {
            int orderQuantity = 0;

            var pointer = Head;

            while (pointer != null)
            {
                orderQuantity += pointer.CurrentOrder.Quantity;
                pointer = pointer.Next;
            }

            return orderQuantity;
        }

        public List<Order> GetLimitOrderRecords()
        {
            List<Order> orderRecords = new List<Order>();
            OrderBookEntry pointer = Head;

            while (Head != null)
            {
                var currentOrder = pointer.CurrentOrder;
                if (currentOrder.Quantity > 0)
                    orderRecords.Add(currentOrder);

                pointer = pointer.Next;
            }

            return orderRecords;
        }
    }
}