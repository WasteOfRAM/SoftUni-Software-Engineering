using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BakeryOpenning
{
    internal class Bakery
    {
        private List<Employee> employees;

        public Bakery(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            employees = new List<Employee>();
        }

        public string Name { get; set; }
        public int Capacity { get; private set; }
        public int Count { get => this.employees.Count; }

        public void Add(Employee employee)
        {
            if(employees.Count < this.Capacity)
                employees.Add(employee);
        }

        public bool Remove(string employeeName)
        {
            Employee employeeToRemove = employees.Find(e => e.Name == employeeName);

            return employees.Remove(employeeToRemove);
        }

        public Employee GetOldestEmployee()
        {
            var tempList = employees.OrderByDescending(e => e.Age);

            return tempList.FirstOrDefault();
        }

        public Employee GetEmployee(string name)
        {
            return employees.Find(e => e.Name == name);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Employees working at Bakery {Name}:");
            sb.AppendLine(string.Join(Environment.NewLine, employees));

            return sb.ToString();
        }
    }
}
