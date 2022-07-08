namespace P06.Food_Shortage.Models
{
    using System;
    using Interfaces;

    public class Citizen : INameable, IIdentifiable, IBirthable, IBuyer
    {
        public Citizen(string name, int age, string id, DateTime birthDate)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
            this.BirthDate = birthDate;
            this.Food = 0;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Id { get; private set; }

        public DateTime BirthDate {get; private set; }

        public int Food { get; private set; }

        public void BuyFood()
        {
            this.Food += 10;
        }
    }
}
