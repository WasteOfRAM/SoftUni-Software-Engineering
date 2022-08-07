namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        public const double InitialOxygen = 70;

        public Biologist(string name) 
            : base(name, InitialOxygen)
        {
        }

        public override void Breath()
        {
            double currentOxygen = this.Oxygen - 5;

            if (currentOxygen < 0)
                this.Oxygen = 0;
            else
                this.Oxygen = currentOxygen;
        }
    }
}
