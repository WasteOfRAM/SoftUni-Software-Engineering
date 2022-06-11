using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VetClinic
{
    public class Clinic
    {
        private List<Pet> pets;

        public Clinic(int capacity)
        {
            this.Capacity = capacity;
            pets = new List<Pet>();
        }

        public int Capacity { get; private set; }
        public int Count { get { return this.pets.Count; } }

        public void Add(Pet pet)
        {
            if (pets.Count < this.Capacity)
                pets.Add(pet);
        }

        public bool Remove(string name)
        {
            Pet pet = pets.Find(p => p.Name == name);
            return pets.Remove(pet);
        }

        public Pet GetPet(string name, string owner)
        {
            return pets.Find(p => p.Name == name && p.Owner == owner);
        }

        public Pet GetOldestPet()
        {
            if (pets.Count == 0)
                return null;

            var tempPets = new List<Pet>(pets); 
            return tempPets.OrderByDescending(pet => pet.Age).First();
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("The clinic has the following patients:");

            foreach (Pet pet in pets)
            {
                sb.AppendLine($"Pet {pet.Name} with owner: {pet.Owner}");
            }

            return sb.ToString();
        }
    }
}
