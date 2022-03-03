namespace OrderBookTest.Model
{
    public class Order
    {
        public Order(long orderId, string symbol, bool isBuy, decimal price, int quantity)
        {
            this.OrderId = orderId;
            this.Symbol = symbol;
            this.IsBuy = isBuy;
            this.Price = price;
            this.Quantity = quantity;
        }

        public long OrderId { get; }
        public string Symbol { get; }
        public bool IsBuy { get; }
        public decimal Price { get; }
        public int Quantity { get; }
    }
}
