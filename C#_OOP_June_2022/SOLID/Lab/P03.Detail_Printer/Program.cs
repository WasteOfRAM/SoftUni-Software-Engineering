using System;
using System.Collections.Generic;

namespace P03.DetailPrinter
{
    class Program
    {
        static void Main()
        {
            var employee = new Employee("Pesho");
            var manager = new Manager("Gosho", new string[] { "asdf", "asdawe23", "536fgh" });

            IList<IEmployee> employeeList = new List<IEmployee> { employee, manager };

            var printer = new DetailsPrinter(employeeList);

            printer.PrintDetails();
        }
    }
}
