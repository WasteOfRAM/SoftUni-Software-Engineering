using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClasses
{
    public class Family
    {
        public List<Person> familyMembers;

        public List<Person> FamilyMembers { get => this.familyMembers; set => this.familyMembers = value; }

        public Family()
        {
            familyMembers = new List<Person>();
        }

        public void AddMember(Person member)
        {
            familyMembers.Add(member);
        }
        public void AddMember(string name, int age)
        {
            familyMembers.Add(new Person(name, age));
        }

        public Person GetOldestMember()
        {
            var result = familyMembers.Any() ? familyMembers.Aggregate((p1, p2) => p1.Age > p2.Age ? p1 : p2) : null;

            return result;
        }
    }
}
