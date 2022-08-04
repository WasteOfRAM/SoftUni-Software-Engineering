namespace AquaShop.Models.Decorations
{
    public class Plant : Decoration
    {
        private const int comfort = 5;
        private const decimal price = 10.0m;

        public Plant() 
            : base(comfort, price)
        {
        }
    }
}
