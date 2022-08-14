namespace PlanetWars.Models.Weapons
{
    public class NuclearWeapon : Weapon
    {
        private const double PriceOfWeapon = 15;

        public NuclearWeapon(int destructionLevel) 
            : base(destructionLevel, PriceOfWeapon)
        {
        }
    }
}
