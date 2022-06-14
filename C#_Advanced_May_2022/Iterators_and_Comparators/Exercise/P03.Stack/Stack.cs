using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace P03.Stack
{
    public class Stack<T> : IEnumerable<T>
    {
        private List<T> stack;

        public Stack(params T[] elements)
        {
            this.stack = new List<T>(elements);
        }

        public void Push(params T[] elements)
        {
            foreach (var item in elements)
            {
                this.stack.Add(item);
            }
        }

        public void Pop()
        {
            if (stack.Count == 0)
                throw new ArgumentException("No elements");
            else
                this.stack.RemoveAt(stack.Count - 1);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.stack.Count - 1; i >= 0; i--)
            {
                yield return stack[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
