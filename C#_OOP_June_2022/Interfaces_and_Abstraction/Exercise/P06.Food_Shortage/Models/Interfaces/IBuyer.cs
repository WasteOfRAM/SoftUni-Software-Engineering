namespace P06.Food_Shortage.Models.Interfaces
{
    public interface IBuyer : INameable
    {
        int Food { get; }

        void BuyFood();
    }
}
