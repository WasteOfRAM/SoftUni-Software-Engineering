using Easter.Models.Dyes.Contracts;
using System.Collections.Generic;

namespace Easter.Models.Bunnies
{
    public class SleepyBunny : Bunny
    {
        private const int InitialEnergy = 50;

        public SleepyBunny(string name) 
            : base(name, InitialEnergy, new List<IDye>())
        {
        }

        public override void Work()
        {
            this.Energy -= 15;
        }
    }
}
