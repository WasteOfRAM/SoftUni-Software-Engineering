namespace P04.Border_Control
{
    public class Citizen : IIdentifiable
    {
        public Citizen(string name, int age, string id)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Id { get; private set; }

        public bool FakeIdCheck(string fakeIdIdentifier)
        {
            var idLastDigits = this.Id.Substring(this.Id.Length - fakeIdIdentifier.Length, fakeIdIdentifier.Length);

            if (idLastDigits == fakeIdIdentifier)
                return true;

            return false;
        }
    }
}
