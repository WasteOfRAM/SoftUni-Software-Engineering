using System;
using System.Collections.Generic;

namespace PersonsInfo
{
    public class StartUp
    {
        static void Main()
        {
            var personOne = new Person("werwesr", "asfefas", 30, 45545);
            var personTwo = new Person("werwasdesr", "asfefaasds", 310, 455245);

            var persons = new List<Person>();
            persons.Add(personOne);
            persons.Add(personTwo);

            Team team = new Team("SoftUni");

            foreach (Person person in persons)
            {
                team.AddPlayer(person);
            }
        }
    }
}
