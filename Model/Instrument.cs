using System;
namespace OrderBookTest.Model
{
    public class Instrument
    {
        public String Symbol { get; private set; }

        public Instrument(String symbol)
        {
            if (String.IsNullOrEmpty(symbol))
                throw new ArgumentNullException("symbol");

            Symbol = symbol;
        }
    }
}