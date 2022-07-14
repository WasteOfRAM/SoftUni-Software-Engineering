namespace P04.Wild_Farm.IO
{
    using System;
    using Interfaces;
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
