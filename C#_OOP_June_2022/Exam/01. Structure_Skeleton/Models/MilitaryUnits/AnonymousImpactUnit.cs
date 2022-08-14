namespace PlanetWars.Models.MilitaryUnits
{
    public class AnonymousImpactUnit : MilitaryUnit
    {
        private const double CostPerUnit = 30;

        public AnonymousImpactUnit() 
            : base(CostPerUnit)
        {
        }
    }
}
