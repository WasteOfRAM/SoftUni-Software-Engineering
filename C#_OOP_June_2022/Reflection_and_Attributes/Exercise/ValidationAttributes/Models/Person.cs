namespace ValidationAttributes.Models
{
    using Utilities.Attributes;
    public class Person
    {
        private const int MinAge = 12;
        private const int MaxAge = 90;

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        [MyRequired]
        public string Name { get; set; }

        [MyRange(MinAge, MaxAge)]
        public int Age { get; set; }
    }
}
