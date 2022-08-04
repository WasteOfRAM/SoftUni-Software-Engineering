namespace AquaShop.Models.Aquariums
{
    public class FreshwaterAquarium : Aquarium
    {
        private const int AquariumCapacity = 50;

        public FreshwaterAquarium(string name) 
            : base(name, AquariumCapacity)
        {
        }
    }
}
