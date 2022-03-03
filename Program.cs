namespace OrderBookTest
{
    using Consumer;

    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = new AppEnvironment();
            var consumer = new OrderConsumer();

            environment.ProcessingStartEvent += consumer.StartProcessing;
            environment.ProcessingFinishEvent += consumer.FinishProcessing;
            environment.OrderActionEvent += consumer.HandleOrderAction;

            environment.Run();
        }
    }
}
