namespace Easter.Core
{
    using Contracts;
    using Easter.Models.Bunnies;
    using Easter.Models.Bunnies.Contracts;
    using Easter.Models.Dyes;
    using Easter.Models.Eggs;
    using Easter.Models.Eggs.Contracts;
    using Easter.Models.Workshops;
    using Easter.Models.Workshops.Contracts;
    using Easter.Repositories;
    using Easter.Repositories.Contracts;
    using Easter.Utilities.Messages;
    using System;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IBunny> bunnys;
        private IRepository<IEgg> eggs;
        private IWorkshop workshop;

        public Controller()
        {
            this.bunnys = new BunnyRepository();
            this.eggs = new EggRepository();
            this.workshop = new Workshop();
        }

        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny bunny;

            if(bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            this.bunnys.Add(bunny);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            var bunny = this.bunnys.FindByName(bunnyName);

            if(bunny == null)
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);

            var dye = new Dye(power);
            bunny.AddDye(dye);

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            var egg = new Egg(eggName, energyRequired);
            this.eggs.Add(egg);

            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            var suitableBunnys = this.bunnys.Models.Where(b => b.Energy >= 50).OrderByDescending(b => b.Energy).ToList();

            if(suitableBunnys.Count == 0)
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);

            var egg = this.eggs.FindByName(eggName);

            foreach (var bunny in suitableBunnys)
            {
                this.workshop.Color(egg, bunny);

                if (bunny.Energy == 0)
                    this.bunnys.Remove(bunny);
            }

            return egg.IsDone() ? string.Format(OutputMessages.EggIsDone, eggName) : string.Format(OutputMessages.EggIsNotDone, eggName);
        }

        public string Report()
        {
            var countColoredEggs = this.eggs.Models.Where(e => e.IsDone()).ToList().Count;

            var sb = new StringBuilder();

            sb.AppendLine($"{countColoredEggs} eggs are done!")
                .AppendLine("Bunnies info:");

            foreach (var bunny in this.bunnys.Models)
            {
                sb.AppendLine(bunny.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
