using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const string RacerRacingBehavior = "aggressive";
        private const int Experience = 10;

        public StreetRacer(string username, ICar car) 
            : base(username, RacerRacingBehavior, Experience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            this.DrivingExperience += 5;
        }
    }
}
