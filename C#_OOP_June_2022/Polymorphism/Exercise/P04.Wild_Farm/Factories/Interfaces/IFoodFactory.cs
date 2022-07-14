namespace P04.Wild_Farm.Factories.Interfaces
{
    using Models.Foods;
    public interface IFoodFactory
    {
        Food CreateFood(string type, int quantity);
    }
}
