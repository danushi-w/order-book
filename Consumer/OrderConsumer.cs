namespace OrderBookTest.Consumer
{
    using System;
    using Model;
    using Interface;

    /// <summary>
    /// TODO: Fill in this class and any other required classes.
    /// </summary>
    public class OrderConsumer : IOrderConsumer
    {
        public void StartProcessing(object sender, ProcessingStartEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void HandleOrderAction(object sender, OrderActionEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void FinishProcessing(object sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
