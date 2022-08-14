namespace PlanetWars.Models.MilitaryUnits
{
    public class StormTroopers : MilitaryUnit
    {
        private const double CostPerUnit = 2.5;

        public StormTroopers() 
            : base(CostPerUnit)
        {
        }
    }
}
