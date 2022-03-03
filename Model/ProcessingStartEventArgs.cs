namespace OrderBookTest.Model
{
    using System;
    using Interface;

    public class ProcessingStartEventArgs : EventArgs
    {
        public ILog Log { get; }

        public ProcessingStartEventArgs(ILog log) => this.Log = log;
    }
}
