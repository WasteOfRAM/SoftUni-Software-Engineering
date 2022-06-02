using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClasses
{
    public class Family
    {
        public List<Person> familyMembers;

        public Family()
        {
            familyMembers = new List<Person>();
        }

        public void AddMember(Person member)
        {
            familyMembers.Add(member);
        }
        
        public Person GetOldestMember()
        {
            Person result = new Person();

            if(familyMembers.Count > 0)
                result = familyMembers.OrderByDescending(p => p.Age).First();

            return result;
        }
    }
}
