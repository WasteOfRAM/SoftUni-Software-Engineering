using System;
using System.Collections.Generic;
using System.Linq;

namespace P04.Product_Shop
{
    internal class Program
    {
        static void Main()
        {
            var stores = new Dictionary<string, Dictionary<string, double>>();

            string input;
            while ((input = Console.ReadLine()) != "Revision")
            {
                string storeName = input.Split(", ")[0];
                string product = input.Split(", ")[1];
                double productPrice = double.Parse(input.Split(",")[2]);

                if(!stores.ContainsKey(storeName))
                    stores[storeName] = new Dictionary<string, double>();

                if(!stores[storeName].ContainsKey(product))
                    stores[storeName][product] = productPrice;

                stores[storeName][product] = productPrice;
            }

            foreach (var store in stores.OrderBy(s => s.Key))
            {
                Console.WriteLine(store.Key + "->");
                foreach (var product in store.Value)
                {
                    Console.WriteLine($"Product: {product.Key}, Price: {product.Value}");
                }
            }
        }
    }
}
