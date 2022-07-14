namespace P04.Wild_Farm.Models.Animals.Mammals
{
    using System;
    using System.Collections.Generic;
    using Models.Foods;

    public class Dog : Mammal
    {
        private const double DogWeightMultiplier = 0.40;

        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }

        protected override IReadOnlyCollection<Type> Eats => new List<Type> { typeof(Meat) }.AsReadOnly();

        protected override double WeightMultiplier => DogWeightMultiplier;

        public override string ProduceSound()
        {
            return "Woof!";
        }

        public override string ToString()
        {
            return base.ToString() + $"{this.Weight}, {this.LivingRegion}, {this.FoodEaten}]";
        }
    }
}
