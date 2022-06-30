using System;

namespace CustomStack
{
    public class StartUp
    {
        static void Main()
        {
            var stack = new StackOfStrings();

            stack.AddRange(new string[] {"a", "b", "c"});
        }
    }
}
