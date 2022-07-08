namespace P08.Collection_Hierarchy
{
    using System;
    using Models;
    using Models.Interfaces;
    public class Program
    {
        static void Main()
        {
            var words = Console.ReadLine().Split();
            int removeOperations = int.Parse(Console.ReadLine());

            IAddCollection<string> addCollection = new AddCollection<string>();
            IAddRemoveCollection<string> addRemoveCollection = new AddRemoveCollection<string>();
            IMyList<string> myList = new MyList<string>();

            AddToCollection(words, addCollection);
            AddToCollection(words, addRemoveCollection);
            AddToCollection(words, myList);

            RemoveFromCollection(removeOperations, addRemoveCollection);
            RemoveFromCollection(removeOperations, myList);
        }

        private static void RemoveFromCollection(int removeOperations, IAddRemoveCollection<string> addRemoveCollection)
        {
            for (int i = 0; i < removeOperations; i++)
            {
                string removedStr = addRemoveCollection.Remove();

                Console.Write(removedStr + " ");
            }
            Console.WriteLine();
        }

        private static void AddToCollection(string[] words, IAddCollection<string> addCollection)
        {
            foreach (var item in words)
            {
                Console.Write(addCollection.Add(item) + " ");
            }
            Console.WriteLine();
        }
    }
}
