namespace P08.Collection_Hierarchy.Models
{
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class AddRemoveCollection<T> : IAddRemoveCollection<T>
    {
        private IList<T> data;

        public AddRemoveCollection()
        {
            this.data = new List<T>();
        }

        public int Add(T item)
        {
            this.data.Insert(0, item);

            return 0;
        }

        public T Remove()
        {
            T itemToRemove = this.data.LastOrDefault();

            if(itemToRemove != null)
                this.data.Remove(itemToRemove);

            return itemToRemove;
        }
    }
}
