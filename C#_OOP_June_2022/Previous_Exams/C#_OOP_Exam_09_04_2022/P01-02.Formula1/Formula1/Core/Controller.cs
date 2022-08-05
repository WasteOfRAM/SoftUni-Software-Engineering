namespace Formula1.Core
{
    using Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Models;
    using Models.Contracts;
    using System;
    using Utilities;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IPilot> pilots;
        private IRepository<IRace> races;
        private IRepository<IFormulaOneCar> cars;

        public Controller()
        {
            this.pilots = new PilotRepository();
            this.races = new RaceRepository();
            this.cars = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var pilot = this.pilots.FindByName(pilotName);
            if (pilot == null || pilot.Car != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));

            var car = this.cars.FindByName(carModel);
            if(car == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));

            pilot.AddCar(car);
            this.cars.Remove(car);

            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace race = this.races.FindByName(raceName);
            if (race == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            IPilot pilot = this.pilots.FindByName(pilotFullName);
            if(pilot == null || !pilot.CanRace || race.Pilots.Any(p => p.FullName == pilotFullName))
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));

            race.AddPilot(pilot);

            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            IFormulaOneCar car;

            if(type == "Ferrari")
            {
                car = this.cars.FindByName(model);
                if (car != null)
                    throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));

                car = new Ferrari(model, horsepower, engineDisplacement);
                this.cars.Add(car);
            }
            else if (type == "Williams")
            {
                car = this.cars.FindByName(model);
                if (car != null)
                    throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));

                car = new Williams(model, horsepower, engineDisplacement);
                this.cars.Add(car);
            }
            else
            {
               throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreatePilot(string fullName)
        {
            var pilot = this.pilots.FindByName(fullName);

            if (pilot != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));

            pilot = new Pilot(fullName);
            this.pilots.Add(pilot);
            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            IRace race = this.races.FindByName(raceName);

            if (race != null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));

            race = new Race(raceName, numberOfLaps);

            this.races.Add(race);
            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string PilotReport()
        {
            var sb = new StringBuilder();

            foreach (var pilot in this.pilots.Models.OrderByDescending(p => p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            var sb = new StringBuilder();

            foreach (var race in this.races.Models.Where(r => r.TookPlace))
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartRace(string raceName)
        {
            IRace race = this.races.FindByName(raceName);
            if(race == null)
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));

            if (race.Pilots.Count < 3)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));

            if(race.TookPlace)
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));

            race.TookPlace = true;

            var podium = race.Pilots.OrderByDescending(p => p.Car.RaceScoreCalculator(race.NumberOfLaps)).Take(3).ToList();

            podium[0].WinRace();

            var sb = new StringBuilder();

            sb.AppendLine(string.Format(OutputMessages.PilotFirstPlace, podium[0].FullName, race.RaceName))
                .AppendLine(string.Format(OutputMessages.PilotSecondPlace, podium[1].FullName, race.RaceName))
                .Append(string.Format(OutputMessages.PilotThirdPlace, podium[2].FullName, race.RaceName));

            return sb.ToString();
        }
    }
}
