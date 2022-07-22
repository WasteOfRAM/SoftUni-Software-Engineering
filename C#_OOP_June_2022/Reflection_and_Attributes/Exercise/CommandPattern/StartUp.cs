namespace CommandPattern
{
    using System;

    using Core;
    using Core.Contracts;
    using IO;
    using IO.Contracts;
    public class StartUp
    {
        public static void Main()
        {
            ICommandInterpreter command = new CommandInterpreter();
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(command, reader, writer);
            engine.Run();
        }
    }
}
