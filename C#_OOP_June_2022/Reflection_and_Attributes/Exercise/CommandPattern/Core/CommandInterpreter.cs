namespace CommandPattern.Core
{
    using System;
    using System.Linq;
    using Contracts;
    using System.Reflection;

    public class CommandInterpreter : ICommandInterpreter
    {
        public string Read(string args)
        {
            string[] cmdSplit = args.Split();
            string cmdName = cmdSplit[0];
            string[] cmdArgs = cmdSplit.Skip(1).ToArray();

            Assembly assembly = Assembly.GetCallingAssembly();
            Type cmdType = assembly?.GetTypes().FirstOrDefault(t => t.Name == $"{cmdName}Command" && t.GetInterfaces().Any(i => i == typeof(ICommand)));

            if (cmdType == null)
                throw new InvalidOperationException($"Invalid command type: {cmdName}");

            var classInstance = Activator.CreateInstance(cmdType);

            MethodInfo executeMethod = cmdType.GetMethod("Execute");

            string result = executeMethod.Invoke(classInstance, new object[] { cmdArgs }).ToString();

            return result;
        }
    }
}
