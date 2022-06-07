using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create_Custom_Data_Structures
{
    public class MyList
    {
        private const int INITIAL_CAPACITY = 4;
        private int[] data;

        public int Count { get; private set; }
        public int this[int index]
        {
            get { ValidatingIndex(index); return data[index]; }
            set { ValidatingIndex(index); data[index] = value; }
        }


        public MyList()
            : this(INITIAL_CAPACITY)
        {

        }

        public MyList(int capacity)
        {
            this.data = new int[capacity];
            this.Count = 0;
        }

        public void Add(int element)
        {
            if (this.Count == this.data.Length)
            {
                Resize();
            }

            data[this.Count] = element;
            this.Count++;
        }

        public void Insert(int index, int element)
        {
            ValidatingIndex(index);

            if (this.Count == this.data.Length)
            {
                Resize();
            }

            ShiftRight(index);
            this.data[index] = element;
            this.Count++;
        }

        public int RemoveAt(int index)
        {
            ValidatingIndex(index);
            int element = this.data[index];
            this.data[index] = default(int);
            Shift(index);
            this.Count--;

            if (this.Count <= this.data.Length / 2)
                Shrink();

            return element;
        }

        public bool Constains(int element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.data[i] == element)
                    return true;
            }

            return false;
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            ValidatingIndex(firstIndex);
            ValidatingIndex(secondIndex);

            int firstElemValue = this.data[firstIndex];
            this.data[firstIndex] = this.data[secondIndex];
            this.data[secondIndex] = firstElemValue;
        }

        public void Clear()
        {
            this.Count = 0;
            this.data = new int[INITIAL_CAPACITY];
        }

        public void ForEach(Action<int> action)
        {
            for (int i = 0; i < this.Count; i++)
            {
                action(this.data[i]);
            }
        }

        private void Shift(int index)
        {
            for (int i = index; i < this.Count - 1; i++)
            {
                this.data[i] = this.data[i + 1];
            }
        }

        private void ShiftRight(int index)
        {
            for (int i = this.Count; i > index; i--)
            {
                this.data[i] = this.data[i - 1];
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

        private void ValidatingIndex(int index)
        {
            if (index >= 0 && index < this.Count)
                return;

            throw new ArgumentOutOfRangeException();
        }
    }
}
