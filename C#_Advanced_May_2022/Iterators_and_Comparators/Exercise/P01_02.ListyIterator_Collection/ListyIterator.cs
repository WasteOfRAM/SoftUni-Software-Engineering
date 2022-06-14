using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace P01_02.ListyIterator_Collection
{
    public class ListyIterator<T> : IEnumerable<T>
    {
        private List<T> list;
        private int currentIndex;

        public ListyIterator(params T[] list)
        {
            this.list = new List<T>(list);
            currentIndex = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < list.Count; i++)
            {
                yield return list[i];
            }
        }

        public bool HasNext() => currentIndex < list.Count - 1;

        public bool Move()
        {
            bool canMove = this.HasNext();
            if (canMove)
                currentIndex++;

            return canMove;
        }

        public void Print()
        {
            if (list.Count == 0)
                throw new ArgumentException("Invalid Operation!");

            Console.WriteLine(this.list[currentIndex]);
        }

        public void PrintAll()
        {
            Console.WriteLine(string.Join(" ", list));
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
