namespace OrderBookTest.Consumer
{
    using System;
    using Model;
    using Interface;
    using System.Collections.Generic;

    /// <summary>
    /// TODO: Fill in this class and any other required classes.
    /// </summary>
    public class OrderConsumer : IOrderConsumer
    {
        ILog logger;
        Dictionary<string, IOrderBook> exchange;
        Dictionary<long, IOrderBook> orderBookCache;
        IOrderBook orderBook;
         

        public void StartProcessing(object sender, ProcessingStartEventArgs args)
        {
            logger = args.Log;
            exchange = new Dictionary<string, IOrderBook>();
            orderBookCache = new Dictionary<long, IOrderBook>();
        }

        public void HandleOrderAction(object sender, OrderActionEventArgs args)
        {
            Order order = args.Order;

            if (args.Action == Model.Action.Add)
            {
                Instrument instrument = new Instrument(order.Symbol);

                // If exchange does not contain an orderbook for instrument
                if (!exchange.TryGetValue(instrument.Symbol, out orderBook))
                {
                    // create new OrderBook and add to exchange
                    orderBook = new OrderBook(instrument, logger);
                    exchange.Add(instrument.Symbol, orderBook);
                }

                orderBook.AddOrder(order);
                orderBookCache.Add(order.OrderId, orderBook);
            }
            else
            {
                // If orderId exists, get existing OrderBook
                if (orderBookCache.TryGetValue(order.OrderId, out orderBook))
                {
                    if (args.Action == Model.Action.Edit)
                    {
                        orderBook.UpdateOrder(order);
                    }
                    else if (args.Action == Model.Action.Remove)
                    {
                        orderBook.RemoveOrder(order.OrderId);
                        orderBookCache.Remove(order.OrderId);
                    }
                }
            }
        }

        public void FinishProcessing(object sender, EventArgs args)
        {
            foreach (var orderbook in exchange.Values)
            {
                orderbook.WriteBookToLog();
            }
        }
    }
}
