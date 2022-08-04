namespace AquaShop.Models.Aquariums
{
    public class SaltwaterAquarium : Aquarium
    {
        private const int AquariumCapacity = 25;
        public SaltwaterAquarium(string name) 
            : base(name, AquariumCapacity)
        {
        }
    }
}
