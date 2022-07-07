using System;
using System.Collections.Generic;
using System.Linq;

namespace P04.Border_Control
{
    public class Program
    {
        static void Main()
        {
            var inhabitantsList = new List<IIdentifiable>();

            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split();

                if (inputArgs.Length == 3)
                {
                    inhabitantsList.Add(new Citizen(inputArgs[0], int.Parse(inputArgs[1]), inputArgs[2]));
                }
                else
                {
                    inhabitantsList.Add(new Robot(inputArgs[0], inputArgs[1]));
                }
            }

            string fakeIdIdentifier = Console.ReadLine();

            //var fakeIdsList = inhabitantsList.Where(inhabitant => inhabitant.FakeIdCheck(fakeIdIdentifier)).Select(id => id.Id).ToList();

            var fakeIdsList = inhabitantsList.Where(inhabitant => inhabitant.FakeIdCheck(fakeIdIdentifier)).ToList();

            foreach (var inhabitant in fakeIdsList)
            {
                Console.WriteLine(inhabitant.Id);
            }
        }
    }
}
