using System;

namespace BoxOfT
{
    public class Box<T>
    {
        private T[] data;
        private const int INITIAL_SIZE = 4;

        public Box()
        {
            data = new T[INITIAL_SIZE];
            this.Count = 0;
        }
        public int Count { get; private set; }

        public void Add(T element)
        {
            if (this.data.Length == this.Count)
                Resize();

            data[this.Count++] = element;
        }

        public T Remove()
        {
            int index = this.Count - 1;
            ValidateIndex(index);

            T result = this.data[this.Count - 1];
            this.data[this.Count - 1] = default;
            this.Count--;

            return result;
        }

        private void ValidateIndex(int index)
        {
            if (index >= 0 && index < this.Count)
                return;

            throw new ArgumentOutOfRangeException();
        }

        private void Resize()
        {
            T[] newData = new T[this.data.Length * 2];

            for (int i = 0; i < this.data.Length; i++)
            {
                newData[i] = this.data[i];
            }

            this.data = newData;
        }
    }
}
