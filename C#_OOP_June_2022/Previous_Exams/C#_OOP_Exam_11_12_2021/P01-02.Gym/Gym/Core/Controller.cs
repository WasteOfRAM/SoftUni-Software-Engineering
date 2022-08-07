namespace Gym.Core
{
    using System;
    using System.Linq;
    using Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Models.Equipment;
    using Models.Equipment.Contracts;
    using Models.Gyms;
    using Models.Gyms.Contracts;
    using Models.Athletes;
    using Models.Athletes.Contracts;
    using System.Collections.Generic;
    using Utilities.Messages;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IEquipment> equipment;
        private ICollection<IGym> gyms;

        public Controller()
        {
            this.equipment = new EquipmentRepository();
            this.gyms = new HashSet<IGym>();
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            bool validGym = false;
            IAthlete athlete;
            IGym gym = this.gyms.FirstOrDefault(n => n.Name == gymName);

            if(athleteType == "Boxer")
            {
                if (gym.GetType() == typeof(BoxingGym))
                    validGym = true;

                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == "Weightlifter")
            {
                if (gym.GetType() == typeof(WeightliftingGym))
                    validGym = true;

                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            string result = string.Empty;

            if (validGym)
            {
                gym.AddAthlete(athlete);
                result = string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
            }
            else
            {
                result = OutputMessages.InappropriateGym;
            }

            return result;
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment equipment;

            if (equipmentType == "BoxingGloves")
            {
                equipment = new BoxingGloves();
            }
            else if (equipmentType == "Kettlebell")
            {
                equipment = new Kettlebell();
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            this.equipment.Add(equipment);

            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym;

            if(gymType == "BoxingGym")
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == "WeightliftingGym")
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            this.gyms.Add(gym);

            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string EquipmentWeight(string gymName)
        {
            var gym = this.gyms.FirstOrDefault(gn => gn.Name == gymName);

            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, gym.EquipmentWeight);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            var gym = this.gyms.FirstOrDefault(n => n.Name == gymName);
            var equipment = this.equipment.FindByType(equipmentType);

            if (equipment == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));

            gym.AddEquipment(equipment);
            this.equipment.Remove(equipment);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            foreach (var gym in this.gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string TrainAthletes(string gymName)
        {
            var gym = this.gyms.FirstOrDefault(gn => gn.Name == gymName);

            gym.Exercise();

            return string.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
        }
    }
}
