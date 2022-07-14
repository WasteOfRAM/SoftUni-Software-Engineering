namespace P04.Wild_Farm.Models.Animals.Mammals
{
    using System;
    using System.Collections.Generic;
    using Models.Foods;

    public class Cat : Feline
    {
        private const double CatWeightMultiplier = 0.30;

        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed)
        {
        }

        protected override IReadOnlyCollection<Type> Eats => new List<Type> { typeof(Vegetable), typeof(Meat) }.AsReadOnly();

        protected override double WeightMultiplier => CatWeightMultiplier;

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
