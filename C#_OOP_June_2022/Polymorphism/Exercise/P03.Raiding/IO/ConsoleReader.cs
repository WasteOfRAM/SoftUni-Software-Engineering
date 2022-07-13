namespace P03.Raiding.IO
{
    using System;

    using Interfaces;
    public class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            string text = Console.ReadLine();

            return text;
        }
    }
}
