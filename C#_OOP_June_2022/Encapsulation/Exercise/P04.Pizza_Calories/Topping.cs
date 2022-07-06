namespace P04.Pizza_Calories
{
    using System;
    using System.Collections.Generic;
    public class Topping
    {
        private Dictionary<string, double> modifiers = new Dictionary<string, double>
        {
            {"meat", 1.2},
            {"veggies", 0.8},
            {"cheese", 1.1},
            {"sauce", 0.9}
        };

        private string toppingType;
        private int toppingWeight;

        public Topping(string toppingType, int toppingWeight)
        {
            this.ToppingType = toppingType;
            this.ToppingWeight = toppingWeight;
        }

        public string ToppingType
        {
            get
            {
                return this.toppingType;
            }
            private set
            {
                if (!modifiers.ContainsKey(value.ToLower()))
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");

                this.toppingType = value;
            }
        }

        public int ToppingWeight
        {
            get
            {
                return this.toppingWeight;
            }
            private set
            {
                if (value < 1 || value > 50)
                    throw new ArgumentException($"{this.ToppingType} weight should be in the range [1..50].");

                this.toppingWeight = value;
            }
        }

        public double CaloriesPegGram => 2 * this.ToppingWeight * modifiers[this.ToppingType.ToLower()];
    }
}
