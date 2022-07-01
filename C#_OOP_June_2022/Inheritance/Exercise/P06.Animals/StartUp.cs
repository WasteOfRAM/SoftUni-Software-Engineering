namespace Animals
{
    using System;
    using System.Collections.Generic;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var animals = new List<Animal>();

            Animal animal = null;

            string animalType;
            while ((animalType = Console.ReadLine()) != "Beast!")
            {
                string[] info = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string name = info[0];
                int age = int.Parse(info[1]);

                try
                {
                    if (animalType == "Dog")
                        animal = new Dog(name, age, info[2]);
                    else if (animalType == "Frog")
                        animal = new Frog(name, age, info[2]);
                    else if (animalType == "Cat")
                        animal = new Cat(name, age, info[2]);
                    else if (animalType == "Tomcat")
                        animal = new Tomcat(name, age);
                    else if (animalType == "Kitten")
                        animal = new Kitten(name, age);
                    else
                        throw new ArgumentException("Invalid input!");

                    animals.Add(animal);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var currAnimal in animals)
            {
                Console.WriteLine(currAnimal);
            }
        }
    }
}
