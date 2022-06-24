using System.Collections.Generic;
using System.Linq;

namespace Zoo
{
    public class Zoo
    {
        private List<Animal> animals;
        private string name;
        private int capacity;

        public Zoo(string name, int capacity)
        {
            this.name = name;
            this.capacity = capacity;
            this.animals = new List<Animal>();
        }

        public List<Animal> Animals { get => this.animals; }
        public string Name { get => this.name; set => this.name = value; }
        public int Capacity { get => this.capacity; set => this.capacity = value; }

        public string AddAnimal(Animal animal)
        {
            if (string.IsNullOrWhiteSpace(animal.Species))
                return "Invalid animal species.";

            if(animal.Diet != "herbivore" && animal.Diet != "carnivore")
                return "Invalid animal diet.";

            if (this.animals.Count >= this.capacity)
                return "The zoo is full.";

            this.animals.Add(animal);
            return $"Successfully added {animal.Species} to the zoo.";
        }

        public int RemoveAnimals(string species)
        {
            return this.animals.RemoveAll(animal => animal.Species == species);
        }

        public List<Animal> GetAnimalsByDiet(string diet)
        {
            return this.animals.Where(animals => animals.Diet == diet).ToList();
        }

        public Animal GetAnimalByWeight(double weight)
        {
            return this.animals.FirstOrDefault(animals => animals.Weight == weight);
        }

        public string GetAnimalCountByLength(double minimumLength, double maximumLength)
        {
            List<Animal> animalsBy = this.animals.Where(animal => animal.Length >= minimumLength && animal.Length <= maximumLength).ToList();

            return $"There are {animalsBy.Count} animals with a length between {minimumLength} and {maximumLength} meters.";
        }
    }
}
