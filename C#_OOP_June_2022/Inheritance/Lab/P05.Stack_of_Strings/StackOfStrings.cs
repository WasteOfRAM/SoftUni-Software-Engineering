using System.Collections.Generic;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
        {
            return base.Count == 0;
        }

        public void AddRange(params string[] range)
        {
            foreach (var str in range)
            {
                base.Push(str);
            }
        }
    }
}
