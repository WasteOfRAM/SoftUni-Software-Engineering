namespace P06.Food_Shortage.Models
{
    using Interfaces;
    using System;

    public class Pet : IBirthable
    {
        public Pet(string name, DateTime birthDate)
        {
            this.Name = name;
            this.BirthDate = birthDate;
        }

        public string Name { get; private set; }
        public DateTime BirthDate {get; private set;}
    }
}
