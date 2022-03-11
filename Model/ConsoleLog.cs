using System;
using System.Data;
using OrderBookTest.Interface;

namespace OrderBookTest.Model
{
    public class ConsoleLog : ILog
    {
        void ILog.Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
