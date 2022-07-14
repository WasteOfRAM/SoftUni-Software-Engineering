namespace P04.Wild_Farm.Core
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using IO.Interfaces;
    using Models.Animals;
    using Factories;
    using Factories.Interfaces;
    using Models.Foods;
    using Exeptions;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICollection<Animal> animals;
        private readonly IFoodFactory foodFactory;
        private readonly IAnimalFactory animalFactory;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.animals = new List<Animal>();
            this.foodFactory = new FoodFactory();
            this.animalFactory = new AnimalFactory();
        }

        public void Start()
        {
            string command;
            while ((command = this.reader.ReadLine()) != "End")
            {
                string[] animalData = command.Split();
                string[] foodData = this.reader.ReadLine().Split();

                Animal animal;
                Food food;

                try
                {
                    animal = BuildAnimal(animalData);
                    food = this.foodFactory.CreateFood(foodData[0], int.Parse(foodData[1]));

                    this.writer.WriteLine(animal.ProduceSound());
                    this.animals.Add(animal);

                    animal.EatFood(food);
                }
                catch (InvalidAnimalTypeExeption iate)
                {
                    this.writer.WriteLine(iate.Message);
                }
                catch (InvalidFoodTypeExeption ifte)
                {
                    this.writer.WriteLine(ifte.Message);
                }
                catch (WrongFoodTypeExeption wfte)
                {
                    this.writer.WriteLine(wfte.Message);
                }

            }

            foreach (var currAnimal in this.animals)
            {
                this.writer.WriteLine(currAnimal.ToString());
            }
        }

        private Animal BuildAnimal(string[] animalData)
        {
            Animal animal;

            if (animalData.Length == 4)
            {
                string type = animalData[0];
                string name = animalData[1];
                double weight = double.Parse(animalData[2]);
                string thirdParam = animalData[3];

                animal = this.animalFactory.CreateAnimal(type, name, weight, thirdParam);
            }
            else if (animalData.Length == 5)
            {
                string type = animalData[0];
                string name = animalData[1];
                double weight = double.Parse(animalData[2]);
                string thirdParam = animalData[3];
                string fourthParam = animalData[4];

                animal = this.animalFactory.CreateAnimal(type, name, weight, thirdParam, fourthParam);
            }
            else
            {
                throw new ArgumentException("Invalid input");
            }

            return animal;
        }
    }
}
