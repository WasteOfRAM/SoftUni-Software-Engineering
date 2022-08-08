namespace CarRacing.Models.Maps
{
    using CarRacing.Models.Racers.Contracts;
    using CarRacing.Utilities.Messages;
    using Contracts;
    using System.Collections.Generic;

    public class Map : IMap
    {
        private readonly IReadOnlyDictionary<string, double> racingBehaviorMultiplier = new Dictionary<string, double> { { "strict",1.2 }, { "aggressive", 1.1 } };

        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if(!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }
            else if (!racerOne.IsAvailable() || !racerTwo.IsAvailable())
            {
                return racerOne.IsAvailable() ? string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username) 
                                              : string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }

            racerOne.Race();
            racerTwo.Race();

            double racerOneWiningChance = racerOne.Car.HorsePower * racerOne.DrivingExperience * this.racingBehaviorMultiplier[racerOne.RacingBehavior];
            double racerTwoWiningChance = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * this.racingBehaviorMultiplier[racerTwo.RacingBehavior];

            return racerOneWiningChance > racerTwoWiningChance ? string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerOne.Username)
                                                               : string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerTwo.Username);
        }
    }
}
