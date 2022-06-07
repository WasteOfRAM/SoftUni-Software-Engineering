using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create_Custom_Data_Structures
{
    public class CustomQueue
    {
        private const int INITIAL_CAPACITY = 4;
        private int[] data;

        public int Count { get; private set; }

        public CustomQueue()
            :this(INITIAL_CAPACITY)
        {

        }

        public CustomQueue(int size)
        {
            this.data = new int[size];
            this.Count = 0;
        }

        public void Enqueue(int element)
        {
            if (this.Count == this.data.Length)
                Resize();

            this.data[this.Count++] = element;
        }

        public int Dequeue()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            int element = this.data[0];

            for (int i = 0; i < this.Count; i++)
            {
                this.data[i] = this.data[i + 1];
            }

            this.Count--;

            if (this.Count < this.data.Length)
                Shrink();

            return element;
        }

        public int Peek()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("Queue is empty");

            return this.data[0];
        }

        public void Clear()
        {
            this.data = new int[INITIAL_CAPACITY];
            this.Count = 0;
        }

        public void ForEach(Action<int> action)
        {
            for (int i = 0; i < this.data.Length; i++)
            {
                action(this.data.Length);
            }
        }

        private void Shrink()
        {
            if (this.data.Length / 2 < 2)
                return;

            int newSize = this.data.Length / 2;
            int[] newData = new int[newSize];

            for (int i = 0; i < newSize; i++)
            {
                newData[i] = this.data[i];
            }

            this.data = newData;
        }

        private void Resize()
        {
            int newSize = this.data.Length * 2;
            int[] newData = new int[newSize];

            for (int i = 0; i < this.data.Length; i++)
            {
                newData[i] = this.data[i];
            }

            this.data = newData;
        }
    }
}
