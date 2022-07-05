using System;
using System.Linq;
using System.Collections.Generic;

namespace P03.Shopping_Spree
{
    public class Program
    {
        static void Main()
        {
            var people = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
            var products = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);

            var peopleList = new List<Person>();
            var productList = new List<Product>();

            try
            {
                foreach (var person in people)
                {
                    var tokens = person.Split("=");

                    var currentPerson = new Person(tokens[0], int.Parse(tokens[1]));
                    peopleList.Add(currentPerson);
                }

                foreach (var product in products)
                {
                    var tokens = product.Split("=");

                    productList.Add(new Product(tokens[0], int.Parse(tokens[1])));
                }

                string command;
                while ((command = Console.ReadLine()) != "END")
                {
                    var person = peopleList.FirstOrDefault(n => n.Name == command.Split()[0]);
                    var product = productList.FirstOrDefault(p => p.Name == command.Split()[1]);

                    if (person.BuyProduct(product))
                        Console.WriteLine($"{person.Name} bought {product.Name}");
                    else
                        Console.WriteLine($"{person.Name} can't afford {product.Name}");
                }

                foreach (var person in peopleList)
                {
                    Console.WriteLine(person.Bag.Count == 0 ? $"{person.Name} - Nothing bought" : $"{person.Name} - {string.Join(", ", person.Bag)}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
