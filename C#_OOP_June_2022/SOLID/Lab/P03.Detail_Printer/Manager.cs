namespace P03.DetailPrinter
{
    using System;
    using System.Collections.Generic;
    public class Manager : IEmployee
    {
        public Manager(string name, ICollection<string> documents)
        {
            this.Name = name;
            this.Documents = new List<string>(documents);
        }

        public IReadOnlyCollection<string> Documents { get; set; }
        public string Name { get; private set; }

        public string Print()
        {
            return this.Name + Environment.NewLine + string.Join(Environment.NewLine, this.Documents);
        }
    }
}
