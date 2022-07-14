namespace P04.Wild_Farm.Factories.Interfaces
{
    using Models.Animals;
    public interface IAnimalFactory
    {
        Animal CreateAnimal(string type, string name, double weight, string thirdParam, string fourthParam = null);
    }
}
