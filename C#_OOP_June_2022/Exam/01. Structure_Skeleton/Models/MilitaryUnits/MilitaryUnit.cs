namespace PlanetWars.Models.MilitaryUnits
{
    using Contracts;
    using PlanetWars.Utilities.Messages;
    using System;

    public abstract class MilitaryUnit : IMilitaryUnit
    {
        public MilitaryUnit(double cost)
        {
            this.Cost = cost;
            this.EnduranceLevel = 1;
        }

        public double Cost { get; private set; }

        public int EnduranceLevel { get; private set; }

        public void IncreaseEndurance()
        {
            this.EnduranceLevel++;

            if(this.EnduranceLevel > 20)
            {
                this.EnduranceLevel = 20;
                throw new ArgumentException(ExceptionMessages.EnduranceLevelExceeded);
            }
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
