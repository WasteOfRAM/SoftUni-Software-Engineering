namespace PlanetWars.Models.Weapons
{
    public class BioChemicalWeapon : Weapon
    {
        private const double PriceOfWeapon = 3.2;

        public BioChemicalWeapon(int destructionLevel) 
            : base(destructionLevel, PriceOfWeapon)
        {
        }
    }
}
