namespace P08.Collection_Hierarchy.Models
{
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class MyList<T> : IMyList<T>
    {
        private IList<T> data;

        public MyList()
        {
            this.data = new List<T>();
        }

        public int Used => this.data.Count;

        public int Add(T item)
        {
            this.data.Insert(0, item);

            return 0;
        }

        public T Remove()
        {
            T itemToRemove = this.data.FirstOrDefault();

            if(itemToRemove != null)
                this.data.Remove(itemToRemove);

            return itemToRemove;
        }
    }
}
