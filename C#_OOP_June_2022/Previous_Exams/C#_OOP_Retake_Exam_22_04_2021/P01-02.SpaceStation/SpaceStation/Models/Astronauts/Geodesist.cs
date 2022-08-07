namespace SpaceStation.Models.Astronauts
{
    public class Geodesist : Astronaut
    {
        public const double InitialOxygen = 50;

        public Geodesist(string name) 
            : base(name, InitialOxygen)
        {
        }
    }
}
