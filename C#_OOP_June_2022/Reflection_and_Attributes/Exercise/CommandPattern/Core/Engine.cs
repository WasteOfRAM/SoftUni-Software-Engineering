namespace CommandPattern.Core
{
    using System;
    using IO.Contracts;
    using Contracts;

    public class Engine : IEngine
    {
        private readonly ICommandInterpreter interpreter;
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(ICommandInterpreter interpreter, IReader reader, IWriter writer)
        {
            this.interpreter = interpreter;
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            while (true)
            {
                string command = this.reader.ReadLine();

                string result = this.interpreter.Read(command);

                this.writer.WriteLine(result); 
            }
        }
    }
}
