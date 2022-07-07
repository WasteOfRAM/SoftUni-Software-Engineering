namespace P04.Border_Control
{
    public class Robot : IIdentifiable
    {
        public Robot(string model, string id)
        {
            this.Model = model;
            this.Id = id;
        }

        public string Model { get; private set; }
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
