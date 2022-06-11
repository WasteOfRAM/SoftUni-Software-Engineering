using System;
using System.Collections.Generic;
using System.Text;

namespace VetClinic
{
    public class Pet
    {
        public Pet(string neme, int age, string owner)
        {
            this.Name = neme;
            this.Age = age;
            this.Owner = owner;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Owner { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} Age: {Age} Owner: {Owner}";
            //return $"{Name} {Age} ({Owner})";
        }
    }
}
