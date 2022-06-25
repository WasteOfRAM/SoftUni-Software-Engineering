using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renovators
{
    public class Catalog
    {
        private List<Renovator> renovators;

        public Catalog(string name, int neededRenovators, string project)
        {
            Name = name;
            NeededRenovators = neededRenovators;
            Project = project;

            this.renovators = new List<Renovator>();
        }

        public string Name { get; set; }
        public int NeededRenovators { get; set; }
        public string Project { get; set; }
        public int Count { get => this.renovators.Count;}

        public string AddRenovator(Renovator renovator)
        {
            if (string.IsNullOrEmpty(renovator.Name) || string.IsNullOrEmpty(renovator.Type))
                return "Invalid renovator's information.";

            if (this.renovators.Count >= this.NeededRenovators)
                return "Renovators are no more needed.";

            if (renovator.Rate > 350)
                return "Invalid renovator's rate.";

            this.renovators.Add(renovator);
            return $"Successfully added {renovator.Name} to the catalog.";
        }

        public bool RemoveRenovator(string name)
        {
            var renovatorToRemove = this.renovators.FirstOrDefault(r => r.Name == name);

            return this.renovators.Remove(renovatorToRemove);
        }

        public int RemoveRenovatorBySpecialty(string type)
        {
            return this.renovators.RemoveAll(r => r.Type == type);
        }

        public Renovator HireRenovator(string name)
        {
            var renovator = this.renovators.FirstOrDefault(r => r.Name == name);

            if(renovator == null)
                return null;

            renovator.Hired = true;

            return renovator;
        }

        public List<Renovator> PayRenovators(int days)
        {
            return this.renovators.Where(r => r.Days >= days).ToList();
        }

        public string Report()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Renovators available for Project {this.Project}:");

            sb.Append(string.Join(Environment.NewLine, this.renovators.Where(r => r.Hired == false).ToList()));

            return sb.ToString().TrimEnd();
        }
    }
}
