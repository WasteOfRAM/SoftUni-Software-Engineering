namespace P04.Wild_Farm.Models.Animals.Birds
{
    using System;
    using System.Collections.Generic;
    using Models.Foods;
    public class Hen : Bird
    {
        private const double HenWeightMultiplier = 0.35;
        public Hen(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        protected override IReadOnlyCollection<Type> Eats => new List<Type> { typeof(Fruit), typeof(Meat), typeof(Seeds), typeof(Vegetable) }.AsReadOnly();

        protected override double WeightMultiplier => HenWeightMultiplier;

        public override string ProduceSound()
        {
            return "Cluck";
        }
    }
}
