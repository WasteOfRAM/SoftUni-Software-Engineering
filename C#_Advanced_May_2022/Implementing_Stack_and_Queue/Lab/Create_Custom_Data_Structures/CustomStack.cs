using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create_Custom_Data_Structures
{
    internal class CustomStack
    {
        private const int INITIAL_CAPACITY = 4;
        private int[] data;

        public int Count { get; private set; }

        public CustomStack()
            :this(INITIAL_CAPACITY)
        {

        }

        public CustomStack(int size)
        {
            this.data = new int[size];
            this.Count = 0;
        }

        public void Push(int element)
        {
            if (this.Count == this.data.Length)
                Resize();

            this.data[this.Count++] = element;
        }

        public int Pop()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("CustomStack is empty");

            int lastElement = this.data[this.Count - 1];
            this.Count--;

            if (this.Count <= this.data.Length / 2)
                Shrink();

            return lastElement;
        }

        public int Peek()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("CustomStack is empty");

            int lastElement = this.data[this.Count - 1];

            return lastElement;
        }

        public void ForEach(Action<object> action)
        {
            for (int i = 0; i < this.Count; i++)
            {
                action(this.data[i]);
            }
        }

        private void Resize() => this.data = ChageArraySize("*");
        private void Shrink() => this.data = ChageArraySize("/");

        private int[] ChageArraySize(string operation)
        {
            int loops;
            int newSize = this.data.Length;

            if (operation == "*")
            {
                newSize = this.data.Length * 2;
                loops = this.data.Length;
            }
            else
            {
                if (this.data.Length / 2 > 2)
                    newSize = this.data.Length / 2;

                loops = newSize;
            }

            var newData = new int[newSize];

            for (int i = 0; i < loops; i++)
            {
                newData[i] = this.data[i];
            }

            return newData;
        }
    }
}
