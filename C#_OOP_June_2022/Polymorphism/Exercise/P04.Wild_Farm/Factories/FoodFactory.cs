namespace P04.Wild_Farm.Factories
{
    using Interfaces;
    using Models.Foods;
    using Exeptions;

    public class FoodFactory : IFoodFactory
    {
        public Food CreateFood(string type, int quantity)
        {
            Food food;

            if (type == "Vegetable")
                food = new Vegetable(quantity);
            else if (type == "Fruit")
                food = new Fruit(quantity);
            else if (type == "Meat")
                food = new Meat(quantity);
            else if (type == "Seeds")
                food = new Seeds(quantity);
            else
                throw new InvalidFoodTypeExeption(ExeptionMessages.InvalidFoodType);

            return food;
        }
    }
}
