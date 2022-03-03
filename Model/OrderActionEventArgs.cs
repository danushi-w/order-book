namespace OrderBookTest.Model
{
    using System;

    public class OrderActionEventArgs : EventArgs
    {
        public Action Action { get; }
        public Order Order { get; }

        public OrderActionEventArgs(Action action, Order order)
        {
            this.Action = action;
            this.Order = order;
        }
    }
}
