namespace PlanetWars.Models.MilitaryUnits
{
    public class SpaceForces : MilitaryUnit
    {
        private const double CostPerUnit = 11;

        public SpaceForces() 
            : base(CostPerUnit)
        {
        }
    }
}
