namespace P05.Birthday_Celebrations.Models
{
    using System;
    using Interfaces;

    public class Citizen : IIdentifiable, IBirthable
    {
        public Citizen(string name, int age, string id, DateTime birthDate)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
            this.BirthDate = birthDate;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Id { get; private set; }

        public DateTime BirthDate {get; private set; }
    }
}
