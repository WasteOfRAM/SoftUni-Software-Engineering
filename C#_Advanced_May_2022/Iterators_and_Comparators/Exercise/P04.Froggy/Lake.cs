using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace P04.Froggy
{
    public class Lake : IEnumerable<int>
    {
        private int[] data;

        public Lake(params int[] elements)
        {
            this.data = elements;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if(i % 2 == 0)
                    yield return data[i];
            }

            for (int i = data.Length - 1; i >= 0; i--)
            {
                if (i % 2 != 0)
                    yield return data[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
