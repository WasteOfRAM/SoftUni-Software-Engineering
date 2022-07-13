namespace P03.Raiding
{
    using System;
    using Core;
    using Core.Interfaces;
    using IO;
    using IO.Interfaces;
    public class StartUp
    {
        static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader, writer);
            engine.Start();
        }
    }
}
