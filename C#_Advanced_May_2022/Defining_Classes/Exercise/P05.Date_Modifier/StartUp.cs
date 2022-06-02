using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main()
        {
            string dateOneInput = Console.ReadLine();
            string dateTwoInput = Console.ReadLine();

            int dateOneYear = int.Parse(dateOneInput.Split()[0]);
            int dateOneMonth = int.Parse(dateOneInput.Split()[1]);
            int dateOneDay = int.Parse(dateOneInput.Split()[2]);

            int dateTwoYear = int.Parse(dateTwoInput.Split()[0]);
            int dateTwoMounth = int.Parse(dateTwoInput.Split()[1]);
            int dateTwoDay = int.Parse(dateTwoInput.Split()[2]);

            DateTime dateOne = new DateTime(dateOneYear, dateOneMonth, dateOneDay);
            DateTime dateTwo = new DateTime(dateTwoYear, dateTwoMounth, dateTwoDay);

            DateModifier dateModifier = new DateModifier(dateOne, dateTwo);

            Console.WriteLine(dateModifier.DateDifference());
        }
    }
}
