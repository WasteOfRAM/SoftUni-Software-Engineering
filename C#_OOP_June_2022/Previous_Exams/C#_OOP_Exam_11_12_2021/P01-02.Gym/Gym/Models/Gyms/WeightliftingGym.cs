﻿namespace Gym.Models.Gyms
{
    public class WeightliftingGym : Gym
    {
        private const int AthletesCapacity = 20;

        public WeightliftingGym(string name) 
            : base(name, AthletesCapacity)
        {
        }
    }
}
