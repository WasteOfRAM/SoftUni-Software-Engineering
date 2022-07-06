using System;

namespace P04.Pizza_Calories
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                var pizzaInput = Console.ReadLine().Split();
                var doughInput = Console.ReadLine().Split();

                var dough = new Dough(doughInput[1], doughInput[2], int.Parse(doughInput[3]));
                var pizza = new Pizza(pizzaInput[1], dough);

                string input;
                while ((input = Console.ReadLine()) != "END")
                {
                    var inputSplit = input.Split();

                    var topping = new Topping(inputSplit[1], int.Parse(inputSplit[2]));

                    pizza.AddTopping(topping);
                }
                
                Console.WriteLine(pizza);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
