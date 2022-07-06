namespace P04.Pizza_Calories
{
    using System;
    using System.Collections.Generic;
    public class Dough
    {
        //Modifiers
        private Dictionary<string, double> flourModifiers = new Dictionary<string, double>
        {
            {"white", 1.5},
            {"wholegrain", 1.0}
        };

        private Dictionary<string, double> bakingModifiers = new Dictionary<string, double>
        {
            {"crispy", 0.9},
            {"chewy", 1.1},
            {"homemade", 1.0}
        };

        private string flourType;
        private string bakingTechnique;
        private int doughWeight;

        public Dough(string flourType, string bakingTechnique, int doughWeigh)
        {
            this.FlourType = flourType;
            this.BakingTechnique = bakingTechnique;
            this.DoughWeight = doughWeigh;
        }

        public string FlourType
        {
            get
            {
                return this.flourType;
            }
            private set
            {
                if (!flourModifiers.ContainsKey(value.ToLower()))
                    throw new ArgumentException("Invalid type of dough.");

                this.flourType = value;
            }
        }
        public string BakingTechnique
        {
            get
            {
                return this.bakingTechnique;
            }
            private set
            {
                if (!bakingModifiers.ContainsKey(value.ToLower()))
                    throw new ArgumentException("Invalid type of dough.");

                this.bakingTechnique = value;
            }
        }
        private int DoughWeight
        {
            get
            {
                return this.doughWeight;
            }
            set
            {
                if (value < 1 || value > 200)
                    throw new ArgumentException("Dough weight should be in the range [1..200].");

                this.doughWeight = value;
            }
        }

        public double CaloriesPegGram => 2 * this.DoughWeight * flourModifiers[this.FlourType.ToLower()] * bakingModifiers[this.BakingTechnique.ToLower()]; 
    }
}
