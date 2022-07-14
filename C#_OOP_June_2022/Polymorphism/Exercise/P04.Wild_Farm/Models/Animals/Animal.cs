namespace P04.Wild_Farm.Models.Animals
{
    using Models.Foods;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Exeptions;

    public abstract class Animal
    {
        public Animal(string name, double weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        public string Name { get; }
        public double Weight { get; private set; }
        public int FoodEaten { get; private set; }

        protected abstract IReadOnlyCollection<Type> Eats { get; }

        protected abstract double WeightMultiplier { get; }

        public abstract string ProduceSound();

        public virtual void EatFood(Food food)
        {
            if(!this.Eats.Contains(food.GetType()))
                throw new WrongFoodTypeExeption(string.Format(ExeptionMessages.WrongFood, this.GetType().Name, food.GetType().Name));

            this.FoodEaten += food.Quantity;
            this.Weight += this.WeightMultiplier * food.Quantity;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} [{this.Name}, ";
        }
    }
}
