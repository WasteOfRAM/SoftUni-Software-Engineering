namespace P04.Wild_Farm.Factories
{
    using Interfaces;
    using Models.Animals;
    using Models.Animals.Birds;
    using Models.Animals.Mammals;
    using Exeptions;

    public class AnimalFactory : IAnimalFactory
    {
        public Animal CreateAnimal(string type, string name, double weight, string thirdParam, string fourthParam = null)
        {
            Animal animal;

            if (type == "Hen")
                animal = new Hen(name, weight, double.Parse(thirdParam));
            else if (type == "Owl")
                animal = new Owl(name, weight, double.Parse(thirdParam));
            else if (type == "Mouse")
                animal = new Mouse(name, weight, thirdParam);
            else if (type == "Cat")
                animal = new Cat(name, weight, thirdParam, fourthParam);
            else if (type == "Dog")
                animal = new Dog(name, weight, thirdParam);
            else if (type == "Tiger")
                animal = new Tiger(name, weight, thirdParam, fourthParam);
            else
                throw new InvalidAnimalTypeExeption(ExeptionMessages.InvalidAnimalType);

            return animal;
        }
    }
}
