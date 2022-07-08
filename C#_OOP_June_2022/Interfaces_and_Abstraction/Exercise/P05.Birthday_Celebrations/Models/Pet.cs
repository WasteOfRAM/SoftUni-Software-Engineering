namespace P05.Birthday_Celebrations.Models
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
