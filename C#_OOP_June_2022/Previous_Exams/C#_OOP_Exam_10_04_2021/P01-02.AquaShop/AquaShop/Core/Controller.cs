namespace AquaShop.Core
{
    using System;
    using Models.Aquariums.Contracts;
    using Contracts;
    using Repositories;
    using System.Collections.Generic;
    using Models.Aquariums;
    using Utilities.Messages;
    using AquaShop.Models.Decorations.Contracts;
    using AquaShop.Models.Decorations;
    using System.Linq;
    using AquaShop.Models.Fish.Contracts;
    using AquaShop.Models.Fish;
    using System.Text;

    public class Controller : IController
    {
        private DecorationRepository decorations = new DecorationRepository();
        private List<IAquarium> aquariums = new List<IAquarium>();

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium aquarium;

            if (aquariumType == "FreshwaterAquarium")
                aquarium = new FreshwaterAquarium(aquariumName);
            else if (aquariumType == "SaltwaterAquarium")
                aquarium = new SaltwaterAquarium(aquariumName);
            else
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);

            this.aquariums.Add(aquarium);

            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration decoration;

            if (decorationType == "Ornament")
                decoration = new Ornament();
            else if (decorationType == "Plant")
                decoration = new Plant();
            else
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);

            this.decorations.Add(decoration);

            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            string result = OutputMessages.UnsuitableWater;

            var aquarium = this.aquariums.First(a => a.Name == aquariumName);

            IFish fish;
            
            if(fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
                if(aquarium.GetType() == typeof(FreshwaterAquarium))
                {
                    aquarium.AddFish(fish);
                    result = string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
                }
            }
            else if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
                if (aquarium.GetType() == typeof(SaltwaterAquarium))
                {
                    aquarium.AddFish(fish);
                    result = string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
                }
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            return result;
        }

        public string CalculateValue(string aquariumName)
        {
            var aquarium = this.aquariums.First(a => a.Name == aquariumName);

            var value = aquarium.Fish.Sum(f => f.Price) + aquarium.Decorations.Sum(d => d.Price);

            return string.Format(OutputMessages.AquariumValue, aquariumName, value);
        }

        public string FeedFish(string aquariumName)
        {
            var aquarium = this.aquariums.First(a => a.Name == aquariumName);
            aquarium.Feed();

            return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            var decoration = this.decorations.FindByType(decorationType);

            if (decoration == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));

            this.decorations.Remove(decoration);

            this.aquariums.First(a => a.Name == aquariumName).AddDecoration(decoration);

            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            foreach (var aquarium in this.aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
