namespace P04.Pizza_Calories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Pizza
    {
        private string name;
        private Dough dough;
        private List<Topping> toppings;

        public Pizza(string name, Dough dough)
        {
            this.Name = name;
            this.Dough = dough;
            toppings = new List<Topping>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if(value.Length < 1 || value.Length > 15)
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");

                this.name = value;
            }
        }

        public Dough Dough
        {
            get
            {
                return this.dough;
            }
            private set
            {
                this.dough = value;
            }
        }

        public int ToppingsCount
        {
            get
            {
                return toppings.Count;
            }
        }

        public double Calories => this.dough.CaloriesPegGram + this.toppings.Sum(t => t.CaloriesPegGram);

        public void AddTopping(Topping topping)
        {
            if (this.ToppingsCount == 10)
                throw new ArgumentException("Number of toppings should be in range [0..10].");

            this.toppings.Add(topping);
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.Calories:f2} Calories.";
        }
    }
}
