namespace OrderBookTest
{
    using System;
    using Model;
    using Interface;

    using Action = Model.Action;

    public class AppEnvironment : IAppEnvironment
    {
        public event EventHandler<OrderActionEventArgs> OrderActionEvent;
        public event EventHandler<ProcessingStartEventArgs> ProcessingStartEvent;
        public event EventHandler ProcessingFinishEvent;

        private readonly ILog log;

        public void Run()
        {
            OnProcessingStart(new ProcessingStartEventArgs(log));
            FeedOrders();
            OnProcessingFinish();
        }

        private void FeedOrders()
        {
            Command[] commands =
            {
                new Command(Action.Add, 1L, "MSFT.L", true, 5, 200),
                new Command(Action.Add, 2L, "VOD.L", true, 15, 100),
                new Command(Action.Add, 3L, "MSFT.L", false, 5, 300),
                new Command(Action.Add, 4L, "MSFT.L", true, 7, 150),
                new Command(Action.Remove, 1L, null, true, -1, -1),
                new Command(Action.Add, 5L, "VOD.L", false, 17, 300),
                new Command(Action.Add, 6L, "VOD.L", true, 12, 150),
                new Command(Action.Edit, 3L, null, true, 7, 200),
                new Command(Action.Add, 7L, "VOD.L", false, 16, 100),
                new Command(Action.Add, 8L, "VOD.L", false, 19, 100),
                new Command(Action.Add, 9L, "VOD.L", false, 21, 112),
                new Command(Action.Remove, 5L, null, false, -1, -1)
            };

            foreach (var command in commands)
            {
                OnOrderAction(new OrderActionEventArgs(command.Action, command.Order));
            }
        }

        private void OnProcessingStart(ProcessingStartEventArgs args)
        {
            this.ProcessingStartEvent?.Invoke(this, args);
        }

        private void OnProcessingFinish()
        {
            this.ProcessingFinishEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OnOrderAction(OrderActionEventArgs args)
        {
            this.OrderActionEvent?.Invoke(this, args);
        }

        private class Command
        {
            public Action Action { get; }
            public Order Order { get; }

            public Command(Action action, long orderId, string symbol, bool isBuy, decimal price, int quantity)
            {
                this.Action = action;
                this.Order = new Order(orderId, symbol, isBuy, price, quantity);
            }
        }
    }
}
