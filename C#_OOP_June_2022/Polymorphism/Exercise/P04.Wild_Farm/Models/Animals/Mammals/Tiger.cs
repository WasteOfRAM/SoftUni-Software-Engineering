namespace P04.Wild_Farm.Models.Animals.Mammals
{
    using System;
    using System.Collections.Generic;
    using Models.Foods;

    public class Tiger : Feline
    {
        private const double TigerWeightMultiplier = 1.00;

        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }

        protected override IReadOnlyCollection<Type> Eats => new List<Type> { typeof(Meat) }.AsReadOnly();

        protected override double WeightMultiplier => TigerWeightMultiplier;

        public override string ProduceSound()
        {
            return "ROAR!!!";
        }
    }
}
