using System;
using System.Collections.Generic;
using OrderBookTest.Model;

namespace OrderBookTest.Interface
{
    public interface IOrderBook
    {
        void AddOrder(Order order);

        void UpdateOrder(Order order);

        void RemoveOrder(long orderId);

        List<OrderBookEntry> GetBidOrders();

        List<OrderBookEntry> GetAskOrders();

        void WriteBookToLog();
    }
}
