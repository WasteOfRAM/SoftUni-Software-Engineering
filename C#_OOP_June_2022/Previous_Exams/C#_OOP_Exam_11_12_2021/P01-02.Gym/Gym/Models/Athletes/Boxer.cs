﻿namespace Gym.Models.Athletes
{
    using Utilities.Messages;
    using System;

    public class Boxer : Athlete
    {
        private const int InitialStamina = 60;

        public Boxer(string fullName, string motivation, int numberOfMedals) 
            : base(fullName, motivation, numberOfMedals, InitialStamina)
        {
        }

        public override void Exercise()
        {
            this.Stamina += 15;

            if(this.Stamina > 100)
            {
                this.Stamina = 100;

                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}