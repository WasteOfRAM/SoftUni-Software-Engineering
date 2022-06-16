using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CocktailParty
{
    public class Cocktail
    {
        private List<Ingredient> ingredients;

        public Cocktail(string name, int capacity, int maxAlcoholLevel)
        {
            Name = name;
            Capacity = capacity;
            MaxAlcoholLevel = maxAlcoholLevel;
            ingredients = new List<Ingredient>();
        }

        public List<Ingredient> Ingredients { get => this.ingredients; set => this.ingredients = value; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int MaxAlcoholLevel { get; set; }

        public int CurrentAlcoholLevel { get => this.ingredients.Sum(al => al.Alcohol);}

        public void Add(Ingredient ingredient)
        {
            if(!ingredients.Any(ing => ing.Name == ingredient.Name) && this.ingredients.Count < this.Capacity)
                this.ingredients.Add(ingredient);
        }

        public bool Remove(string name)
        {
            Ingredient toRemove = this.ingredients.Find(i => i.Name == name);

            return this.ingredients.Remove(toRemove);
        }

        public Ingredient FindIngredient(string name)
        {
            return this.ingredients.FirstOrDefault(i => i.Name == name);
        }

        public Ingredient GetMostAlcoholicIngredient()
        {
            return this.ingredients.OrderByDescending(ing => ing.Alcohol).FirstOrDefault();
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Cocktail: {this.Name} - Current Alcohol Level: {this.CurrentAlcoholLevel}")
                .Append(string.Join(Environment.NewLine, this.ingredients));

            return sb.ToString();
        }
    }
}
