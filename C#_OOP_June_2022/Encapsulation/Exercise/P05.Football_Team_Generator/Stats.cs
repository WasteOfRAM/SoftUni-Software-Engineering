namespace P05.Football_Team_Generator
{
    using System;
    public class Stats
    {
        private const int StatMinValue = 0;
        private const int StatMaxValue = 100;

        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public Stats(int endurance, int sprint, int dribble, int passing, int shooting)
        {
            Endurance = endurance;
            Sprint = sprint;
            Dribble = dribble;
            Passing = passing;
            Shooting = shooting;
        }

        public int Endurance
        {
            get
            {
                return this.endurance;
            }
            private set
            {
                if (value < StatMinValue || value > StatMaxValue)
                    throw new ArgumentException(string.Format(ExeptionMessage.InvalidStats, nameof(this.Endurance), StatMinValue, StatMaxValue));

                this.endurance = value;
            }
        }

        public int Sprint
        {
            get
            {
                return this.sprint;
            }
            private set
            {
                if (value < StatMinValue || value > StatMaxValue)
                    throw new ArgumentException(string.Format(ExeptionMessage.InvalidStats, nameof(this.Sprint), StatMinValue, StatMaxValue));

                this.sprint = value;
            }
        }

        public int Dribble
        {
            get
            {
                return this.dribble;
            }
            private set
            {
                if (value < StatMinValue || value > StatMaxValue)
                    throw new ArgumentException(string.Format(ExeptionMessage.InvalidStats, nameof(this.Dribble), StatMinValue, StatMaxValue));

                this.dribble = value;
            }
        }

        public int Passing
        {
            get
            {
                return this.passing;
            }
            private set
            {
                if (value < StatMinValue || value > StatMaxValue)
                    throw new ArgumentException(string.Format(ExeptionMessage.InvalidStats, nameof(this.Passing), StatMinValue, StatMaxValue));

                this.passing = value;
            }
        }

        public int Shooting
        {
            get
            {
                return this.shooting;
            }
            private set
            {
                if (value < StatMinValue || value > StatMaxValue)
                    throw new ArgumentException(string.Format(ExeptionMessage.InvalidStats, nameof(this.Shooting), StatMinValue, StatMaxValue));

                this.shooting = value;
            }
        }

        public double GetAverageSkill() => (this.Endurance + this.Sprint + this.Dribble + this.Passing + this.Shooting) / 5.0;
    }
}
