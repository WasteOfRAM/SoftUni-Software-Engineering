namespace CommandPattern.Core.Commands
{
    using System;

    using Contracts;
    public class ExitCommand : ICommand
    {
        private const int NormalExit = 0;
        public string Execute(string[] args)
        {
            Environment.Exit(NormalExit);

            return null;
        }
    }
}
