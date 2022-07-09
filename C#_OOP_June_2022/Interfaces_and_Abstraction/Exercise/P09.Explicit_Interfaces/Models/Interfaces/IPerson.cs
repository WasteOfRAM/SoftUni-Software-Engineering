namespace P09.Explicit_Interfaces.Models.Interfaces
{
    public interface IPerson
    {
        string Name { get; }
        int Age { get; }

        string GetName();
    }
}
